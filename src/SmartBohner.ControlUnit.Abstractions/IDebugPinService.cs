using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Abstractions
{
    public interface IDebugPinService
    {
        Task OpenPin(int pin);

        Task ClosePin(int pin);

        Task<string> GetPin(int pin);
    }
}
