using System;
using System.Collections.Generic;
using i18n = Flake.MoBa.XpressNetLi.Base.Resources;
using logme = Flake.MoBa.Log.FlakeLog;

namespace Flake.MoBa.XpressNetLi.Base
{
    /// <summary>
    /// Static class with global helper functions
    /// </summary>
    public static class FlakeHelper
    {
        /// <summary>
        /// Shifts the left bit out of a bitarray and puts it to the rigth end
        /// </summary>
        /// <param name="bitArray">array of bits</param>
        /// <param name="shiftValue">the count of digits to shift</param>
        /// <param name="shiftLeft">set true to shift left, false to shift right</param>
        /// <returns>returns a new bitarray</returns>
        public static string ShiftArray(string bitArray, int shiftValue = 1, bool shiftLeft = true)
        {
            if (bitArray.Length == 0 || bitArray.Length == 1) return bitArray;

            string ret = bitArray;
            for (int i = 0; i < shiftValue; i++)
            {
                if (shiftLeft)
                {
                    ret = ret.Substring(1, ret.Length - 1) + ret[0];
                }
                else
                {
                    ret = ret[ret.Length - 1] + ret.Substring(0, ret.Length - 1);
                }
            }
            return ret;
        }

        /// <summary>
        /// Gets the bitwise representation of 2 digit hex string
        /// </summary>
        /// <param name="value">hex string (2 digits)</param>
        /// <param name="UseLittleEndian">select direction of bits</param>
        /// <returns>bool-array with lenght of 8</returns>
        public static bool[] GetBitsOfHexByte(string value, bool UseLittleEndian = true)
        {
            if (value.Length != 2) throw new Exception(i18n.ErrorMessages.HexNot2Digits);
            if (!IsStringHexFormat(value)) throw new Exception(i18n.ErrorMessages.NoHexSign);
            List<bool> ret = new List<bool>();
            string binary = ConvertHexToBinary(value);
            while (binary.Length < 8) { binary = "0" + binary; } // add leading zeros
            foreach (char c in binary) { ret.Add(c == '1'); } // convert to bool-list
            if (UseLittleEndian) ret.Reverse();
            return ret.ToArray();
        }

        /// <summary>
        /// Converts hex string to binary string
        /// </summary>
        /// <param name="value">hex string</param>
        /// <returns>representing bitvalue</returns>
        /// <remarks>big endian</remarks>
        private static string ConvertHexToBinary(string value)
        {
            if (!IsStringHexFormat(value)) throw new Exception(i18n.ErrorMessages.NoHexSign);
            string ret = string.Empty;
            ret = Convert.ToString(Convert.ToInt32(value, 16), 2);
            return ret;
        }

        /// <summary>
        /// Converts decimal to binary string
        /// </summary>
        /// <param name="value">deicmal value</param>
        /// <param name="length">fill string with leading zeros to this lenght</param>
        /// <returns>representing bitvalue</returns>
        public static string ConvertDecimalToBinary(int value, int length = 16)
        {
            string ret = string.Empty;
            ret = Convert.ToString(value, 2);
            while (ret.Length < length) { ret = "0" + ret; } // add leading zeros
            return ret;
        }

        /// <summary>
        /// Converts binary string to decimal
        /// </summary>
        /// <param name="value">binary value string</param>
        /// <returns>representing decimal value</returns>
        /// <remarks>awaits big endian</remarks>
        public static int ConvertBinaryStringToDecimal(string value)
        {
            int ret = 0;
            ret = Convert.ToInt32(value, 2);
            return ret;
        }

        /// <summary>
        /// Converts a 2 digit hex string to byte
        /// </summary>
        /// <param name="value">hex string (2 digits)</param>
        /// <returns>representing bytevalue</returns>
        public static byte ConvertTwoDigitHexToByte(string value)
        {
            if (value.Length != 2) throw new Exception(i18n.ErrorMessages.HexNot2Digits);
            if (!IsStringHexFormat(value)) throw new Exception(i18n.ErrorMessages.NoHexSign);
            if (value.ToLower().StartsWith("0x"))
            {
                value = value.TrimStart(new char[] { '0', 'x' });
            }
            return byte.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        /// <summary>
        /// Reverses a bitarray
        /// </summary>
        /// <param name="array">array to reverse</param>
        /// <returns>reversed bitarray</returns>
        public static string ReverseBitArray(string array)
        {
            string ret = string.Empty;
            for (int i = array.Length - 1; i > -1; i--)
            {
                ret += array[i];
            }
            return ret;
        }

        /// <summary>
        /// Get the BCD value as decimal
        /// </summary>
        /// <param name="value">byte in BCD code</param>
        /// <returns>Returns the representation of the BCD code</returns>
        public static double GetDecimalFromBCD(byte value)
        {
            try
            {
                string tmp = string.Empty;
                tmp = BitConverter.ToString(new byte[] { value });
                double ret = 0.0;
                ret = double.Parse(tmp[0] + System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + tmp[1]);
                return ret;
            }
            catch (Exception ex)
            {
                logme.Log(ex);

                return 0;
            }
        }

        /// <summary>
        /// Calculate the checksum of a bytearray
        /// </summary>
        /// <param name="array">bytearray to calculate the checksum for</param>
        /// <param name="ignoreLeadingBytes">ignore the first two bites (LI standard)</param>
        /// <returns>returns the checksum for the given array</returns>
        public static byte CalculateChecksumByteOfArray(byte[] array, bool ignoreLeadingBytes = true)
        {
            int i = 0;
            byte ret = 0;
            foreach (byte b in array)
            {
                i++;
                if (i < 3 && ignoreLeadingBytes) continue;
                ret = (ret ^= b);
            }

            return ret;
        }

        #region RegEx

        /// <summary>
        /// Regex test für hex strings
        /// </summary>
        /// <param name="value">hexnumber as string</param>
        /// <returns>true or false</returns>
        private static bool IsStringHexFormat(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z");
        }

        #endregion RegEx
    }
}