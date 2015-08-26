using System.Collections.Generic;
using Flake.MoBa.XpressNetLi.Base;

namespace Flake.MoBa.XpressNetLi.Comunication
{
    public class CommunicationHelper
    {
        /// <summary>
        /// Calculate the checksum of a bytearray and append it to it
        /// </summary>
        /// <remarks>the first to bytes of the array will be ignored</remarks>
        public static void AddChecksumByteToArray(ref byte[] byteArray)
        {
            List<byte> ret = new List<byte>(byteArray);
            ret.Add(CalculateChecksumByteOfArray(ref byteArray));
            byteArray = ret.ToArray();
        }

        /// <summary>
        /// Calculate the checksum of a bytearray
        /// </summary>
        /// <param name="array">bytearray to calculate the checksum for</param>
        /// <param name="ignoreLeadingBytes">ignore the first two bites (LI standard)</param>
        /// <returns>returns the checksum for the given array</returns>
        private static byte CalculateChecksumByteOfArray(ref byte[] array, bool ignoreLeadingBytes = true)
        {
            return FlakeHelper.CalculateChecksumByteOfArray(array, ignoreLeadingBytes);
        }
    }
}