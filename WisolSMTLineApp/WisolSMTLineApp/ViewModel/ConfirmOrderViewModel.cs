using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WisolSMTLineApp.Model;

namespace WisolSMTLineApp.ViewModel
{
    public class ConfirmOrderViewModel : BaseViewModel
    {
        int amount = Setting.DefaultLots;
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public ObservableCollection<Order> LstOrderNotFinish { get; set; }
            = new ObservableCollection<Order>();
        public ConfirmOrderViewModel()
        {
            Init();
        }
        async void Init()
        {
            try
            {
                (await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.LineInfoID)).ForEach(x => LstOrderNotFinish.Add(x));
            }
            catch (Exception)
            {

            }
        }
        object lockObject = new object();
        public async void ConfirmOrder(object b)
        {

            var UnconfirmOrder = (Order)b;
            try
            {
                if (UnconfirmOrder != null)
                {
                    if (Api.Controller.ConfirmOrder(UnconfirmOrder))
                    {
                        var Plan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
                        if (Plan != null)
                        {
                            Plan.Remain += UnconfirmOrder.Amount;
                            Plan.Order += UnconfirmOrder.Amount;
                            await Api.Controller.UpdatePlan(Plan);

                        }
                        LstOrderNotFinish.Clear();
                        (await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.LineInfoID)).ForEach(x => LstOrderNotFinish.Add(x));
                        MessageBox.Show("Order Confirmed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Confirm order failed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {

            }
        }

        public async void CreateOrder()
        {
            var CurrentPlan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.LineInfoID, Setting.SelectedProduct.ProductID);
            if (CurrentPlan == null)
            {
                MessageBox.Show("No plan found, please create a new plan");
                return;
            }
            var UnconfirlOrders = await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.LineInfoID);
            if (UnconfirlOrders != null)
            {
                var Order = UnconfirlOrders.Where(x => x.ProductID == Setting.SelectedProduct.ProductID).FirstOrDefault();
                if (Order != null)
                {
                    MessageBox.Show("Please confirm current order");
                    return;
                }
            }

            var order = new Order()
            {
                Amount = Amount,
                CreatedTime = DateTime.Now,
                LineInfoID = Setting.SelectedLine.LineInfoID,
                ProductID = Setting.SelectedProduct.ProductID,
                OrderStatus = OrderStatus.WAITING,
                Reason = OrderShortageReason.WAITING,
                PlanInfoID = CurrentPlan.PlanInfoID
            };
            if (Api.Controller.CreateOrder(order))
            {
                //LstOrderNotFinish.Clear();
                (await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.LineInfoID)).ForEach(x => LstOrderNotFinish.Add(x));
                MessageBox.Show("Create order successfully");
            }
            else
                MessageBox.Show("Create order failed, something happened");
        }


        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new CommandHandler(() => CreateOrder(), () => CanExecute));
            }
        }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler((p) => ConfirmOrder(p), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }
    }
}
