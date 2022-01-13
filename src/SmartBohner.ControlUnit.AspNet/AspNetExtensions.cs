using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using SmartBohner.ControlUnit.Gpio;
using Microsoft.Extensions.DependencyInjection;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Extensions;
using Microsoft.Extensions.Logging;

namespace SmartBohner.ControlUnit.AspNet
{
    public static class AspNetExtensions
    {
        public static void InitControlUnit(this WebApplication app)
        {
            app.InitButtonsInternal(); 
            app.InitDiodsInternal();
            app.InitMessagingServiceInternal();
        }

        private static void InitButtonsInternal(this WebApplication app)
        {
            var factory =  app.Services.GetService<PinServiceFactory>();

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            factory.WithPinAsOutput(4);
            factory.WithPinAsOutput(14);
            factory.WithPinAsOutput(15);
            factory.WithPinAsOutput(17);
            factory.WithPinAsOutput(18);
            factory.WithPinAsOutput(27);
            factory.WithPinAsOutput(22);
        }

        private static void InitDiodsInternal(this WebApplication app)
        {
            var factory = app.Services.GetService<PinServiceFactory>();

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            factory.WithPinAsInput(8);
            factory.WithPinAsInput(7);
            factory.WithPinAsInput(0);
            factory.WithPinAsInput(1);
            factory.WithPinAsInput(5);
            factory.WithPinAsInput(6);
            factory.WithPinAsInput(12);
            factory.WithPinAsInput(13);
            factory.WithPinAsInput(19);
            factory.WithPinAsInput(16);
            factory.WithPinAsInput(26);
            factory.WithPinAsInput(20);
            factory.WithPinAsInput(21);
        }

        private static void InitMessagingServiceInternal(this WebApplication app)
        {
            var messenger = app.Services.GetService<IMaintenanceMessagingService>();
            var gpioChangeContainer = app.Services.GetService<IGpioChangeContainer>();
            var logger = app.Services.GetService<ILogger<IMaintenanceMessagingService>>();

            if (messenger is null 
                || gpioChangeContainer is null)
            {
                throw new ArgumentNullException();
            }

            gpioChangeContainer.Add(12, () => messenger.Publish(MessageType.Alarm));
            gpioChangeContainer.Add(12, () => messenger.Publish(MessageType.CalcClean));
            gpioChangeContainer.Add(19, () => messenger.Publish(MessageType.Clean));
            gpioChangeContainer.Add(26, () => messenger.Publish(MessageType.WasteFull));
            gpioChangeContainer.Add(20, () => messenger.Publish(MessageType.NoBeans));
            gpioChangeContainer.Add(21, () => messenger.Publish(MessageType.Alarm));


            messenger.Subscribe(async () => logger.LogInformation("No water!!!"), MessageType.NoWater);

        }
    }
}
