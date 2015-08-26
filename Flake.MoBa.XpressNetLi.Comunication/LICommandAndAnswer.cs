using Flake.MoBa.XpressNetLi.Comunication.Interfaces;

namespace Flake.MoBa.XpressNetLi.Comunication
{
    /// <summary>
    /// A command ans answer for a central
    /// </summary>
    public class LICommandAndAnswer
    {
        /// <summary>
        /// command to the central
        /// </summary>
        public ILICommunication Command { get; set; }

        /// <summary>
        /// answer of central
        /// </summary>
        public ILICommunication Answer { get; set; }

        /// <summary>
        /// Creates a new Command
        /// </summary>
        /// <param name="command">filled command</param>
        public LICommandAndAnswer(ILICommunication command)
        {
            Command = command;
            Answer = null;
        }
    }
}