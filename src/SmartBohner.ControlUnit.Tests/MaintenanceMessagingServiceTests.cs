using NUnit.Framework;
using SmartBohner.ControlUnit;
using SmartBohner.ControlUnit.Abstractions;
using System;
using System.Threading.Tasks;

namespace SmartBohner.ControlUnit.Tests
{
    [TestFixture]
    public class MaintenanceMessagingServiceTests
    {
        [Test]
        public void SubscribeShouldRegisterCallback()
        {
            var service = new MaintenanceMessagingService();

            service.Subscribe(async x => { }, MessageType.Alarm);

            Assert.That(service.Callbacks[MessageType.Alarm], Has.Count.EqualTo(1));
        }

        [Test]
        public void PublishShouldInvokeCallback()
        {
            bool called = false;
            var service = new MaintenanceMessagingService();
            service.Callbacks[MessageType.NoWater].Add(x => throw new InvalidOperationException());
            service.Callbacks[MessageType.WasteFull].Add(x => throw new InvalidOperationException());
            service.Callbacks[MessageType.NoBeans].Add(x => throw new InvalidOperationException());
            service.Callbacks[MessageType.Clean].Add(x => throw new InvalidOperationException());
            service.Callbacks[MessageType.CalcClean].Add(x => throw new InvalidOperationException());

            service.Callbacks[MessageType.Alarm].Add(async x => called = x == PinEventType.Raising);

            Assert.That(() => service.Publish(MessageType.Alarm, PinEventType.Raising), Throws.Nothing);
        }

        [Test]
        public void UnsubscribeShouldRemoveCallback()
        {
            var service = new MaintenanceMessagingService();
            service.Callbacks[MessageType.Alarm].Add(TestFunc);

            Assert.That(service.Callbacks[MessageType.Alarm], Has.Count.EqualTo(1));

            service.Unsubscribe(TestFunc);

            Assert.That(service.Callbacks[MessageType.Alarm], Is.Empty);

            Task TestFunc(PinEventType eventType)
            {
                return Task.CompletedTask;
            }
        }
    }
}
