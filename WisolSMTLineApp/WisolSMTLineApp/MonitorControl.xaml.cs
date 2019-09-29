
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WisolSMTLineApp.Model;
using WisolSMTLineApp.ViewModel;
using static PandaApp.GPIOCommunication.GPIOHelper;
using static WisolSMTLineApp.MainWindow;

namespace WisolSMTLineApp
{
    /// <summary>
    /// Interaction logic for MonitorControl.xaml
    /// </summary>
    public partial class MonitorControl : UserControl
    {

        MonitorViewModel MonitorVM;
        public MonitorControl()
        {
            InitializeComponent();
            Loaded += MonitorControl_Loaded;
            Unloaded += MonitorControl_Unloaded;
        }

        private void MonitorControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (MonitorVM != null)
            {
                MonitorVM.Dispose();
            }
        }

        private void MonitorControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!StartUp)
            {
                MonitorVM = new MonitorViewModel();
                DataContext = MonitorVM;
            }
            StartUp = false;
        }

        private void TextBlock_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var VM = ((TextBlock)sender).DataContext as MonitorViewModel;
            if (VM.Plan != null)
            {
                PlanUpdate planUpdate = new PlanUpdate(VM.Plan);
                planUpdate.ShowDialog();
            }
        }
    }
}
