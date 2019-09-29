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
        public ObservableCollection<ProductionDtl> LstOrderNotFinish { get; set; }
            = new ObservableCollection<ProductionDtl>();
        public ConfirmOrderViewModel()
        {
            Api.Controller.getLstOrderNotFinish(Setting.SelectedLine.ID)?.ForEach(x => LstOrderNotFinish.Add(x));
        }
        object lockObject = new object();
        public async void ConfirmOrder(object b)
        {
            //Confirm to server
            var UnconfirmOrder = (ProductionDtl)b;
            //lock (lockObject)
            //{
            //    if (Api.Controller.ConfirmOrder(ProductionDtl))
            //    {
            //        LstOrderNotFinish.Clear();
            //        Api.Controller.getLstOrderNotFinish(Setting.SelectedLine.ID)?.ForEach(x => LstOrderNotFinish.Add(x));
            //        MessageBox.Show("Order Confirmed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Confirm order failed", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
            try
            {
                if (UnconfirmOrder != null)
                {
                    if (Api.Controller.ConfirmOrder(UnconfirmOrder))
                    {
                        var Plans = Api.Controller.GetProductionPlan(Setting.SelectedLine.ID);
                        if (Plans != null)
                        {
                            if (Plans.Count > 0)
                            {
                                var Plan = Plans.Where(x => x.Product_ID == UnconfirmOrder.Product_ID).FirstOrDefault();
                                if (Plan != null)
                                {
                                    Plan.Remain_Qty += UnconfirmOrder.Amount;
                                    Plan.Ordered_Qty += UnconfirmOrder.Amount;
                                    await Api.Controller.UpdatePlan(Plan);
                                }
                            }
                        }
                        LstOrderNotFinish.Clear();
                        Api.Controller.getLstOrderNotFinish(Setting.SelectedLine.ID)?.ForEach(x => LstOrderNotFinish.Add(x));
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

        public void CreateOrder()
        {
            var ProductionDtl = new ProductionDtl()
            {
                Amount = Amount,
                Factory_ID = 1,
                Working_Date = App.TodayDate,
                Shift_ID = App.CurrentShift,
                Line_ID = Setting.SelectedLine.ID,
                Product_ID = Setting.SelectedProduct.ID,
                Message = "WAITTING"
            };
            if (Api.Controller.CreateOrder(ProductionDtl))
            {
                LstOrderNotFinish.Clear();
                Api.Controller.getLstOrderNotFinish(Setting.SelectedLine.ID)?.ForEach(x => LstOrderNotFinish.Add(x));
                MessageBox.Show("Create order successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Create order failed, something happened", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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
