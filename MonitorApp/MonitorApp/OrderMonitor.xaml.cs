using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for OrderMonitor.xaml
    /// </summary>
    public partial class OrderMonitor : UserControl
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<LineInfo> LineInfos { get; set; } = new List<LineInfo>();
        public ObservableCollection<Order> UnconfirmOrders { get; set; } = new ObservableCollection<Order>();

        public OrderMonitor()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += OrderMonitor_Loaded;
            Unloaded += OrderMonitor_Unloaded;
            //Init();

        }

        private void OrderMonitor_Unloaded(object sender, RoutedEventArgs e)
        {
            CancelTask();
            UnconfirmOrders.CollectionChanged -= UnconfirmOrders_CollectionChanged;
        }

        private void OrderMonitor_Loaded(object sender, RoutedEventArgs e)
        {
            UnconfirmOrders.CollectionChanged += UnconfirmOrders_CollectionChanged;
            Init();
        }

        private void UnconfirmOrders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                myMediaElement.Stop();
                myMediaElement.Play();
            }
        }
        CancellationTokenSource MainLoopCTS;
        public void CancelTask()
        {
            if (MainLoopCTS != null)
            {
                if (!MainLoopCTS.IsCancellationRequested)
                    MainLoopCTS.Cancel();
            }
        }
        private async void Init()
        {
            try
            {
                if (MainLoopCTS != null)
                {
                    if (!MainLoopCTS.IsCancellationRequested)
                        MainLoopCTS.Cancel();
                }
                MainLoopCTS = new CancellationTokenSource();
                await MainLoop(MainLoopCTS.Token);
            }
            catch (Exception)
            {

            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = sender as Button;
            var order = ClickedButton.DataContext as Order;
            if (ClickedButton.Tag.ToString() == "ok")
            {
                if (order.OrderStatus == OrderStatus.WAITING)
                {
                    order.OrderStatus = OrderStatus.OK;
                    order.Reason = OrderShortageReason.OK;
                }
            }
            else if (ClickedButton.Tag.ToString() == "jig-shortage")
            {
                order.OrderStatus = OrderStatus.SHORTAGE;
                order.Reason = OrderShortageReason.JIG_Shortage;
            }
            else if (ClickedButton.Tag.ToString() == "magazin-shortage")
            {
                order.OrderStatus = OrderStatus.SHORTAGE;
                order.Reason = OrderShortageReason.MGZ_Shortage;
            }
            else if (ClickedButton.Tag.ToString() == "plasma-waiting")
            {
                order.OrderStatus = OrderStatus.SHORTAGE;
                order.Reason = OrderShortageReason.PLM_Waiting;
            }
            else if (ClickedButton.Tag.ToString() == "pcb-shortage")
            {
                order.OrderStatus = OrderStatus.SHORTAGE;
                order.Reason = OrderShortageReason.PCB_Shortage;
            }
            await Api.Controller.UpdateOrder(order);
            myMediaElement.Stop();
        }

        object LockObject = new object();
        public async Task MainLoop(CancellationToken token)
        {
            try
            {
                Products = await Api.Controller.GetProducts();
                LineInfos = Api.Controller.getLstLine();
            }
            catch
            {
                //MessageBox.Show("Fail To start, Please run app again");
            }

            while (true)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    var List_UnconfirmOrder = await Api.Controller.getLstOrderNotFinishAsync();
                    if (List_UnconfirmOrder != null)
                    {
                        if (UnconfirmOrders.Count > 0)
                        {
                            var MaxID = UnconfirmOrders.Max(x => x.OrderID);
                            var NewOrder = List_UnconfirmOrder.Where(x => x.OrderID > MaxID).ToList();
                            NewOrder.ForEach(x =>
                            {
                                x.LineInfo = LineInfos.Where(z => z.LineInfoID == x.LineInfoID).FirstOrDefault();
                                x.Product = Products.Where(z => z.ProductID == x.ProductID).FirstOrDefault();
                                UnconfirmOrders.Add(x);
                            });
                            List<Order> ConfirmedOrder = new List<Order>();
                            foreach (Order Order in UnconfirmOrders)
                            {
                                if (List_UnconfirmOrder.Where(x => x.OrderID == Order.OrderID).FirstOrDefault() == null)
                                {
                                    ConfirmedOrder.Add(Order);
                                }
                            }
                            if (ConfirmedOrder.Count > 0)
                            {
                                ConfirmedOrder.ForEach(x => UnconfirmOrders.Remove(x));
                            }
                        }
                        else
                        {
                            List_UnconfirmOrder.ForEach(x =>
                            {
                                x.LineInfo = LineInfos.Where(z => z.LineInfoID == x.LineInfoID).FirstOrDefault();
                                x.Product = Products.Where(z => z.ProductID == x.ProductID).FirstOrDefault();
                                UnconfirmOrders.Add(x);
                            });
                        }

                    }
                }
                catch
                {
                    return;
                }
                await Task.Delay(1000);
            }
        }
    }
}
