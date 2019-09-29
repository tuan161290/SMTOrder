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
    public class OrderViewModel : BaseViewModel
    {
        public Product Product { get; private set; }

        int _Amount;
        public int Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged(nameof(Amount)); }
        }
        public OrderViewModel()
        {
            Amount = Setting.DefaultLots;
            Product = Setting.SelectedProduct;
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
                //Api.Controller.getLstOrderNotFinish(2)?.ForEach(x => LstOrderNotFinish.Add(x));
                MessageBox.Show("Create order successfully");
            }
            else
                MessageBox.Show("Create order failed, something happened");
        }

        private ICommand _orderCommand;
        public ICommand OrderCommand
        {
            get
            {
                return _orderCommand ?? (_orderCommand = new CommandHandler(() => CreateOrder(), () => CanExecute));
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
