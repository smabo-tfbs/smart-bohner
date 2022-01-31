namespace SmartBohner.Gpio.Abstractions
{
    public interface IPinServiceFactory
    {

        /// <summary>
        /// Creates the <see cref="IPinService"/>-object
        /// </summary>
        /// <returns></returns>
        IPinService Build();


        /// <summary>
        /// Specified that a pin is opened for input-signals
        /// </summary>
        /// <param name="pin">The pin to set</param>
        /// <returns></returns>
        IPinServiceFactory WithPinAsInput(int pin);


        /// <summary>
        /// Specified that a pin is opened for input-signals
        /// </summary>
        /// <param name="pin">The pin to set</param>
        /// <param name="onChanged">The callback that gets called when the pin-state changed</param>
        /// <returns></returns>
        IPinServiceFactory WithPinAsInput(int pin, Action onChanged);

        /// <summary>
        /// Specifies that a pin is opened for output-signals
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        IPinServiceFactory WithPinAsOutput(int pin);
    }
}