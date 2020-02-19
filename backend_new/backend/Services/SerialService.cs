using System;
using System.IO.Ports;
using System.Device.Gpio;

namespace backend.Services
{
    public class SerialService : ISerialService
    {
        private static bool _toggle = false;
        private readonly int _pinNum = 3;
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
            for(int i = 1; i < _controlla.PinCount + 1; i++)
            { 
                Console.WriteLine($"Pin {i} info:");
                Console.WriteLine($"Pin {i} IsOpen? {_controlla.IsPinOpen(i)}");
                Console.WriteLine($"Pin {i} Has Pinmode = {_controlla.GetPinMode(i)}");
            }
        }

        public void write()
        {
            try
            {
                _controlla.Write(_pinNum, _toggle ? PinValue.High : PinValue.Low);
                Console.WriteLine($"Wrote pin#{_pinNum} = {_toggle}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception = {ex.ToString()}");
            }
            _toggle = !_toggle;

        }


    }
}
