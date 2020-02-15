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
            Console.WriteLine($"created controller = {_controlla.ToString()}");
            try
            {
                Console.WriteLine($"writing to pin value {_toggle}");
                _controlla.Write(_pinNum, _toggle ? PinValue.High : PinValue.Low);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception = {ex.ToString()}");
            }
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
