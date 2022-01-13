using Microsoft.Extensions.Logging;
using SmartBohner.ControlUnit.Abstractions;
using SmartBohner.ControlUnit.Gpio;
using System.Device.Gpio;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SmartBohner.ControlUnit.Extensions
{
    /// <summary>
    /// This class is a singleton holding all callbacks for pin-changings
    /// </summary>
    public class GpioChangeContainer : IGpioChangeContainer
    {
        private readonly int timeout = TimeSpan.FromSeconds(6).Milliseconds;

        private readonly IMaintenanceMessagingService maintenanceMessagingService;
        private readonly ILogger<GpioChangeContainer> logger;

        private GpioController controller = new();

        public GpioChangeContainer(IMaintenanceMessagingService maintenanceMessagingService, ILogger<GpioChangeContainer> logger)
        {
            this.maintenanceMessagingService = maintenanceMessagingService;
            this.logger = logger;
        }

        public void Add(int pin, MessageType messageType)
        {
            if (!controller.IsPinOpen(pin))
            {
                controller.OpenPin(pin);
            }

            controller.RegisterCallbackForPinValueChangedEvent(pin, PinEventTypes.Rising, (x, y) => RegisterPinChanged(y.PinNumber, y.ChangeType, messageType));

            //controller.RegisterCallbackForPinValueChangedEvent(pin, PinEventTypes.Rising, (x, y) => onChanged());
        }

        private void RegisterPinChanged(int pin, PinEventTypes changeType, MessageType messageType)
        {
            switch (changeType)
            {
                case PinEventTypes.None:
                    break;
                case PinEventTypes.Rising:
                    Rising(pin, PinEventType.Raising, messageType);
                    break;
                case PinEventTypes.Falling:
                    break;
            }
        }

        private void Rising(int pin, PinEventType pinEventType, MessageType messageType)
        {
            if (pinChangedUpInfo.Find(x => x.Pin == pin) is { } foundPin )
            {
                if (foundPin.HitCount >= 2)
                {
                    logger.LogInformation($"Pin {foundPin.Pin}: HitCount is {foundPin.HitCount}. Publish message");
                    maintenanceMessagingService.Publish(messageType, pinEventType);
                }
                else
                {
                    logger.LogInformation($"Pin {foundPin.Pin}: HitCount is {foundPin.HitCount}. Reseting timer");
                    foundPin.Timer.Stop();
                    foundPin.Timer.Start();
                    foundPin.HitCount++;
                }
            }
            else
            {
                logger.LogInformation("Pin changed first time. Starting timer");

                var timer = new Timer(timeout)
                {
                    Interval = timeout,
                    AutoReset = false,
                };
                timer.Elapsed += (sender, e) => TimerElapsed(pin);

                pinChangedUpInfo.Add(new PinChangedUpInfo
                {
                    Pin = pin,
                    HitCount = 1,
                    Timer =  timer
                });
            }
        }

        private void TimerElapsed(int pin)
        {
            logger.LogInformation($"Pin {pin} timed out. Reseting");
            var item = pinChangedUpInfo.First(x => x.Pin == pin);

            item.Timer.Stop();
            item.Timer.Dispose();

            pinChangedUpInfo.Remove(item);
        }

        private List<PinChangedUpInfo> pinChangedUpInfo = new List<PinChangedUpInfo>();

        public void Dispose()
        {
            controller.Dispose();
        }
    }

    class PinChangedUpInfo
    {
        public int Pin { get; set; }
        public Timer Timer { get; set; }

        public int HitCount { get; set; } = 0;
    }
}
