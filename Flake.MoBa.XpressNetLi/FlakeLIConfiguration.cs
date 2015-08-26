using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Flake.MoBa.XpressNetLi
{
    /// <summary>
    /// Configuration class
    /// </summary>
    public class FlakeLIConfiguration
    {
        /// <summary>
        /// path to config file
        /// </summary>
        private string _Path;

        /// <summary>
        /// data class of current config
        /// </summary>
        public FlakeLIConfigData Data { get; private set; }

        /// <summary>
        /// creates a new config
        /// </summary>
        public FlakeLIConfiguration()
        {
            _Path = @"Flake.MoBa.XpressNetLi.conf";
            Data = new FlakeLIConfigData();
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
                XmlSerializer ser = new XmlSerializer(typeof(FlakeLIConfigData));
                StreamReader sr = new StreamReader(_Path);
                Data = (FlakeLIConfigData)ser.Deserialize(sr);
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
            XmlSerializer ser = new XmlSerializer(typeof(FlakeLIConfigData));
            FileStream str = new FileStream(path, FileMode.Create);
            ser.Serialize(str, Data);
            str.Close();
        }
    }
}