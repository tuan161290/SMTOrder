using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WisolSMTLineApp.Model;

namespace WisolSMTLineApp
{
    /// <summary>
    /// Interaction logic for FluxOrderUpdate.xaml
    /// </summary>
    public partial class FluxOrderUpdate : Window
    {

        public FluxOrder CurrentFluxOrder { get; set; }

        public FluxOrderUpdate(FluxOrder createdFluxOrder)
        {
            InitializeComponent();
            createdFluxOrder.LineInput = App.Now;
            CurrentFluxOrder = createdFluxOrder;
            DataContext = this;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            CurrentFluxOrder.IsFinished = true;
            CurrentFluxOrder.LineInput = App.Now;
            if (await Api.Controller.UpdateFluxOrder(CurrentFluxOrder))
            {
                MessageBox.Show("Solder input confirmed successfully");
            }
            else
                MessageBox.Show("Error, Something happened!");

            FluxOrder FluxOrder = new FluxOrder()
            {
                CreatedTime = App.Now,
                LineInfoID = Setting.SelectedLine.LineInfoID,
                FLuxOrderStatus = FLuxOrderStatus.WAITING,
                IsFinished = false,
            };
            if (await Api.Controller.CreateFluxOrderAsync(FluxOrder))
            {
                MessageBox.Show("New solder order created successfully");
            }
            else
                MessageBox.Show("Error, Something happened!");

        }
    }
}
