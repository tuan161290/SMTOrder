using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static PandaApp.GPIOCommunication.GPIOHelper;

namespace WisolSMTLineApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Dispatcher MainDispatcher { get; set; }
        public static bool StartUp = true;
        public MainWindow()
        {
            MainDispatcher = this.Dispatcher;
            InitializeComponent();
            MainTabControl = MainTab;
            DataContext = this;           
        }
        public static TabControl MainTabControl;
        public static ConfirmationWindow ConfirmWindow { get; set; }

    }
}
