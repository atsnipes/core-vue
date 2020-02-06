using System;
using System.IO.Ports;
using System.Device.Gpio;

namespace backend.Services
{
    public class SerialService 
    {
        private bool _toggle = false;
        private readonly int _pinNum = 3;
        private GpioController _controlla;
    
        public SerialService()
        {
            _controlla = new GpioController(PinNumberingScheme.Board);
            _controlla.SetPinMode(_pinNum, PinMode.Output);
        }

        public void write()
        {
            try
            {
                _controlla.Write(_pinNum, _toggle ? PinValue.High : PinValue.Low);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception = {ex.ToString()}");
            }

            _toggle = !_toggle;
        }


    }
}
