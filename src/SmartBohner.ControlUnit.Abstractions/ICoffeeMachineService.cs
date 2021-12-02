using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Abstractions
{
    /// <summary>
    /// Provide basic info and functions to controll the coffee machine
    /// </summary>
    internal interface ICoffeeMachineService
    {
        /// <summary>
        /// Get a value whether the coffee-machine is on
        /// </summary>
        /// <returns><see langword="true"/>: if the machine is on; otherwise false</returns>
        Task<bool> IsOn();

        /// <summary>
        /// Send a signal to the coffee-machine to start up
        /// </summary>
        /// <returns></returns>
        Task Start();

        /// <summary>
        /// Send a signal to the coffee-machine to shutdown
        /// </summary>
        /// <returns></returns>
        Task Shutdown();
    }
}
