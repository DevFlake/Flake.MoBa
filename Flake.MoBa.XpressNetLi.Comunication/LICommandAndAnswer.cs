using Flake.MoBa.XpressNetLi.Entities.Interfaces;

namespace Flake.MoBa.XpressNetLi.Comunication
{
    /// <summary>
    /// A command ans answer for a central
    /// </summary>
    public class LiCommandAndAnswer : ILiCommandAndAnswer
    {
        /// <summary>
        /// command to the central
        /// </summary>
        public ILiCommunication Command { get; set; }

        /// <summary>
        /// answer of central
        /// </summary>
        public ILiCommunication Answer { get; set; }

        /// <summary>
        /// Creates a new Command
        /// </summary>
        /// <param name="command">filled command</param>
        public LiCommandAndAnswer(ILiCommunication command)
        {
            Command = command;
            Answer = null;
        }
    }
}