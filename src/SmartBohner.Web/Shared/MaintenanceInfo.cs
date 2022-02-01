using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.Web.Shared
{
    public class MaintenanceInfo
    {
        public bool HasAlarm { get; init; }

        public bool NeedsCalcClean { get; init; }

        public bool HasNoBeans { get; init; }

        public bool HasNoWater { get; init; }

        public bool HasWasteFull { get; init; }
    }
}
