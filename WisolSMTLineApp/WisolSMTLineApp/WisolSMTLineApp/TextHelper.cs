using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WisolSMTLineApp.Model;

namespace WisolSMTLineApp
{
    public class TextHelper
    {
        public static async Task<string> Read(string Path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    string savedText = await sr.ReadToEndAsync();
                    return savedText;
                }
            }
            catch (System.Exception)
            {
                return string.Empty;
            }

        }

        public static async void Write(string TextToWrite)
        {
            using (StreamWriter writer = File.CreateText("Setting.txt"))
            {
                await writer.WriteAsync(TextToWrite);
            }
        }
        static object lockobject = new object();
        public static void SaveToFile()
        {
            lock (lockobject)
            {
                string txt_Setting = JsonConvert.SerializeObject(Settings);
                Write(txt_Setting);
            }
        }
        public static void WriteToSetting(string Key, string Value)
        {
            lock (lockobject)
            {
                var Setting = Settings.Where(x => x.Key == Key).FirstOrDefault();
                if (Setting == null)
                {
                    Settings.Add(new KeyValue() { Key = Key, Value = Value });
                }
                else
                {
                    Setting.Value = Value;
                }
            }
        }

        public static string ReadSetting(string Key)
        {
            var SavedSetting = Settings.Where(x => x.Key == Key).FirstOrDefault();
            if (SavedSetting != null)
            {
                return SavedSetting.Value;
            }
            return "";

        }

        public class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public static void InitSetting()
        {
            try
            {
                string txt_Setting = Read("Setting.txt").Result;
                if (txt_Setting != string.Empty)
                {
                    Settings = JsonConvert.DeserializeObject<List<KeyValue>>(txt_Setting);
                    Setting.LineID = Settings.Where(x => x.Key == "LineID").FirstOrDefault().Value;
                    Setting.COMPort = Settings.Where(x => x.Key == "COMPort").FirstOrDefault().Value;
                    Setting.WorkingMode = Settings.Where(x => x.Key == "WorkingMode").FirstOrDefault().Value == "Auto" ? WorkingMode.Auto : WorkingMode.Manual;
                    Setting.DefaultLots = int.Parse(Settings.Where(x => x.Key == "DefaultLots").FirstOrDefault().Value);
                    Setting.DefaultLevel = int.Parse(Settings.Where(x => x.Key == "DefaultLevel").FirstOrDefault().Value);

                    var SelectedLine = Settings.Where(x => x.Key == "SelectedLine").FirstOrDefault();
                    if (SelectedLine != null)
                        Setting.SelectedLine = JsonConvert.DeserializeObject<LineInfo>(SelectedLine.Value);

                    var SelectedProduct = Settings.Where(x => x.Key == "SelectedProduct").FirstOrDefault();
                    if (SelectedProduct != null)
                        Setting.SelectedProduct = JsonConvert.DeserializeObject<Product>(SelectedProduct.Value);

                }
                else
                {
                    {
                        txt_Setting = JsonConvert.SerializeObject(Settings);
                        Write(txt_Setting);
                    }
                }
            }
            catch
            {

            }
        }

        public static List<KeyValue> Settings = new List<KeyValue>()
        {
            new KeyValue(){Key = "COMPort", Value="COM7"},
            new KeyValue(){Key = "WorkingMode", Value = "0"},
            new KeyValue(){Key = "DefaultLots", Value="24"},
            new KeyValue(){Key = "DefaultLevel", Value="15"},
            new KeyValue(){Key = "LineID", Value="SMT-I"}
        };
    }

    public class Setting
    {

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        public static string COMPort { get; set; }
        public static WorkingMode workingMode;
        public static WorkingMode WorkingMode
        {
            get
            {
                return workingMode;
            }
            set
            {
                workingMode = value;
                //TextHelper.Settings.Where(x => x.Key == "WorkingMode").FirstOrDefault().Value = value.ToString();
                //TextHelper.WriteSettingToTxt("WorkingMode", workingMode.ToString());
            }
        }

        static string _LineID;
        public static string LineID
        {
            get { return _LineID; }
            set
            {
                _LineID = value;
                NotifyStaticPropertyChanged("LineID");
                //TextHelper.WriteSettingToTxt(nameof(LineID), _LineID.ToString());
            }
        }

        private static int defaultLots;
        public static int DefaultLots
        {
            get { return defaultLots; }
            set
            {
                defaultLots = value;
                //NotifyStaticPropertyChanged("DefaultLots");
                //TextHelper.WriteSettingToTxt(nameof(DefaultLots), defaultLots.ToString());
            }
        }

        private static int defaultLevel;
        public static int DefaultLevel
        {
            get { return defaultLevel; }
            set
            {
                defaultLevel = value;
                //NotifyStaticPropertyChanged("DefaultLevel");
                //TextHelper.WriteSettingToTxt("DefaultLevel", value.ToString());
            }
        }

        static int elapsedNode;
        public static int ElapsedNode
        {
            get { return elapsedNode; }
            set
            {
                elapsedNode = value;
                NotifyStaticPropertyChanged("ElapsedNode");
            }
        }
        public static LineInfo SelectedLine { get; set; }
        static Product selectedProduct;
        public static Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                NotifyStaticPropertyChanged("SelectedProduct");
                TextHelper.WriteToSetting("SelectedProduct", JsonConvert.SerializeObject(selectedProduct));
                TextHelper.SaveToFile();
            }
        }
    }

}
