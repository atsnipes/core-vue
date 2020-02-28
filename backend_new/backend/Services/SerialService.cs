using System;
using System.IO.Ports;
using System.Device.Gpio;

namespace backend.Services
{
    public class SerialService : ISerialService
    {
        private static bool _toggle = false;
        private readonly int _pinNum = 4;
        private GpioController _controlla;
    
        public SerialService()
        {
            _controlla = new GpioController(PinNumberingScheme.Board);
            Console.WriteLine($"created controller = {_controlla.ToString()}");
            try
            {
                _controlla.OpenPin(_pinNum);
                Console.WriteLine($"Opening to pin#{_pinNum}");

                _controlla.SetPinMode(_pinNum, PinMode.Output);
                Console.WriteLine($"Setting to pin#{_pinNum} mode to = {PinMode.Output}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception = {ex.ToString()}");
            }
            
        }

        public void readPinsStatus()
        {
/*            for(int i = 1; i < _controlla.PinCount + 1; i++)
            { */
                Console.WriteLine($"Pin {_pinNum} info:");
                Console.WriteLine($"Pin {_pinNum} IsOpen? {_controlla.IsPinOpen(_pinNum)}");
                Console.WriteLine($"Pin {_pinNum} Has Pinmode = {_controlla.GetPinMode(_pinNum)}");
           
        }

        public void write()
        {
            PinValue pinVal = _toggle ? PinValue.High : PinValue.Low;
            try
            {
                Console.WriteLine($"Pin Value = {pinVal}");
                _controlla.Write(_pinNum, pinVal);
                if(_toggle)
                {
                    _controlla.Write(_pinNum, PinValue.High);
                    Console.WriteLine($"[WROTE SHIT BITCH] - Wrote HIGH reading {_controlla.Read(_pinNum).ToString()}");
                }
                else { 
                    _controlla.Write(_pinNum, PinValue.Low);
                    Console.WriteLine($"[WROTE SHIT BITCH] - Wrote LOW reading {_controlla.Read(_pinNum).ToString()}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception = {ex.ToString()}");
            }
            _toggle = !_toggle;

        }


    }
}
