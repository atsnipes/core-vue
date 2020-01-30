using System;
using System.IO.Ports;
using System.Text;

namespace backend.Services
{
    public class SerialService : ISerialService
    {
        private string[] _portNames;
        private SerialPort _serPort;
        private StringBuilder _sb;

        private string _port;
        public string Port
        {
            get
            {
                return _port;
            }
        }

        public SerialService(string port)
        {
            _serPort = new SerialPort();
            _port = _serPort.PortName;
            _sb = new StringBuilder();
        }

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

        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string currentLine = "";
            string Data = _serPort.ReadExisting();

            Data.Replace("\n", ""); //remove new lines

            foreach (char c in Data)
            {
                if (c == '\r')
                {
                    currentLine = _sb.ToString();
                    _sb.Clear();

                    Console.WriteLine(currentLine);
                }
                else
                {
                    _sb.Append(c);
                }
            }
        }

        public void writeSerial(string text)
        {
            //SerialPort serPort = new SerialPort("/dev/ttyACM0");
            //_serPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);

            try
            {
                text = text + "\r\n";
                Console.WriteLine($"writeSerial(): writing text = {text}");
                _serPort.Write(text);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"serPort.write({text}) failed via {ex.Message}");
            }
        }


    }
}
