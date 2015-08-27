namespace Flake.MoBa.XpressNetLi.Entities.Interfaces
{
    /// <summary>
    /// Interface for an answer of central
    /// </summary>
    public interface ILiCommunication
    {
        /// <summary>
        /// Answer in a bytearray
        /// </summary>
        byte[] ByteArray { get; }

        /// <summary>
        /// Lenght of the internal bytearray (command)
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Name of Answertype
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Descrition of Answer
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Get a log message for this answer
        /// </summary>
        string LogMessage { get; }
    }
}