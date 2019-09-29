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
            //var Plan = await Api.Controller.GetProductionPlanAsync(Setting.SelectedLine.ID);
            var Orders = await Api.Controller.getLstOrderNotFinishAsync(Setting.SelectedLine.ID);
            if (Orders != null)
            {
                var Order = Orders.Where(x => x.ID == Setting.SelectedProduct.ID).FirstOrDefault();
                if (Order != null)
                {
                    MessageBox.Show("Please confirm current order");
                    return;
                }
            }

            var ProductionDtl = new ProductionDtl()
            {
                Amount = Amount,
                Factory_ID = 1,
                Working_Date = App.TodayDate,
                Shift_ID = App.CurrentShift,
                Line_ID = Setting.SelectedLine.ID,
                Product_ID = Setting.SelectedProduct.ID,
                Message = "waiting"
            };
            if (Api.Controller.CreateOrder(ProductionDtl))
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
