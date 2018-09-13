using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Numerics;

namespace FishEffect
{
	class ListHelper
	{
		public static byte[][] Separate(byte[] source, byte delim)
		{
			byte[] sourcePlusDelim = source.Concat(new byte[] { delim });
			byte[] step = new byte[0];
			byte[][] result = new byte[0][];
			int resultIndex = 0;

			for (int i = 0; i < sourcePlusDelim.Length; i++)
			{
				byte currentByte = sourcePlusDelim[i + 1];

				if (currentByte != delim)
				{
					step = step.Concat(new byte[] { currentByte });
				}
				else
				{
					result[resultIndex] = result[resultIndex].Concat(step);
					resultIndex++;
					step = new byte[0];
				}
			}

			return result;
		}

		public static byte[] Join(byte[][] source, byte delim)
		{
			byte[] result = new byte[0];

			for (int i = 0; i < source.Length; i++)
			{
				result = result.Concat(source[i]);
				result = result.Concat(new byte[] { delim });
			}

			return result;
		}

		public static byte[][] ConcatArray(byte[][] source, byte[] newItem)
		{
			byte[][] result = new byte[source.Length + 1][];

			for (int i = 0; i < source.Length; i++)
			{
				result[i] = source[i];
			}

			result[source.Length] = newItem;

			return result;
		}
	}
}
