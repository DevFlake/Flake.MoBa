namespace Flake.MoBa.XpressNetLi.Comunication.Commands
{
    /// <summary>
    /// Base class for commands to central
    /// </summary>
    public class CommandBase
    {
        /// <summary>
        /// command as bytearray
        /// </summary>
        internal byte[] _ByteArray;

        /// <summary>
        /// Name of command
        /// </summary>
        internal string _Name;

        /// <summary>
        /// description of command
        /// </summary>
        internal string _Description;

        /// <summary>
        /// command as bytearray
        /// </summary>
        public byte[] ByteArray
        {
            get { return _ByteArray; }
            set { _ByteArray = value; }
        }

        /// <summary>
        /// Name of command
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// description of command
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        /// <summary>
        /// lenght of bytearray
        /// </summary>
        public int Length
        {
            get { return _ByteArray.Length; }
        }

        /// <summary>
        /// Creates a new command base class
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="description">a litte description</param>
        public CommandBase(string name, string description)
        {
            _Name = name;
            _Description = description;
        }
    }
}