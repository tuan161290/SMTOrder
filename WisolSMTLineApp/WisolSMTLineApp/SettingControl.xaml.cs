using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WisolSMTLineApp.ViewModel;

namespace WisolSMTLineApp
{
    /// <summary>
    /// Interaction logic for SettingControl.xaml
    /// </summary>
    public partial class SettingControl : UserControl
    {
        SettingViewModel SettingVM;
        public SettingControl()
        {
            InitializeComponent();
            Loaded += SettingControl_Loaded;
        }
        private void SettingControl_Loaded(object sender, RoutedEventArgs e)
        {
            SettingVM = new SettingViewModel();
            DataContext = SettingVM;
        }
    }
}
