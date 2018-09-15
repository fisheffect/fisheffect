using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Linq;
using System.Numerics;

namespace FishEffect
{
	class ListHelper
	{
		public static BigInteger GetLength(byte[] list)
		{
			Runtime.Log("GetLength list:");
			Runtime.Log(list.AsString());
			
			Runtime.Log("GetLength AppGlobals.ListDelim:");
			Runtime.Log(AppGlobals.ListDelim.AsString());
			
			BigInteger arrayLength = 0;
			for (int i = 0; i < list.Length; i++)
			{
				byte[] item = list.Range(i, AppGlobals.ListDelim.Length);
				
				Runtime.Log("GetLength item:");
				Runtime.Log(item.AsString());
				
				if (item == AppGlobals.ListDelim)
				{
					Runtime.Log("GetLength item == AppGlobals.ListDelim !!!");
					arrayLength = arrayLength + 1;
				}
			}

			return arrayLength;
		}

		public static byte[] GetAt(byte[] list, BigInteger position)
		{
			BigInteger arrayLength = 0;
			BigInteger previousDelimPosition = 0;
			byte[] returnValue = new byte[0];
			
			for (int i = 0; i < list.Length; i++)
			{
				byte[] item = list.Range(i, AppGlobals.ListDelim.Length);
				if (item == AppGlobals.ListDelim)
				{
					if (arrayLength == position)
					{
						returnValue = list.Range((int) previousDelimPosition + AppGlobals.ListDelim.Length - 1, i - (int) previousDelimPosition);
						break;
					}

					previousDelimPosition = i;
					arrayLength = arrayLength + 1;
				}
			}

			return returnValue;
		}

		public static byte[] Add(byte[] list, byte[] newItem)
		{
			byte[] result = list.Concat(newItem);
			result = result.Concat(AppGlobals.ListDelim);
			return result;
		}

		public static byte[] RemoveAt(byte[] list, BigInteger position)
		{
			BigInteger arrayLength = 0;
			BigInteger previousDelimPosition = 0;
			byte[] firstPart = new byte[0];
			byte[] remainingPart = new byte[0];

			for (int i = 0; i < list.Length; i++)
			{
				byte[] item = list.Range(i, AppGlobals.ListDelim.Length);
				if (item == AppGlobals.ListDelim)
				{
					if (arrayLength == position)
					{
						firstPart = list.Range(0, i - (int) previousDelimPosition + AppGlobals.ListDelim.Length);
						remainingPart = list.Range(i + AppGlobals.ListDelim.Length, list.Length);
						break;
					}

					previousDelimPosition = i;
					arrayLength = arrayLength + 1;
				}
			}

			return firstPart.Concat(remainingPart);
		}

		public static byte[] EditAt(byte[] list, BigInteger position, byte[] newItem)
		{
			BigInteger arrayLength = 0;
			BigInteger previousDelimPosition = 0;
			byte[] firstPart = new byte[0];
			byte[] remainingPart = new byte[0];

			for (int i = 0; i < list.Length; i++)
			{
				byte[] item = list.Range(i, AppGlobals.ListDelim.Length);
				if (item == AppGlobals.ListDelim)
				{
					if (arrayLength == position)
					{
						firstPart = list.Range(0, i - (int) previousDelimPosition + AppGlobals.ListDelim.Length);
						remainingPart = list.Range(i + AppGlobals.ListDelim.Length, list.Length);
						break;
					}

					previousDelimPosition = i;
					arrayLength = arrayLength + 1;
				}
			}

			byte[] result = firstPart.Concat(newItem);
			result = result.Concat(remainingPart);

			return result;
		}

		public static byte[] Search(byte[] list, byte[] part)
		{
			BigInteger arrayLength = 0;
			BigInteger previousDelimPosition = 0;
			BigInteger startOfResult = -1;
			byte[] returnValue = new byte[0];
			
			for (int i = 0; i < list.Length; i++)
			{
				byte[] item = list.Range(i, AppGlobals.ListDelim.Length);
				if (item == AppGlobals.ListDelim)
				{
					if (startOfResult == -1)
					{
						previousDelimPosition = i;
					}
					else
					{
						returnValue = list.Range((int) startOfResult, i - (int) startOfResult);
						break;
					}
				}
				else
				{
					byte[] partSearch = list.Range(i, part.Length);
					if (partSearch == part)
					{
						if (previousDelimPosition > -1)
						{
							startOfResult = previousDelimPosition + AppGlobals.ListDelim.Length - 1;
						}
					}
				}
			}

			return returnValue;
		}
	}
}
