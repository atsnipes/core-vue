using System;
using System.IO.Ports;

namespace backend.Services
{
    public class SerialService 
    {
        private Boolean _toggle = false;
        private readonly string _port;
        public string[] PortNames => SerialPort.GetPortNames();
        
    
        public SerialService(string port)
        {

            Console.WriteLine($"Available ports are = {PortNames}");
            Console.WriteLine($"Trying to connect to {port}");
            _port = port;
        }

        public void write(string port, int baud)
        {
            SerialPort serPort = new SerialPort(port, baud);
            try
            {
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
            catch(Exception ex)
            {
                Console.WriteLine($"Exception = {ex.ToString()}");
            }
        }


    }
}
