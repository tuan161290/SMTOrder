using MonitorApp.Model;
using MonitorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WiSolSMTRepo.Model;

namespace MonitorApp
{
    /// <summary>
    /// Interaction logic for LineMonitor.xaml
    /// </summary>
    public partial class LineMonitor : UserControl
    {
        public List<LineViewModel> LineInfos { get; set; } = new List<LineViewModel>();
        public LineMonitor()
        {
            InitializeComponent();
            Init();
        }

        private async void Init()
        {
            try
            {
                List<LineInfo> RunningPlans = await Api.Controller.GetRunningPlan();
                RunningPlans.ForEach(p => LineInfos.Add(new LineViewModel()
                {
                    Name = p.Name,
                    LineInfoID = p.LineInfoID,
                }));
                DataContext = this;
                await MainLoop();
            }
            catch (Exception)
            {

            }

        }
        FluxOrder NullOrder = new FluxOrder() { FLuxOrderStatus = FLuxOrderStatus.NO_ORDER, Duration = TimeSpan.FromSeconds(0) };
        public async Task MainLoop()
        {

            while (true)
            {
                try
                {
                    List<LineInfo> RunningPlans = await Api.Controller.GetRunningPlan();
                    RunningPlans.ForEach(p =>
                    {
                        LineInfos.ForEach(LineView =>
                        {
                            if (p.LineInfoID == LineView.LineInfoID)
                            {

                                if (p.CurrentPlan != null)
                                {
                                    if (p.CurrentPlan.Remain > 0)
                                    {
                                        if (p.Orders.Count > 0)
                                            LineView.WorkingStatus = WorkingStatus.Order;
                                        else
                                            LineView.WorkingStatus = WorkingStatus.Normal;
                                    }
                                    else
                                    {
                                        LineView.WorkingStatus = WorkingStatus.STOP;
                                    }
                                    LineView.Name = p.Name;
                                    LineView.ProductName = p.CurrentProduct.Name;
                                    LineView.Order = p.CurrentPlan.Order;
                                    LineView.Elapse = p.CurrentPlan.Elapsed;
                                    LineView.Remain = p.CurrentPlan.Remain;

                                    if (p.CurrentFluxOrder != null)
                                    {
                                        if (p.CurrentFluxOrder.FLuxOrderStatus == FLuxOrderStatus.WAITING)
                                        {
                                            p.CurrentFluxOrder.Duration = DateTime.Now - p.CurrentFluxOrder.CreatedTime;
                                            if (p.CurrentFluxOrder.Duration.TotalHours >= 5)
                                            {
                                                p.CurrentFluxOrder.FLuxOrderStatus = FLuxOrderStatus.DEFROST;
                                            }
                                        }
                                        else if (p.CurrentFluxOrder.FLuxOrderStatus == FLuxOrderStatus.DEFROSTING)
                                        {
                                            p.CurrentFluxOrder.Duration = DateTime.Now - p.CurrentFluxOrder.DefrostTimeStamp;
                                            if (p.CurrentFluxOrder.Duration.TotalHours >= 3)
                                            {
                                                p.CurrentFluxOrder.FLuxOrderStatus = FLuxOrderStatus.READY;
                                            }
                                        }
                                        LineView.FluxOrderVM = p.CurrentFluxOrder;
                                    }
                                    else
                                    {
                                        LineView.FluxOrderVM = NullOrder;
                                    }
                                }
                                else
                                {
                                    LineView.Order = 0;
                                    LineView.Elapse = 0;
                                    LineView.Remain = 0;
                                    LineView.ProductName = "NO_PRODUCT";
                                    LineView.WorkingStatus = WorkingStatus.NO_PRODUCTION;
                                    LineView.FluxOrderVM = NullOrder;
                                }
                            }
                        });
                    });
                }
                catch
                {

                }
                await Task.Delay(1000);
            }
        }

        private async void FluxOrderAction_Click(object sender, RoutedEventArgs e)
        {
            var LineVM = ((Button)sender).DataContext as LineViewModel;
            var FluxOrder = LineVM.FluxOrderVM;
            if (FluxOrder != NullOrder)
            {
                if (FluxOrder.FLuxOrderStatus == FLuxOrderStatus.DEFROST)
                {
                    FluxOrder.FLuxOrderStatus = FLuxOrderStatus.DEFROSTING;
                    FluxOrder.DefrostTimeStamp = DateTime.Now;
                    await Api.Controller.UpdateFluxOrder(FluxOrder);
                }
                else if (FluxOrder.FLuxOrderStatus == FLuxOrderStatus.READY)
                {
                    FluxOrder.FLuxOrderStatus = FLuxOrderStatus.OK;
                    await Api.Controller.UpdateFluxOrder(FluxOrder);
                }
            }
        }
    }
}
