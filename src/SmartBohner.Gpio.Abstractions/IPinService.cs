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

        Task<string> GetPin(int pin);
    }

    public static class Pin
    {
        public const string High = "HIGH";
        public const string Low = "LOW";
    }
}
