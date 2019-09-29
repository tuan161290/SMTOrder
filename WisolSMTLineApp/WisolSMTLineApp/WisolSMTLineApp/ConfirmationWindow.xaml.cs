using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WisolSMTLineApp.ViewModel;

namespace WisolSMTLineApp
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : UserControl, INotifyPropertyChanged
    {

        ConfirmOrderViewModel ConfirmOrderVM;
        public ConfirmationWindow()
        {
            InitializeComponent();
            Loaded += ConfirmationWindow_Loaded;
            Unloaded += ConfirmationWindow_Unloaded;
        }

        private void ConfirmationWindow_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void ConfirmationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ConfirmOrderVM = new ConfirmOrderViewModel();
            DataContext = ConfirmOrderVM;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void NotifyPropertyChanged(string ProName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ProName));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Close();
        }
    }
}
