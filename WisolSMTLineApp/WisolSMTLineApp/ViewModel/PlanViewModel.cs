using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using WisolSMTLineApp.Model;

namespace WisolSMTLineApp.ViewModel
{
    public class PlanViewModel : BaseViewModel
    {
        public static PlanViewModel PlanVM;
        //ProductionPlan CreatedPlan;
        public ObservableCollection<Shift> Shifts { get; private set; } = new ObservableCollection<Shift>();
        public Shift SelectedShift { get; set; }
        public static ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();
        Product selectedProduct;
        public Product SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                selectedProduct = value;
                Setting.SelectedProduct = selectedProduct;
                OnPropertyChanged(nameof(SelectedProduct));
                //TextHelper.WriteToSetting("SelectedProduct", JsonConvert.SerializeObject(value));
                //TextHelper.SaveToFile();
            }
        }
        public int RemainNodes { get; set; } = Setting.DefaultLots;
        public Controller controller;
        static TimeSpan TodayDateTime { get { return TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss")); } }

        DateTime _SelectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        object lockObject = new object();
        public PlanViewModel()
        {
            PlanVM = this;
            Initilize();
        }
        public async void Initilize()
        {
            //List<Shift> _shifts = null;
            List<Product> _products = null;
            try
            {
                await Task.Run(async () =>
                {
                    controller = new Controller();
                    //_shifts = await controller.GetShifts();
                    _products = await controller.GetProducts();
                    //CreatedPlan = Api.Controller.GetProductionPlan(App.TodayDate, 1, Setting.SelectedLine.ID, App.CurrentShift);
                });
            }
            catch (Exception)
            {

            }
            finally
            {
                //if (_shifts != null)
                //{
                //    Shifts.Clear();
                //    _shifts.ForEach(x => { Shifts.Add(x); });
                //    SelectedShift = (App.CurrentShift == 1) ? Shifts[0] : Shifts[1];
                //}
                if (_products != null)
                {
                    Products.Clear();
                    _products.ForEach(x => Products.Add(x));
                    if (Setting.SelectedProduct != null)
                    {
                        SelectedProduct = Products.Where(x => x.ProductID == Setting.SelectedProduct.ProductID).FirstOrDefault();
                    }
                    else
                    {
                        SelectedProduct = Products[0];
                    }
                    //Setting.SelectedProduct = SelectedProduct;
                }
                if (Setting.SelectedLine != null)
                    SelectedLine = Setting.SelectedLine;
            }
        }
        public void Create_Plan()
        {
            var success = controller.NewProductionPlan(new PlanInfo()
            {
                ProductID = Setting.SelectedProduct.ProductID,
                LineInfoID = Setting.SelectedLine.LineInfoID,
                CreatedTime = DateTime.Now,
                Remain = RemainNodes,
                Order = RemainNodes,
                IsComplete = false,
            });
            MessageBox.Show(success);
        }


        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => Create_Plan(), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }
        LineInfo selectedLine;
        public LineInfo SelectedLine
        {
            get { return selectedLine; }
            set { selectedLine = value; OnPropertyChanged(nameof(SelectedLine)); }
        }
    }
}
