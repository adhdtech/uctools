using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GregoryAdam.Base.ExtensionMethods
{
	public static class Encoding
	{
		#region UTF8
		#region Char
		//======================================================================
		/// <summary>
		/// Converts a char to a utf8 byte array
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static byte[] ToUTF8(this char c)
		{
			return System.Text.Encoding.UTF8.GetBytes(new char[] { c });
		}
		//______________________________________________________________________
		/// <summary>
		/// Converts a char array to a UTF8 byte array
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static byte[] ToUTF8(this char[] c)
		{
			return System.Text.Encoding.UTF8.GetBytes(c);
		}
		//______________________________________________________________________
		/// <summary>
		/// Converts a char array to a UTF8 byte array
		/// </summary>
		/// <param name="c">Char array</param>
		/// <param name="index">starting at</param>
		/// <param name="count">number of chars</param>
		/// <returns></returns>
		public static byte[] ToUTF8(this char[] c, int index, int count)
		{
			return System.Text.Encoding.UTF8.GetBytes(c, index, count);
		}
		//______________________________________________________________________
		//======================================================================
		#endregion Char

		#region String
		/// <summary>
		/// Converts a string to a utf8 byte array
		/// </summary>
		/// <param name="s">string to convert</param>
		/// <returns></returns>
		public static byte[] ToUTF8(this string s)
		{
			return System.Text.Encoding.UTF8.GetBytes(s);
		}
		//______________________________________________________________________
		/// <summary>
		/// Converts a string to a utf8 byte array
		/// </summary>
		/// <param name="s">string to convert</param>
		/// <param name="index">start index</param>
		/// <param name="count">number of chars to convert</param>
		/// <returns></returns>
		public static byte[] ToUTF8(this string s, int index, int count)
		{
			return System.Text.Encoding.UTF8.GetBytes(s.Substring(index, count));
		}
		//______________________________________________________________________
		//======================================================================
		#endregion String

		#region Byte
		//______________________________________________________________________
		/// <summary>
		/// Converts a UTF8 byte array to string
		/// </summary>
		/// <param name="b">byte array to convert</param>
		/// <returns></returns>
		public static string FromUTF8(this byte[] b)
		{
			return System.Text.Encoding.UTF8.GetString(b);
		}
		//______________________________________________________________________
		/// <summary>
		/// Converts a UTF8 byte array to string
		/// </summary>
		/// <param name="b">byte array to convert</param>
		/// <param name="index">start index</param>
		/// <param name="count">number of bytes</param>
		/// <returns></returns>
		public static string FromUTF8(this byte[] b, int index, int count)
		{
			return System.Text.Encoding.UTF8.GetString(b, index, count);
		}
		//______________________________________________________________________
		#endregion Byte
		#endregion UTF8

	}
}
