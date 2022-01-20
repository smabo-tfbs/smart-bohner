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
        private readonly int timeout = 10000;

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

            controller.RegisterCallbackForPinValueChangedEvent(pin, PinEventTypes.Rising, (x, y) => PinChanged(y.PinNumber, y.ChangeType, messageType));
        }

        private void PinChanged(int pin, PinEventTypes changeType, MessageType messageType)
        {
            if (changeType is PinEventTypes.Rising)
            {
                Rising(pin, PinEventType.Rising, messageType);
            }
            else
            {
                logger.LogInformation("Invalid changetype doing nothing");
            }
        }

        private void Rising(int pin, PinEventType pinEventType, MessageType messageType)
        {
            if (pinChangedUpInfo.Find(x => x.Pin == pin) is { } foundPin)
            {
                if (foundPin.HitCount >= 4)
                {
                    logger.LogInformation($"Pin {foundPin.Pin}: HitCount is {foundPin.HitCount}. Publish message");
                    maintenanceMessagingService.Publish(messageType, pinEventType);
                }


                logger.LogInformation($"Pin {foundPin.Pin}: HitCount is {foundPin.HitCount}. Reseting timer");
                foundPin.Timer.Stop();
                foundPin.Timer.Start();
                foundPin.HitCount++;
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
                    Timer = timer
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
