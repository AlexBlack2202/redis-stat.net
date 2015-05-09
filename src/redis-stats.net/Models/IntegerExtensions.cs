// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="">
//   
// </copyright>
//  <summary>
//   IntegerExtensions.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net.Models
{
    using System;
    using System.Globalization;

    /// <summary>The integer extensions.</summary>
    public static class IntegerExtensions
    {
        #region Static Fields

        /// <summary>
        ///     Prefixes for numbers
        ///     Since a long can only hold 9.2E18 (ie., exa territory) we
        ///     don't need zetta, yotta, xona, weka, vunda, uda, treda, sorta,
        ///     rinta, quexa, pepta, ocha, nena, minga, or luma
        ///     Just so you know...
        /// </summary>
        private static readonly string[] sizes = { " ", "K", "M", "G", "T", "P", "E" };

        #endregion

        #region Public Methods and Operators

        /// <summary>Format a set of bytes into a human readable format</summary>
        /// <param name="value"></param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ToReadableSIUnit(this long value)
        {
            if (value == 0)
            {
                return value.ToString(CultureInfo.InvariantCulture);
            }

            // Get the exponent
            // Since logn(x) = y means "Multiply 'n' by itself 'y' times to get 'x'",
            // the integer part of the log base 10 of any number is the exponent.
            // (This is called the "characteristic" in math parlance)
            // Since we are using longs which don't have a faractional part, this can't be negative.
            var exponent = (int)Math.Log10(value);
            var group = exponent / 3;
            if (group >= sizes.Length)
            {
                throw new ApplicationException("64 bit numbers are bigger than they should be...");
            }

            var divisor = Math.Pow(10, group * 3);
            return string.Format("{0:0.0} {1}", value / divisor, sizes[group]);
        }

        #endregion
    }
}