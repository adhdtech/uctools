using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GregoryAdam.Base.ExtensionMethods
{
	public static class HexConversion
	{
		//======================================================================
		#region ToHex

		//______________________________________________________________________
		/// <summary>
		/// Hex value of a byte
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string ToHex(this byte b)
		{
			return b.ToString("X2");
		}
		//______________________________________________________________________
		/// <summary>
		/// hex value of a byte array
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string ToHex(this byte[] b)
		{
			var sb = new StringBuilder(b.Length << 1);
			var upperBound = b.GetUpperBound(0);

			for (int i = b.GetLowerBound(0); i <= upperBound; i++)
				sb.Append(b[i].ToHex());

			return sb.ToString();
		}
		//______________________________________________________________________
		/// <summary>
		/// Hex value of s a string.  The string is converted to a utf8 byte array first
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string ToUTF8Hex(this string s)
		{
			return s.ToUTF8().ToHex();
		}
		#endregion
		//======================================================================
		#region FromHex
		/// <summary>
		/// Converts a hex string to a byte array
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static byte[] FromHex(this string s)
		{
			if (s.Length % 2 != 0)
				throw new ArgumentException("Length must be even");

			var bb = new byte[s.Length >> 1];

			for (int i = 0, j = 0; i < s.Length; i += 2, j++)
			{
				bb[j] = Byte.Parse(s.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
			}

			return bb;
		}
		//______________________________________________________________________
		/// <summary>
		/// converts a hex string to a string (via utf8 conversion)
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string FromUTF8Hex(this string s)
		{
			return s.FromHex().FromUTF8();

		}
		#endregion FromHex
		//======================================================================

	}
}
