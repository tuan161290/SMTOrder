using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using WisolSMTLineApp.Model;
using WisolSMTLineApp.ViewModel;

namespace WisolSMTLineApp
{
    /// <summary>
    /// Interaction logic for PlanControl.xaml
    /// </summary>
    public partial class PlanControl : UserControl
    {
        public static PlanViewModel PlanVM { get; set; }
        public PlanControl()
        {
            InitializeComponent();
            PlanVM = new PlanViewModel();           
            this.DataContext = PlanVM;
        }
    }
}
