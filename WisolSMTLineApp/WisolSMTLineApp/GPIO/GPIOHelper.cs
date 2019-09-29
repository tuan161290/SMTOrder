using PandaApp.SerialCommnunication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PandaApp.GPIOCommunication
{
    public class GPIOHelper
    {

        public class GPIOBoard
        {
            public static GPIOSerial GPIOCOM;
            private const PinValue ON = PinValue.ON;
            private const PinValue OFF = PinValue.OFF;
            SemaphoreSlim GPIOIOLock = new SemaphoreSlim(1, 1);
            private ushort _OutputRegister;
            public ushort OutputRegister
            {
                get { return _OutputRegister; }
                private set
                {
                    if (_OutputRegister != value)
                    {
                        _OutputRegister = value;
                        foreach (GPIOPin GP in OutputPins)
                        {
                            GP.PinValue = (_OutputRegister & GP.GPIOBitmask) > 0 ? PinValue.ON : PinValue.OFF;
                        }
                    }
                }
            }

            private ushort _InputRegister;
            //private readonly PropertyChangedEventArgs InputChangedEventArgs = new PropertyChangedEventArgs("InputRegister");
            public ushort InputRegister
            {
                get { return _InputRegister; }
                private set
                {
                    if (_InputRegister != value)
                    {
                        _InputRegister = value;
                        //PropertyChanged?.Invoke(this, InputChangedEventArgs);
                        foreach (GPIOPin GP in InputPins)
                        {
                            GP.PinValue = (_InputRegister & GP.GPIOBitmask) > 0 ? ON : OFF;
                        }
                    }
                }
            }

            public byte GPIOStation { get; set; }
            public ObservableCollection<InputPin> InputPins { get; set; } = new ObservableCollection<InputPin>()
            {
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO00, GPIOLabel = "IN_00", GPIODescription = "IN0" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO01, GPIOLabel = "IN_01", GPIODescription = "IN1" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO02, GPIOLabel = "IN_02", GPIODescription = "IN2" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO03, GPIOLabel = "IN_03", GPIODescription = "IN3" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO04, GPIOLabel = "IN_04", GPIODescription = "IN4" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO05, GPIOLabel = "IN_05", GPIODescription = "IN5" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO06, GPIOLabel = "IN_06", GPIODescription = "IN6" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO07, GPIOLabel = "IN_07", GPIODescription = "IN7" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO08, GPIOLabel = "IN_08", GPIODescription = "IN8" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO09, GPIOLabel = "IN_09", GPIODescription = "IN9" },
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO10, GPIOLabel = "IN_10", GPIODescription = "IN10"},
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO11, GPIOLabel = "IN_11", GPIODescription = "IN11"},
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO12, GPIOLabel = "IN_12", GPIODescription = "IN12"},
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO13, GPIOLabel = "IN_13", GPIODescription = "IN13"},
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO14, GPIOLabel = "IN_14", GPIODescription = "IN14"},
                new InputPin() { GPIOBitmask = GPIOBitmask.GPIO15, GPIOLabel = "IN_15", GPIODescription = "IN15"},
            };
            public ObservableCollection<OutputPin> OutputPins { get; set; } = new ObservableCollection<OutputPin>()
            {
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO00, GPIOLabel = "OUT_00" ,GPIODescription = "OUT0"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO01, GPIOLabel = "OUT_01", GPIODescription = "OUT1"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO02, GPIOLabel = "OUT_02" ,GPIODescription = "OUT2"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO03, GPIOLabel = "OUT_03" ,GPIODescription = "OUT3" },
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO04, GPIOLabel = "OUT_04" ,GPIODescription = "OUT4"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO05, GPIOLabel = "OUT_05" ,GPIODescription = "OUT5"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO06, GPIOLabel = "OUT_06" ,GPIODescription = "OUT6"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO07, GPIOLabel = "OUT_07" ,GPIODescription = "OUT7"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO08, GPIOLabel = "OUT_08" ,GPIODescription = "OUT8"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO09, GPIOLabel = "OUT_09" ,GPIODescription = "OUT9"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO10, GPIOLabel = "OUT_10" ,GPIODescription = "OUT10"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO11, GPIOLabel = "OUT_11" ,GPIODescription = "OUT11"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO12, GPIOLabel = "OUT_12" ,GPIODescription = "OUT12"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO13, GPIOLabel = "OUT_13" ,GPIODescription = "OUT13"},
                new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO14, GPIOLabel = "OUT_14" ,GPIODescription = "OUT14"},
                //new OutputPin() { GPIOBitmask = GPIOBitmask.GPIO15, GPIOLabel = "OUT_15" ,GPIODescription = "OUT15"},
            };
            public GPIOBoard(byte Station)
            {
                GPIOStation = Station;
            }

            public async Task RST(OutputPin Pin)
            {
                if (GPIOCOM != null)
                {
                    if ((Pin.GPIOBitmask & OutputRegister) > 0)
                    {
                        await GPIOIOLock.WaitAsync();
                        var resp = await GPIOCOM.GPIOWriteAsync(GPIOStation, Flag.GetIOInPut, null);
                        if (resp != null)
                        {
                            InputRegister = BitConverter.ToUInt16(resp, 3);
                            OutputRegister = BitConverter.ToUInt16(resp, 5);
                            OutputRegister &= (ushort)(~Pin.GPIOBitmask);
                            await GPIOCOM.GPIOWriteAsync(GPIOStation, Flag.SetIOOutput, BitConverter.GetBytes(OutputRegister));

                        }
                        GPIOIOLock.Release();
                    }
                }
            }
            public async Task SET(OutputPin Pin)
            {
                if (GPIOCOM != null)
                {
                    if ((Pin.GPIOBitmask & OutputRegister) == 0)
                    {
                        await GPIOIOLock.WaitAsync();
                        var resp = await GPIOCOM.GPIOWriteAsync(GPIOStation, Flag.GetIOInPut, null);
                        if (resp != null)
                        {
                            InputRegister = BitConverter.ToUInt16(resp, 3);
                            OutputRegister = BitConverter.ToUInt16(resp, 5);
                            OutputRegister |= (Pin.GPIOBitmask);
                            await GPIOCOM.GPIOWriteAsync(GPIOStation, Flag.SetIOOutput, BitConverter.GetBytes(OutputRegister));
                        }
                        GPIOIOLock.Release();
                    }
                }
            }
            public async Task GetGPIOsState()
            {
                if (GPIOCOM != null)
                {
                    await GPIOIOLock.WaitAsync();
                    var resp = await GPIOCOM.GPIOWriteAsync(GPIOStation, Flag.GetIOInPut, null);
                    if (resp != null)
                    {
                        InputRegister = BitConverter.ToUInt16(resp, 3);
                        OutputRegister = BitConverter.ToUInt16(resp, 5);
                    }
                    GPIOIOLock.Release();
                }
            }
            public void Close()
            {
                if (GPIOCOM != null)
                {
                    GPIOCOM.CloseDevice();
                }
            }
        }

        public class InputPin : GPIOPin
        {

        }

        public class OutputPin : GPIOPin
        {
            public GPIOBoard Board { get; set; }
            public async Task SET()
            {
                await Board.SET(this);
            }
            public async Task RST()
            {
                await Board.RST(this);
            }
        }

        public class GPIOPin : INotifyPropertyChanged
        {
            string _GPIODescription;
            public string GPIODescription
            {
                get { return _GPIODescription; }
                set
                {
                    _GPIODescription = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GPIODescription"));
                }
            }
            public string GPIOLabel { get; set; }
            public ushort GPIOBitmask { get; set; }
            private PinValue _PinValue;
            public PinValue PinValue
            {
                get
                {
                    return _PinValue;
                }
                set
                {
                    if (_PinValue != value)
                    {
                        _PinValue = value;
                        if (_PinValue == PinValue.ON)
                            NotifyPinValueChanged(Edge.Rise);
                        else
                            NotifyPinValueChanged(Edge.Fall);
                    }
                }
            }
            Stopwatch DebounceTimer = new Stopwatch();
            //public byte GPIOStation { get; set; }
            private readonly PropertyChangedEventArgs PropertyChangedEventArgs = new PropertyChangedEventArgs("PinValue");
            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, PropertyChangedEventArgs);
            }

            private readonly PinValueChangedEventArgs PinArg = new PinValueChangedEventArgs(Edge.Rise);
            private void NotifyPinValueChanged(Edge e)
            {
                // Make sure someone is listening to event
                PinArg.Edge = e;
                onPinValueChanged?.Invoke(this, PinArg);
            }

            private object _eventLock = new object();
            public delegate void PinValueChanged(object sender, PinValueChangedEventArgs e);
            public event PinValueChanged OnPinValueChanged
            {
                add
                {
                    lock (_eventLock)
                    {
                        onPinValueChanged -= value;
                        onPinValueChanged += value;
                    }
                }
                remove
                {
                    lock (_eventLock)
                        onPinValueChanged -= value;
                }
            }
            private event PinValueChanged onPinValueChanged;
            public class PinValueChangedEventArgs : EventArgs
            {
                public Edge Edge { get; set; }
                public PinValueChangedEventArgs(Edge e)
                {
                    Edge = e;
                }
            }
        }

        #region GPIO Communication method
        public class GPIOSerial : SerialHelper
        {
            private int MAXRETRY = 3;
            public GPIOSerial(string ComPort) : base(ComPort, 7, 115200, "GPIOHelper")
            {

            }
            private bool CRC16ErrorCheck(byte[] Data)
            {
                if (Data.Count() < 2)
                    return false;
                try
                {
                    int Count = Data.Length;
                    byte HighCRC = Data[Count - 1];
                    byte LowCRC = Data[Count - 2];
                    byte[] data = new byte[Count - 2];
                    Array.Copy(Data, 0, data, 0, Count - 2);
                    var CalculatedCRC = SerialHelper.CalcCRC(data);
                    return (HighCRC == CalculatedCRC[1] && LowCRC == CalculatedCRC[0]);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            private bool ParseResponde(byte SlaveID, byte FrameType, byte[] Data)
            {
                try
                {
                    int Count = Data.Count();
                    if (Count > 2)
                    {
                        if (CRC16ErrorCheck(Data))
                        {
                            return (SlaveID == Data[0] && FrameType == Data[1] && Data[2] == 0x00);
                        }
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            private SemaphoreSlim Writelock = new SemaphoreSlim(1, 1);
            private static readonly byte[] HEADER = new byte[2] { 0xAA, 0xCC };
            private static readonly byte[] TAIL = new byte[2] { 0xAA, 0xEE };
            /// <summary>
            /// Write data to GPIOBoard
            /// </summary>
            /// <param name="SlaveID">GPIOBoard Station ID</param>
            /// <param name="FrameType">Frame type</param>
            /// <param name="Data">Transmitted data</param>
            /// <returns></returns>
            public async Task<byte[]> GPIOWriteAsync(byte SlaveID, byte FrameType, byte[] Data)
            {
                try
                {
                    await Writelock.WaitAsync();
                    if (IsOpen())
                    {

                        int Retry = MAXRETRY;
                        List<byte> Frame = new List<byte> { SlaveID, FrameType };
                        if (Data != null)
                            Frame.AddRange(Data);
                        Frame.AddRange(SerialHelper.CalcCRC(Frame.ToArray()));
                        Frame.Insert(0, HEADER[0]);
                        Frame.Insert(1, HEADER[1]);
                        Frame.AddRange(TAIL);
                        while (Retry > 0)
                        {
                            await WriteAsync(Frame.ToArray());
                            byte[] ReceivedBytes;
                            ReceivedBytes = await ReadAsync();
                            if (ReceivedBytes != null)
                            {
                                if (ParseResponde(SlaveID, FrameType, ReceivedBytes))
                                    return ReceivedBytes;
                            }
                            //await Serial.WriteAsync(Frame.ToArray());
                            await Task.Delay(20);
                            DiscardInBuffer();
                            Retry--;
                        }
                        return null;
                    }
                    throw new Exception("COM Port has been closed");
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return null;
                }
                finally
                {
                    Writelock.Release();
                }
            }
        }
        #endregion
        #region Define GPIOBitmask
        public class GPIOBitmask
        {

            public const ushort GPIO00 = 1;
            public const ushort GPIO01 = 1 << 1;
            public const ushort GPIO02 = 1 << 2;
            public const ushort GPIO03 = 1 << 3;
            public const ushort GPIO04 = 1 << 4;
            public const ushort GPIO05 = 1 << 5;
            public const ushort GPIO06 = 1 << 6;
            public const ushort GPIO07 = 1 << 7;
            public const ushort GPIO08 = 1 << 8;
            public const ushort GPIO09 = 1 << 9;
            public const ushort GPIO10 = 1 << 10;
            public const ushort GPIO11 = 1 << 11;
            public const ushort GPIO12 = 1 << 12;
            public const ushort GPIO13 = 1 << 13;
            public const ushort GPIO14 = 1 << 14;
            public const ushort GPIO15 = 1 << 15;
        }
        public enum PinValue { OFF, ON }
        public enum Edge { Rise, Fall }
        public class Flag
        {
            public const byte SetIOOutput = 0x20;
            public const byte GetIOInPut = 0x22;
        }
        #endregion
    }
}
    