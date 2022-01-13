﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Gpio
{
    internal class DebugGpioChangeController : IGpioChangeContainer
    {
        private readonly ILogger<DebugGpioChangeController> logger;

        public DebugGpioChangeController(ILogger<DebugGpioChangeController> logger)
        {
            this.logger = logger;
        }

        public void Add(int pin, Action onChanged)
        {
            logger.LogInformation($"{pin} registered for change-tracking");
        }

        public void Dispose()
        {
            logger.LogInformation($"DISPOSED {nameof(DebugGpioChangeController)}");
        }
    }
}