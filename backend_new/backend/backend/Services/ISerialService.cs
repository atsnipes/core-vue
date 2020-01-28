namespace backend.Services
{
    interface ISerialService
    {
        string[] PortNames { get; }

        void writeSerial(string text);
    }
}
