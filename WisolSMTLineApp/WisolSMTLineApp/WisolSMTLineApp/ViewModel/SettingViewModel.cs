using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WisolSMTLineApp.Model;

namespace WisolSMTLineApp.ViewModel
{

    public class SettingViewModel : BaseViewModel
    {
        int defaultLots;
        public int DefaultLots
        {
            get { return defaultLots; }
            set
            {
                defaultLots = value;
                OnPropertyChanged(nameof(defaultLots));
            }
        }

        int defaultLevel;
        public int DefaultLevel
        {
            get { return defaultLevel; }
            set
            {
                defaultLevel = value;
                OnPropertyChanged(nameof(DefaultLevel));
            }
        }

        WorkingMode _WorkingMode;
        public WorkingMode SelectedWorkingMode
        {
            get { return _WorkingMode; }
            set
            {
                _WorkingMode = value;
                OnPropertyChanged(nameof(SelectedWorkingMode));
            }
        }

        string _LineID;
        public string LineID
        {
            get { return _LineID; }
            set
            {
                _LineID = value;
                OnPropertyChanged(nameof(LineID));
            }
        }
        public LineInfo SelectedLine { get; set; }
        public ObservableCollection<LineInfo> Lines { get; set; } = new ObservableCollection<LineInfo>();
        public SettingViewModel()
        {
            //LineID = Setting.LineID;
            DefaultLevel = Setting.DefaultLevel;
            DefaultLots = Setting.DefaultLots;
            var ListLine = Api.Controller.getLstLine();
            Lines.Clear();
            ListLine?.ForEach(x => Lines.Add(x));
            if (ListLine != null)
            {
                if (Setting.SelectedLine != null)
                    SelectedLine = ListLine.Where(x => x.ID == Setting.SelectedLine.ID).FirstOrDefault();
            }
        }

        public void Save_Click()
        {
            if (SelectedLine == null)
            {
                MessageBox.Show("Please select a Line");
                return;
            }
            Setting.DefaultLevel = DefaultLevel;
            Setting.DefaultLots = DefaultLots;
            Setting.SelectedLine = SelectedLine;
            Setting.WorkingMode = SelectedWorkingMode;

            TextHelper.WriteToSetting("SelectedProduct", JsonConvert.SerializeObject(Setting.SelectedProduct));
            TextHelper.WriteToSetting("SelectedLine", JsonConvert.SerializeObject(SelectedLine));
            TextHelper.WriteToSetting("DefaultLevel", DefaultLevel.ToString());
            TextHelper.WriteToSetting("DefaultLots", DefaultLots.ToString());
            TextHelper.WriteToSetting("WorkingMode", SelectedWorkingMode.ToString());
            TextHelper.SaveToFile();
        }

        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => Save_Click(), () => CanExecute));
            }
        }
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }
    }
}
