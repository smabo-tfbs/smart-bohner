namespace SmartBohner.Gpio.Abstractions
{
    /// <summary>
    /// Provide functionality to set a gpio port to a specific value
    /// </summary>
    /// <remarks>
    /// This interface is for internal using ONLY and will get removed later
    /// </remarks>
    public interface IPinService
    {
        /// <summary>
        /// Opens the provided pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        Task OpenPin(int pin);

        Task ClosePin(int pin);

        Task<PinInfo> GetPin(int pin);
    }

    public class PinInfo
    {
        public static PinInfo HighPin => new PinInfo("HIGH");
        public static PinInfo LowPin => new PinInfo("Low");

        public PinInfo(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static bool operator ==(PinInfo pin, string value)
        {
            return pin.Value == value;
        }

        public static bool operator !=(PinInfo pin, string value)
        {
            return pin.Value != value;
        }
    }
}
