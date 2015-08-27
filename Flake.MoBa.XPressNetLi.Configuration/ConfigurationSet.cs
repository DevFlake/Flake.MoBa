using System.IO;
using System.Xml.Serialization;

namespace Flake.MoBa.XPressNetLi.Configuration
{
    /// <summary>
    /// Configuration class
    /// </summary>
    public class ConfigurationSet
    {
        /// <summary>
        /// path to config file
        /// </summary>
        private string _Path;

        /// <summary>
        /// data class of current config
        /// </summary>
        public ConfigData Data { get; private set; }

        /// <summary>
        /// creates a new config
        /// </summary>
        public ConfigurationSet()
        {
            _Path = @"Flake.MoBa.XpressNetLi.conf";
            Data = new ConfigData();
            Read();

#if DEBUG
            Write(@"Flake.MoBa.XpressNetLi.conf.debug");
#endif
        }

        /// <summary>
        /// reads the config from disk
        /// </summary>
        private void Read()
        {
            FileInfo fi = new FileInfo(_Path);
            if (fi.Exists)
            {
                XmlSerializer ser = new XmlSerializer(typeof(ConfigData));
                StreamReader sr = new StreamReader(_Path);
                Data = (ConfigData)ser.Deserialize(sr);
                sr.Close();
            }
            else { Write(); }
        }

        /// <summary>
        /// writes config to disk
        /// </summary>
        /// <param name="path"></param>
        private void Write(string path = "")
        {
            if (path == string.Empty) path = _Path;
            XmlSerializer ser = new XmlSerializer(typeof(ConfigData));
            FileStream str = new FileStream(path, FileMode.Create);
            ser.Serialize(str, Data);
            str.Close();
        }
    }
}