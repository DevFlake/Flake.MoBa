using System;
using i18n = Flake.MoBa.XpressNetLi.Base.Resources;

namespace Flake.MoBa.XpressNetLi.Base
{
    /// <summary>
    /// Representation of extended addresses
    /// </summary>
    public class HiLoAddress
    {
        /// <summary>
        /// Decimal standard address
        /// </summary>
        public int Address { get; private set; }

        /// <summary>
        /// High address as decimal
        /// </summary>
        public int Address_Hi { get; private set; }

        /// <summary>
        /// Low address as decimal
        /// </summary>
        public int Address_Lo { get; private set; }

        /// <summary>
        /// Calculate the hi and lo address
        /// </summary>
        private void CalcHiLoAddresses()
        {
            string binaryaddrress = FlakeHelper.ConvertDecimalToBinary(Address);
            string hi = binaryaddrress.Substring(0, 8);
            string lo = binaryaddrress.Substring(8, 8);
            Address_Hi = FlakeHelper.ConvertBinaryStringToDecimal(hi);
            Address_Lo = FlakeHelper.ConvertBinaryStringToDecimal(lo);
        }

        /// <summary>
        /// Create a new extended address pair from a decimal number
        /// </summary>
        /// <param name="address">decimal number (0-9999)</param>
        public HiLoAddress(int address)
        {
            if (address < 0 || address > 9999) throw new Exception(i18n.ErrorMessages.AddressNotInRange);
            Address = address;
            CalcHiLoAddresses();
        }
    }
}