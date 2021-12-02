using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit
{
    public static class ControlUnitExtensions
    {
        public static IServiceCollection RegisterControlUnit(this IServiceCollection serviceCollection)
        {

            return serviceCollection;
        }
    }
}
