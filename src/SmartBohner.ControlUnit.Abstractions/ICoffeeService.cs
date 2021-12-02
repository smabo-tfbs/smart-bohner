namespace SmartBohner.ControlUnit.Abstractions
{
    public interface ICoffeeService
    {
        /// <summary>
        /// Sends a signal to the coffee-machine to cook a espresso
        /// </summary>
        Task Espresso();

        /// <summary>
        /// Sends a signal to the coffee-machine to cook a lungo
        /// </summary>
        Task Lungo();

        /// <summary>
        /// Sends a signal to the coffee-machine to cook a coffee
        /// </summary>
        Task Coffee();
    }
}
