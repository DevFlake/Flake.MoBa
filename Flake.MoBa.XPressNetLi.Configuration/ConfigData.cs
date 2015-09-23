namespace Flake.MoBa.XPressNetLi.Configuration
{
    public class ConfigData
    {
        /// <summary>
        /// How long does the central wait for an answer after sending a command
        /// </summary>
        public int TimeToWaitForLIAnswer_ms { get; set; } = 100;

        /// <summary>
        /// Timeout for a locomotive to wait for an answer of a central
        /// </summary>
        public int TimeoutForLIResponse_s { get; set; } = 3;

        /// <summary>
        /// Number of errors to ignore before stopping sending
        /// </summary>
        public int AllowedCentralErrorsInARow { get; set; } = 3;

        /// <summary>
        /// Tries for fetching central informations
        /// </summary>
        public int CentralFetchInfoTries { get; set; } = 3;

        /// <summary>
        /// Creates a new ConfigData
        /// </summary>
        public ConfigData()
        {

        }
    }
}
