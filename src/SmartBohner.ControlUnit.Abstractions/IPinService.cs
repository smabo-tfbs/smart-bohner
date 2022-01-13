using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Abstractions
{
    /// <summary>
    /// Provide functionality to set a gpio port to a specific value
    /// </summary>
    /// <remarks>
    /// This interface is for internal using ONLY and will get removed later
    /// </remarks>
    public interface IPinService
    {
        /// <summary>
        /// Opens the provided pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        Task OpenPin(int pin);

        Task ClosePin(int pin);

        Task<string> GetPin(int pin);
    }
}
