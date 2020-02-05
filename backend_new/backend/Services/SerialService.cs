using RJCP.IO.Ports;
using System;

namespace backend.Services
{
    public class SerialService : ISerialService
    {
        Boolean _toggle = false;

       public PortDescription[] PortNames => SerialPortStream.GetPortDescriptions();

        public SerialService(string port)
        {

            foreach (PortDescription desc in PortNames)
            {
                Console.WriteLine("GetPortDescriptions: " + desc.Port + "; Description: " + desc.Description);
            }
            Console.WriteLine($"Trying to open port {port}");

            SerialPortStream serPort = new SerialPortStream(port, 9600);

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
