using System;
using System.IO.Ports;

namespace backend.Services
{
    public class SerialService : ISerialService
    {
        private string[] _portNames;
        public string[] PortNames
        {
            get
            {
                _portNames = SerialPort.GetPortNames();
                Console.WriteLine("The following serial ports were found:");

                // Display each port name to the console.             
                foreach (string portName in _portNames)
                {
                    Console.WriteLine(portName);
                }

                return _portNames;
            }
        }

        public void writeSerial(string text)
        {
            SerialPort serPort = new SerialPort();

            try
            {
                serPort.Write(text);
            }
            catch(Exception ex)
            {
                //Console.WriteLine($"serPort.write({text}) failed via {ex.Message}");
            }
        }


    }
}
