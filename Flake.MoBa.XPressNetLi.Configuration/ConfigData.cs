namespace Flake.MoBa.XPressNetLi.Configuration
{
    public class ConfigData
    {
        /// <summary>
        /// How long does the central wait for an answer after sending a command
        /// </summary>
        public int TimeToWaitForLIAnswer_ms { get; set; }

        /// <summary>
        /// Timeout for a locomotive to wait for an answer of a central
        /// </summary>
        public int TimeoutForLIResponse_s { get; set; }

        /// <summary>
        /// Number of errors to ignore before stopping sending
        /// </summary>
        public int AllowedCentralErrorsInARow { get; set; }

        /// <summary>
        /// Tries for fetching central informations
        /// </summary>
        public int CentralFetchInfoTries { get; set; }
    }
}
