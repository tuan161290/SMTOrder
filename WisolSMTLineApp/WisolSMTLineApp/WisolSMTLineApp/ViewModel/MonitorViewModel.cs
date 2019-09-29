using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WisolSMTLineApp.Model;
using static PandaApp.GPIOCommunication.GPIOHelper;
using static WisolSMTLineApp.App;

namespace WisolSMTLineApp.ViewModel
{
    public class MonitorViewModel : BaseViewModel, IDisposable
    {
        public Product Product { get; private set; }
        static WorkingStatus _WorkingStatus;
        public WorkingStatus WorkingStatus
        {
            get { return _WorkingStatus; }
            set
            {
                if (_WorkingStatus != value)
                {
                    _WorkingStatus = value;
                    OnPropertyChanged("WorkingStatus");
                    if (_WorkingStatus == WorkingStatus.Order)
                    {
                        CreateOrder();
                    }
                }
            }
        }
        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "WorkingStatus")
            {

                if (WorkingStatus == WorkingStatus.Normal)
                {
                    await LightOff();
                }
                else if (WorkingStatus == WorkingStatus.Order)
                {
                    await LightOn();
                }
                else if (WorkingStatus == WorkingStatus.Stop)
                {
                    await LightConstanslyOn();
                }
            }
            base.OnPropertyChanged(propertyName);
        }
        SemaphoreSlim LightLock = new SemaphoreSlim(1, 1);
        CancellationTokenSource LightCancelationTS;
        private async Task LightOn()
        {
            try
            {
                LightCancelationTS = new CancellationTokenSource();
                await OUT.GreenLight.SET();
                await Task.Delay(3000, LightCancelationTS.Token);
                await OUT.GreenLight.RST();
            }
            catch
            {
            }
        }

        private async Task LightConstanslyOn()
        {
            if (LightCancelationTS != null)
                if (!LightCancelationTS.IsCancellationRequested)
                    LightCancelationTS.Cancel();
            await OUT.GreenLight.SET();
        }

        private async Task LightOff()
        {
            if (LightCancelationTS != null)
                if (!LightCancelationTS.IsCancellationRequested)
                    LightCancelationTS.Cancel();
            await OUT.GreenLight.RST();
        }

        public Plan PlanView
        {
            get;
            set;
        } = new Plan();
        private string _OrderDuration;
        public string OrderDuration
        {
            get { return _OrderDuration; }
            set
            {
                if (_OrderDuration != value)
                {
                    _OrderDuration = value;
                    OnPropertyChanged(nameof(OrderDuration));
                }
            }
        }
        //Timer T;

        public async void CreateOrder()
        {
            var Orders = await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.ID);
            if (Orders != null)
            {
                if (Orders.Count > 0)
                {
                    return;
                }
            }
            var ProductionDtl = new ProductionDtl()
            {
                Amount = Setting.DefaultLots,
                Factory_ID = 1,
                Working_Date = App.TodayDate,
                Shift_ID = 1,
                Line_ID = Setting.SelectedLine.ID,
                Product_ID = Setting.SelectedProduct.ID,
                Message = "waiting"
            };
            if (Api.Controller.CreateOrder(ProductionDtl))
            {
                //LstOrderNotFinish.Clear();
                //Api.Controller.getLstOrderNotFinish(2)?.ForEach(x => LstOrderNotFinish.Add(x));
                //MessageBox.Show("Create order successfully");
            }
            else
                MessageBox.Show("Create order failed, something happened");
        }

        SemaphoreSlim ConfirmOrderLock = new SemaphoreSlim(1, 1);
        public async void ConfirmOrder()
        {
            //Confirm to server
            if (ConfirmOrderLock.CurrentCount == 0)
                return;
            await ConfirmOrderLock.WaitAsync();
            try
            {
                if (UnconfirmOrder != null)
                {
                    if (WorkingStatus == WorkingStatus.Stop)
                        UnconfirmOrder.Duaration = (int)Duration.TotalSeconds;
                    if (Api.Controller.ConfirmOrder(UnconfirmOrder))
                    {
                        var Plans = Api.Controller.GetProductionPlan(Setting.SelectedLine.ID);
                        if (Plans != null)
                        {
                            if (Plans.Count > 0)
                            {
                                var Plan = Plans.Where(x => x.Product_ID == UnconfirmOrder.Product_ID).First();
                                if (Plan != null)
                                {
                                    Plan.Remain_Qty += UnconfirmOrder.Amount;
                                    Plan.Ordered_Qty += UnconfirmOrder.Amount;
                                    await Api.Controller.UpdatePlan(Plan);
                                }
                            }
                        }
                        MessageBox.Show("Order Confirmed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Confirm order failed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                ConfirmOrderLock.Release();
            }
        }

        CancellationTokenSource UpdateUITaskCTS;
        public void CancelTask()
        {
            if (UpdateUITaskCTS != null)
                if (!UpdateUITaskCTS.IsCancellationRequested)
                    UpdateUITaskCTS.Cancel();
        }

        public MonitorViewModel()
        {
            CancelTask();
            UpdateUITaskCTS = new CancellationTokenSource();
            UpdateUILoop(UpdateUITaskCTS.Token);
            Product = Setting.SelectedProduct;
        }

        ProductionDtl UnconfirmOrder = null;
        DateTime StartTime = DateTime.Now;
        TimeSpan Duration;
        private async void UpdateUILoop(CancellationToken token)
        {
            try
            {
                //var ProductionPlan1 = await Api.Controller.GetLinePlan(new ProductionPlan()
                //{
                //    Factory_ID = 1,
                //    Working_Date = TodayDate,
                //    Product_ID = Setting.SelectedProduct.ID,
                //    Line_ID = Setting.SelectedLine.ID,
                //    Shift_ID = CurrentShift
                //});
                var Plans = Api.Controller.GetProductionPlan(Setting.SelectedLine.ID).Where(x => x.Is_Active == true).ToList();
                if (Plans == null)
                {
                    IN.CountingSensor.OnPinValueChanged -= CountingSensor_OnPinValueChanged;
                    return;
                }
                else
                {
                    IN.CountingSensor.OnPinValueChanged += CountingSensor_OnPinValueChanged;
                }

                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    ProductionDtl LastOrder = null;
                    var Orders = await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.ID);
                    if (Orders != null)
                        if (Orders.Count > 0)
                            LastOrder = Orders.Where(x => x.Product_ID == Setting.SelectedProduct.ID).First();
                    if (LastOrder != null)
                    {
                        UnconfirmOrder = LastOrder;
                        OrderDuration = "No action";
                       
                        if (WorkingStatus == WorkingStatus.Stop)
                        {
                            Duration = DateTime.Now - StartTime;
                            OrderDuration = $"{LastOrder.Message?.ToUpper()} | {Duration.ToString("hh\\:mm\\:ss")}";
                        }
                        else if (WorkingStatus == WorkingStatus.Order)
                        {
                            StartTime = DateTime.Now;
                            OrderDuration = $"{LastOrder.Message?.ToUpper()} | {Duration.ToString("hh\\:mm\\:ss")}";
                        }
                        else
                        {
                            StartTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        OrderDuration = "No Order";
                        UnconfirmOrder = null;
                    }
                    //var ProductionPlan = Api.Controller.GetProductionPlan(Setting.SelectedLine.ID);
                    //var ProductionPlan = await Api.Controller.GetProductionPlan(new ProductionPlan()
                    //{
                    //    Factory_ID = 1,
                    //    Working_Date = TodayDate,
                    //    Product_ID = Setting.SelectedProduct.ID,
                    //    Line_ID = Setting.SelectedLine.ID,
                    //    Shift_ID = CurrentShift
                    //});
                    Plans = (await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.ID)).ToList();
                    if (Plans != null)
                    {
                        var Plan = Plans.Where(x => x.Product_ID == Setting.SelectedProduct.ID &&
                                               x.Working_Date == TodayDate).FirstOrDefault();
                        if (Plan == null)
                        {
                            foreach (ProductionPlan P in Plans)
                            {
                                if (P.Working_Date != TodayDate)
                                {
                                    P.Is_Active = false;
                                    await Api.Controller.UpdatePlan(P);
                                }
                            }
                            var success = Api.Controller.NewProductionPlan(new ProductionPlan()
                            {
                                Product_ID = Setting.SelectedProduct.ID,
                                //Name = Setting.SelectedProduct.Name,
                                Factory_ID = 1,
                                Line_ID = Setting.SelectedLine.ID,
                                Working_Date = App.TodayDate,
                                Ordered_Qty = PlanView.order,
                                Remain_Qty = PlanView.remain,
                                Good_Prod_Qty = PlanView.elapsed,
                                Is_Active = true
                                //Shift_ID = SelectedShift.ID,
                            });
                        }
                        else
                        {
                            PlanView.elapsed = Plan.Good_Prod_Qty;
                            PlanView.remain = Plan.Remain_Qty;
                            PlanView.order = Plan.Ordered_Qty;
                        }

                        if (PlanView.remain > 0)
                        {
                            if (PlanView.remain <= Setting.DefaultLevel)
                            {
                                WorkingStatus = WorkingStatus.Order;
                                Ellipse = new SolidColorBrush(Colors.Orange);

                            }
                            else if (PlanView.remain > Setting.DefaultLevel)
                            {
                                WorkingStatus = WorkingStatus.Normal;
                                Ellipse = new SolidColorBrush(Colors.Green);

                            }
                        }
                        if (PlanView.remain == 0)
                        {
                            WorkingStatus = WorkingStatus.Stop;
                            Ellipse = new SolidColorBrush(Colors.Red);
                        }
                    }
                    await Task.Delay(1000);
                }
            }
            catch
            {

            }
        }
        SemaphoreSlim LockObject = new SemaphoreSlim(1, 1);
        private async void CountingSensor_OnPinValueChanged(object sender, GPIOPin.PinValueChangedEventArgs e)
        {
            if (e.Edge == Edge.Rise)
            {
                //await LockObject.WaitAsync();
                if (PlanView.remain > 0)
                {
                    var Plans = (await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.ID)).
                                Where(x => x.Is_Active == true).ToList();
                    if (Plans != null)
                    {
                        int count = Plans.Count;
                        var Plan = Plans.Where(x => x.Product_ID == Setting.SelectedProduct.ID).FirstOrDefault();
                        if (Plan != null)
                            if (Plan.Remain_Qty > 0)
                            {
                                Plan.Good_Prod_Qty += 1;
                                Plan.Remain_Qty--;
                                await Api.Controller.UpdatePlan(Plan);
                            }
                    }
                }
                //LockObject.Release();
            }
        }
        SolidColorBrush _Ellipse = new SolidColorBrush(Colors.Green);
        public SolidColorBrush Ellipse
        {
            get { return _Ellipse; }
            set
            {
                _Ellipse = value; OnPropertyChanged(nameof(Ellipse));
            }
        }


        private async void SensorDetected()
        {
            await LightOff();
            CountingSensor_OnPinValueChanged(null, new GPIOPin.PinValueChangedEventArgs(Edge.Rise));
        }

        public void Dispose()
        {
            IN.CountingSensor.OnPinValueChanged -= CountingSensor_OnPinValueChanged;
            CancelTask();
        }

        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        private ICommand _ClickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _ClickCommand ?? (_ClickCommand = new CommandHandler(() => SensorDetected(), () => CanExecute));
            }
        }

        private ICommand _OrderCommand;
        public ICommand OrderCommand
        {
            get
            {
                return _OrderCommand ?? (_OrderCommand = new CommandHandler(() => ConfirmOrder(), () => CanExecute));
            }
        }
    }
}
