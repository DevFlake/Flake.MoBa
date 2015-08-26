using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flake.MoBa.Log
{
    public static class FlakeLog
    {
        // TODO Logging with listeners!
        public enum LogLevel { info = 0, warning = 1, error = 2, unexp = 3, limsg = 7 }

        public static void Log(string msg, LogLevel lvl, byte[] rawdata = null)
        {
            msg = DateTime.Now.ToString("G") + ": " + msg + " " + ((rawdata == null) ? (string.Empty) : (ByteArrayToString(rawdata)));
            Console.WriteLine(msg);
        }

        public static void AddLine(char madeof = '-', int length = 75)
        {
            StringBuilder line = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                line.Append(madeof);
            }
            Console.WriteLine(line);
        }

        private static string ByteArrayToString(byte[] value)
        {
            string ret = "[";

            string add = string.Empty;

            foreach (byte b in value)
            {
                ret += string.Format("{0:x}", b) + " ";
                add += b.ToString() + " ";
            }
            ret = ret.TrimEnd() + "] [" + add.TrimEnd() + "]";
            return ret;
        }

        public static void Log(Exception ex)
        {
            Log(ex.ToString(), LogLevel.unexp);
        }
    }
}