using RJCP.IO.Ports;

namespace backend.Services
{
    interface ISerialService
    {
        PortDescription[] PortNames { get; }
    }
}
