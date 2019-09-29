using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WisolSMTLineApp.Model;
using static PandaApp.GPIOCommunication.GPIOHelper;

namespace WisolSMTLineApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Init();
        }

        static TimeSpan TodayDateTime { get { return TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")); } }
        static ShiftPeriod DayShift = new ShiftPeriod() { From = TimeSpan.Parse("08:00:00") };
        public static int CurrentShift
        {
            get
            {
                //TimeSpan NowTimeStamp = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
                if (TodayDateTime >= DayShift.From && TodayDateTime < DayShift.To)
                    return 1;
                else
                    return 2;
            }
        }
        public static string TodayDate { get { return DateTime.Now.ToString("yyyy-MM-dd"); } }
        private void Init()
        {
            TextHelper.InitSetting();
            var COMport = TextHelper.ReadSetting("COMPort");
            try
            {

                if (GPIOBoard.GPIOCOM == null)
                {
                    //GPIOBoard.GPIOCOM = new GPIOSerial(COMport);
                    foreach (OutputPin OutputPin in F0.OutputPins)
                    {
                        OutputPin.Board = F0;
                    }
                }
                //Loop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //try
            //{

            //}
            //catch
            //{

            //}
        }

        public static GPIOBoard F0 = new GPIOBoard(0xF0);
        CancellationTokenSource GPIOStateLoopCTS;
        public void Loop()
        {
            if (GPIOStateLoopCTS != null)
                if (!GPIOStateLoopCTS.IsCancellationRequested)
                {
                    GPIOStateLoopCTS.Cancel();
                }

            Task.Run(async () =>
            {
                try
                {
                    GPIOStateLoopCTS = new CancellationTokenSource();
                    while (true)
                    {
                        GPIOStateLoopCTS.Token.ThrowIfCancellationRequested();
                        await F0.GetGPIOsState();
                        Thread.Sleep(30);
                        //await OUT.GreenLight.SET();
                    }
                }
                catch
                {

                }

            });
        }

        public class IN
        {
            public static InputPin CountingSensor = F0.InputPins[0];
        }

        public class OUT
        {
            public static OutputPin GreenLight = F0.OutputPins[0];
            public static OutputPin RedLight = F0.OutputPins[1];
            public static OutputPin OrangeLight = F0.OutputPins[2];
        }
    }
}
