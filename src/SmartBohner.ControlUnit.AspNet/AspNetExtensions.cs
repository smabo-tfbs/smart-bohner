using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SmartBohner.ControlUnit.Abstractions;
using Microsoft.Extensions.Logging;
using SmartBohner.Gpio.Abstractions;
using SmartBohner.ControlUnit.Abstractions.Contracts;

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
            var factory =  app.Services.GetService<IPinServiceFactory>();

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
            var factory = app.Services.GetService<IPinServiceFactory>();

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
            var gpioChangeContainer = app.Services.GetService<IGpioChangeContainer>();

            if (gpioChangeContainer is null)
            {
                throw new ArgumentNullException();
            }

            gpioChangeContainer.Add(1, MessageType.Espresso);
            gpioChangeContainer.Add(5, MessageType.Lungo);
            gpioChangeContainer.Add(6, MessageType.Coffee);
            gpioChangeContainer.Add(13, MessageType.CalcClean);
            gpioChangeContainer.Add(19, MessageType.Clean);
            gpioChangeContainer.Add(16, MessageType.NoWater);
            gpioChangeContainer.Add(26, MessageType.WasteFull);
            gpioChangeContainer.Add(20, MessageType.NoBeans);
            gpioChangeContainer.Add(21, MessageType.Alarm);
        }
    }
}
