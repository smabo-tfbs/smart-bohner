namespace SmartBohner.ControlUnit.Abstractions
{
    public interface ISpecialActionService
    {
        /// <summary>
        /// Toggles the Aroma.
        /// </summary>
        /// <param name="newValue">The new value</param>
        /// <returns></returns>
        public Task ToggleAromaPlus(bool newValue);

        /// <summary>
        /// Sends a command to execute a calc-clean
        /// </summary>
        /// <returns></returns>
        public Task ExecuteCalcClean();

        /// <summary>
        /// Sends a command to the controller to execute a steam run.
        /// </summary>
        /// <returns></returns>
        public Task ExecuteSteam();

        /// <summary>
        /// Sends a command to the controller to execute a hot-water run.
        /// </summary>
        /// <returns></returns>
        public Task ExecuteHotWater();
    }
}
