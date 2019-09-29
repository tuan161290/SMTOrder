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
    /// Interaction logic for PlanUpdate.xaml
    /// </summary>
    public partial class PlanUpdate : Window
    {
        PlanInfo Plan;
        public PlanUpdate(PlanInfo VM)
        {
            InitializeComponent();
            Plan = VM;
            DataContext = VM;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure want to update current plan?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (await Api.Controller.UpdatePlan(Plan))
                {
                    MessageBox.Show("Plan Updated!");
                }
                else
                {
                    MessageBox.Show("Plan Update failed, something happened!");
                };
            };
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
