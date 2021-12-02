namespace SmartBohner.ControlUnit.Abstractions
{
    /// <summary>
    /// Provide functions to maintain the coffee-machine
    /// </summary>
    public interface IMaintenanceService
    {
        /// <summary>
        /// Get a value whether the coffee-machine needs water
        /// </summary>
        /// <returns>True: if there is water in the tank; otherwise false</returns>
        Task<bool> NoWater();

        /// <summary>
        /// Get a value whether the coffee-machines waste needs to be emptied
        /// </summary>
        /// <returns>True: if there is waste to empty; otherwise false</returns>
        Task<bool> WasteFull();

        /// <summary>
        /// Get a value whether the coffee-machines beans needs to be filled
        /// </summary>
        /// <returns>True: if the beans are empty; otherwise false</returns>
        Task<bool> NoBeans();

        /// <summary>
        /// Get a value whether the coffee-machine has other alamrs
        /// </summary>
        /// <returns>True: if there is an alarm; otherwise false</returns>
        Task<bool> Alarm();

        /// <summary>
        /// Get a value whether the coffee-machine needs a calc clean
        /// </summary>
        /// <returns>True: if there is water in the tank; otherwise false</returns>
        Task<bool> NeedsCalcClean();

        /// <summary>
        /// Executes a calc clean
        /// </summary>
        void ExecuteCalcClean();


        /// <summary>
        /// Get a value whether the coffee-machine has any warnings
        /// </summary>
        /// <returns>True: if there is any warning; otherwise false</returns>
        Task<bool> HasAny();
    }
}
