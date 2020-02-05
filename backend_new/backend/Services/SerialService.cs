using System;
using System.IO.Ports;

namespace backend.Services
{
    public class SerialService : ISerialService
    {
        readonly Boolean _toggle = false;
        public string[] PortNames => SerialPort.GetPortNames();
    
        public SerialService(string port)
        {

            Console.WriteLine($"Trying to open port {port}");

            SerialPort serPort = new SerialPort(port, 9600);

            //SerialPortStream serPort = new SerialPortStream(port, 9600);

            if (_toggle)
            {
                serPort.WriteLine("on");
                _toggle = !_toggle;
            }
            else
            {
                serPort.WriteLine("off");
                _toggle = !_toggle;
            }

            serPort.Close();
        }
    }
}
