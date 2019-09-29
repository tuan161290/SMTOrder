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
        public int RemainNodes { get; set; }
        public Controller controller;
        static ShiftPeriod DayShift = new ShiftPeriod() { From = TimeSpan.Parse("08:00:00") };
        //static readonly ShiftPeriod NightShift = new ShiftPeriod() { From = TimeSpan.Parse("20:00:00") };
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
        public static int CurrentShift
        {
            get
            {
                //TimeSpan NowTimeStamp = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                if (TodayDateTime >= DayShift.From && TodayDateTime < DayShift.To)
                    return 1;
                else
                    return 2;
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
                        SelectedProduct = Products.Where(x => x.ID == Setting.SelectedProduct.ID).FirstOrDefault();
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
        public async void Create_Plan()
        {
            var CreatePlans = Api.Controller.GetProductionPlan(Setting.SelectedLine.ID);
            bool IsPlanCreated = false;
            if (CreatePlans != null)
            {
                foreach (ProductionPlan Plan in CreatePlans)
                {
                    if (Plan.Product_ID == Setting.SelectedProduct.ID && Plan.Working_Date == App.TodayDate)
                    {
                        MessageBox.Show("Plan has already created");
                        IsPlanCreated = true;
                    }
                    else
                    {
                        if (Plan.Working_Date != App.TodayDate)
                        {
                            Plan.Is_Active = false;
                            await Api.Controller.UpdatePlan(Plan);
                        }
                    }
                }
                if (!IsPlanCreated)
                {
                    var success = controller.NewProductionPlan(new ProductionPlan()
                    {
                        Product_ID = Setting.SelectedProduct.ID,
                        //Name = Setting.SelectedProduct.Name,
                        Factory_ID = 1,
                        Line_ID = Setting.SelectedLine.ID,
                        Working_Date = App.TodayDate,
                        Remain_Qty = RemainNodes,
                        Ordered_Qty = RemainNodes,
                        Is_Active = true
                        //Shift_ID = SelectedShift.ID,
                    });
                    if (success)
                    {
                        MessageBox.Show("Plan created");
                    }
                    else
                    {
                        MessageBox.Show("Plan create failed, something happened");
                    }
                }
            }
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
