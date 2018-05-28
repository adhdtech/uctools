using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GregoryAdam.Base.ExtensionMethods
{
	public static class BitRotate
	{
		#region Rotate Left
		//______________________________________________________________________
		public static UInt32 RotateLeft(this UInt32 x, int nBits)
		{
			nBits &= 0x1f;

			return (x << nBits) | (x >> (32 - nBits));

		}
		//______________________________________________________________________
		public static Int32 RotateLeft(this Int32 x, int nBits)
		{
			return (Int32)(((UInt32)x).RotateLeft(nBits));


		}
		//______________________________________________________________________
		public static UInt64 RotateLeft(this UInt64 x, int nBits)
		{
			nBits &= 0x3f;

			return (x << nBits) | (x >> (64 - nBits));
		}
		//______________________________________________________________________
		public static Int64 RotateLeft(this Int64 x, int nBits)
		{
			return (Int64)(((UInt64)x).RotateLeft(nBits));

		}
		//______________________________________________________________________		
		#endregion Rotate Left

		#region Rotate Right
		//______________________________________________________________________
		public static UInt32 RotateRight(this UInt32 x, int nBits)
		{
			nBits &= 0x1f;

			return (x >> nBits) | (x << (32 - nBits));

		}
		//______________________________________________________________________
		public static Int32 RotateRight(this Int32 x, int nBits)
		{
			return (Int32)(((UInt32)x).RotateLeft(nBits));


		}
		//______________________________________________________________________
		public static UInt64 RotateRight(this UInt64 x, int nBits)
		{
			nBits &= 0x3f;

			return (x >> nBits) | (x << (64 - nBits));
		}
		//______________________________________________________________________
		public static Int64 RotateRight(this Int64 x, int nBits)
		{
			return (Int64)(((UInt64)x).RotateLeft(nBits));

		}
		//______________________________________________________________________
		#endregion

	}
}
