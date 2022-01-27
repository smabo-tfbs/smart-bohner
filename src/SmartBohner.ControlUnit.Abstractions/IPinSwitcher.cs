using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Abstractions
{
    public interface IPinSwitcher
    {
        Task Send2Port(int port, int interval = 500);
    }
}
