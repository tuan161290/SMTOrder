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
        public PlanInfo Plan { get; private set; }
        public PlanViewObject PlanView { get; set; } = new PlanViewObject();
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
                else if (WorkingStatus == WorkingStatus.STOP)
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

        //public Plan PlanView
        //{
        //    get;
        //    set;
        //} = new Plan();
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
            var CurrentPlan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
            if (CurrentPlan == null)
            {
                return;
            }
            var UnconfirlOrders = await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.LineInfoID);
            if (UnconfirlOrders != null)
            {
                var Order = UnconfirlOrders.Where(x => x.ProductID == Setting.SelectedProduct.ProductID).FirstOrDefault();
                if (Order != null)
                {
                    return;
                }
            }

            var order = new Order()
            {
                Amount = Setting.DefaultLots,
                CreatedTime = DateTime.Now,
                LineInfoID = Setting.SelectedLine.LineInfoID,
                ProductID = Setting.SelectedProduct.ProductID,
                OrderStatus = OrderStatus.WAITING,
                Reason = OrderShortageReason.WAITING,
                PlanInfoID = CurrentPlan.PlanInfoID
            };
            Api.Controller.CreateOrder(order);

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
                    if (WorkingStatus == WorkingStatus.STOP)
                        UnconfirmOrder.StopDuration = (int)Duration.TotalSeconds;
                    if (Api.Controller.ConfirmOrder(UnconfirmOrder))
                    {
                        var Plan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
                        if (Plan != null)
                        {
                            Plan.Remain += UnconfirmOrder.Amount;
                            Plan.Order += UnconfirmOrder.Amount;
                        }
                        await Api.Controller.UpdatePlan(Plan);

                    }
                    MessageBox.Show("Order Confirmed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Confirm order failed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void StopLine()
        {
            var Plan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
            if (Plan != null)
            {
                Plan.IsComplete = true;
                if (await Api.Controller.DiscardRemainOrder(Plan))
                    await Api.Controller.UpdatePlan(Plan);
            }
            //WorkingStatus = WorkingStatus.NO_PRODUCTION;
        }

        private async void CreateFluxOrder()
        {
            if ((await Api.Controller.GetUnfinishFluxOrderAsync(Setting.SelectedLine.LineInfoID)) == null)
            {
                FluxOrder FluxOrder = new FluxOrder()
                {
                    CreatedTime = DateTime.Now,
                    FLuxOrderStatus = FLuxOrderStatus.WAITING,
                    LineInfoID = Setting.SelectedLine.LineInfoID
                };
                if (await Api.Controller.CreateFluxOrderAsync(FluxOrder))
                {
                    MessageBox.Show("Solder order create successfully");
                }
            }
            else
                MessageBox.Show("Solder order has already created");
        }
        private async void ConfirmFluxOrder()
        {
            var FluxOrder = await Api.Controller.GetUnfinishFluxOrderAsync(Setting.SelectedLine.LineInfoID);
            if (FluxOrder != null)
            {
                if (FluxOrder.FLuxOrderStatus == FLuxOrderStatus.OK || FluxOrder.FLuxOrderStatus == FLuxOrderStatus.READY)
                {
                    FluxOrder.IsFinished = true;
                    if (await Api.Controller.UpdateFluxOrder(FluxOrder))
                    {
                        MessageBox.Show("Solder order confirmed successfully");
                    }
                    else
                        MessageBox.Show("Error, Something happened!");
                }
                else
                {
                    MessageBox.Show("Solder is not ready!");
                }
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
            Product = Setting.SelectedProduct;
            CancelTask();
            UpdateUITaskCTS = new CancellationTokenSource();
            UpdateUILoop(UpdateUITaskCTS.Token);
        }

        Order UnconfirmOrder = null;
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
                var CurrentPlan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
                if (CurrentPlan == null)
                {
                    IN.CountingSensor.OnPinValueChanged -= CountingSensor_OnPinValueChanged;
                }
                else
                {
                    IN.CountingSensor.OnPinValueChanged += CountingSensor_OnPinValueChanged;
                }

                while (true)
                {
                    token.ThrowIfCancellationRequested();

                    Order LastOrder = null;
                    var Orders = await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.LineInfoID);
                    var Plan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
                    if (Orders != null && Plan != null)
                    {
                        if (Orders.Count > 0)
                            LastOrder = Orders.Where(x => x.ProductID == Setting.SelectedProduct.ProductID).First();
                    }

                    if (LastOrder != null)
                    {
                        UnconfirmOrder = LastOrder;
                        OrderDuration = "No action";

                        if (WorkingStatus == WorkingStatus.STOP)
                        {
                            Duration = DateTime.Now - StartTime;
                            OrderDuration = $"{LastOrder?.Reason.ToString().ToUpper()} | {Duration.ToString("hh\\:mm\\:ss")}";
                        }
                        else if (WorkingStatus == WorkingStatus.Order)
                        {
                            StartTime = DateTime.Now;
                            OrderDuration = $"{LastOrder.Reason.ToString().ToUpper()} | {Duration.ToString("hh\\:mm\\:ss")}";
                        }
                    }
                    else
                    {
                        StartTime = DateTime.Now;
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
                    this.Plan = Plan;
                    if (Plan != null)
                    {
                        PlanView.Elapsed = Plan.Elapsed;
                        PlanView.Ordered = Plan.Order;
                        PlanView.Remain = Plan.Remain;
                        if (PlanView.Remain > 0)
                        {
                            if (PlanView.Remain <= Setting.DefaultLevel)
                            {
                                WorkingStatus = WorkingStatus.Order;
                                Ellipse = new SolidColorBrush(Colors.Orange);

                            }
                            else if (PlanView.Remain > Setting.DefaultLevel)
                            {
                                WorkingStatus = WorkingStatus.Normal;
                                Ellipse = new SolidColorBrush(Colors.Green);

                            }
                        }
                        if (PlanView.Remain == 0)
                        {
                            WorkingStatus = WorkingStatus.STOP;
                            Ellipse = new SolidColorBrush(Colors.Red);
                        }
                    }
                    else
                    {
                        PlanView.Elapsed = 0;
                        PlanView.Remain = 0;
                        PlanView.Ordered = 0;
                        OrderDuration = "NO PRODUCTION";
                        WorkingStatus = WorkingStatus.NO_PRODUCTION;
                        Ellipse = new SolidColorBrush(Colors.Gray);
                        //return;
                    }
                    await Task.Delay(3000);
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
                await LockObject.WaitAsync();
                //if (PlanView.remain > 0)
                {
                    var Plan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
                    if (Plan != null)
                    {
                        if (Plan != null)
                            if (Plan.Remain > 0)
                            {
                                Plan.Elapsed += 1;
                                Plan.Remain--;
                                await Api.Controller.UpdatePlan(Plan);
                            }
                    }
                }
                LockObject.Release();
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
            //CountingSensor_OnPinValueChanged(null, new GPIOPin.PinValueChangedEventArgs(Edge.Rise));
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

        private ICommand _StopCommand;
        public ICommand StopCommand
        {
            get
            {
                return _StopCommand ?? (_StopCommand = new CommandHandler(() => StopLine(), () => CanExecute));
            }
        }

        private ICommand _CreateFluxOrderCommand;
        public ICommand CreateFluxOrderCommand
        {
            get
            {
                return _CreateFluxOrderCommand ?? (_CreateFluxOrderCommand = new CommandHandler(() => CreateFluxOrder(), () => CanExecute));
            }
        }

        private ICommand _ConfirmFluxOrderCommand;
        public ICommand ConfirmFluxOrderCommand
        {
            get
            {
                return _ConfirmFluxOrderCommand ?? (_ConfirmFluxOrderCommand = new CommandHandler(() => ConfirmFluxOrder(), () => CanExecute));
            }
        }
    }
}
