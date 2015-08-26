namespace Flake.MoBa.XpressNetLi.Base.Enums.LocoFunctionRefreshMode
{
    /// <summary>
    /// Mode of locomotive function refresh
    /// </summary>
    public enum LocoFunctionRefreshMode
    {
        /// <summary>
        /// Refresh function F0 to F4
        /// </summary>
        f0tof4 = 0,
        /// <summary>
        /// Refresh function F0 to F8
        /// </summary>
        f0tof8 = 1,
        /// <summary>
        /// Refresh function F0 to F12
        /// </summary>
        f0tof12 = 3,
        /// <summary>
        /// Refresh function F0 to F20
        /// </summary>
        f0tof20 = 7,
        /// <summary>
        /// Refresh function F0 to F28
        /// </summary>
        f0tof28 = 15,
    }
}