namespace Flake.MoBa.XpressNetLi.Comunication.Answers
{
    /// <summary>
    /// Answer Base Class
    /// </summary>
    public class AnswerBase
    {
        /// <summary>
        /// Array which holds the real answer of central
        /// </summary>
        internal byte[] _ByteArray;

        /// <summary>
        /// Name of the answer
        /// </summary>
        internal string _Name;

        /// <summary>
        /// answer description
        /// </summary>
        internal string _Description;

        /// <summary>
        /// Array which holds the real answer of central
        /// </summary>
        public byte[] ByteArray
        {
            get { return _ByteArray; }
        }

        /// <summary>
        /// Name of the answer
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// answer description
        /// </summary>
        public string Description
        {
            get { return _Description; }
        }

        /// <summary>
        /// Lenght of bytearray
        /// </summary>
        public int Length
        {
            get { return _ByteArray.Length; }
        }

        /// <summary>
        /// indicates if this answer is a broadcast
        /// </summary>
        public bool IsBroadCast { get; private set; }

        /// <summary>
        /// Creates a answer base class
        /// </summary>
        /// <param name="name">translated name of answer class</param>
        /// <param name="description">translated description of answer class</param>
        /// <param name="isBroadcast">indicates if the answer is a broadcast</param>
        public AnswerBase(string name, string description, bool isBroadcast = false)
        {
            _Name = name;
            _Description = description;
            IsBroadCast = isBroadcast;
        }
    }
}