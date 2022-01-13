namespace SmartBohner.ControlUnit.Gpio
{
    public interface IGpioChangeContainer : IDisposable
    {
        void Add(int pin, Action onChanged);
    }
}