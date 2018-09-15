using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.Linq;
using System.Numerics;

namespace FishEffect.Helpers
{
    public class ListManager
    {
        public static BigInteger GetLength(byte[] list, BigInteger itemSize)
        {
	        return list.Length / itemSize;
        }

		public static byte[] GetAt(byte[] list, BigInteger itemSize, BigInteger position)
		{
			BigInteger index = itemSize * position;
			return list.Range((int) index, (int) itemSize);
		}

		public static byte[] Add(byte[] list, byte[] newItem)
		{
			byte[] result = list.Concat(newItem);
			return result;
		}

		public static byte[] RemoveAt(byte[] list, BigInteger itemSize, BigInteger position)
		{
			BigInteger start = 0;
			BigInteger middle1 = itemSize * position;
			BigInteger middle2 = (itemSize + 1) * position;
			BigInteger end = list.Length;
			
			byte[] firstPart = list.Range((int) start, (int) middle1);
			byte[] removingItem = list.Range((int) middle1, (int) itemSize); // not used
			byte[] lastPart = list.Range((int) middle2, (int) (end - middle2));

			return firstPart.Concat(lastPart);
		}

		public static byte[] EditAt(byte[] list, BigInteger itemSize, BigInteger position, byte[] newPart)
		{
			BigInteger start = 0;
			BigInteger middle1 = itemSize * position;
			BigInteger middle2 = (itemSize + 1) * position;
			BigInteger end = list.Length;
			
			byte[] firstPart = list.Range((int) start, (int) middle1);
			byte[] removingItem = list.Range((int) middle1, (int) itemSize); // not used
			byte[] lastPart = list.Range((int) middle2, (int) (end - middle2));

			return firstPart.Concat(newPart).Concat(lastPart);
		}

		public static BigInteger IndexOfExactly(byte[] list, BigInteger itemSize, byte[] part)
		{
			BigInteger length = GetLength(list, itemSize);

			for (int i = 0; i < length; i++)
			{
				byte[] partAtI = GetAt(list, itemSize, i);
				if (partAtI == part)
				{
					return i;
				}
			}

			return -1;
		}

	    public static BigInteger IndexOfSearch(byte[] list, BigInteger itemSize, byte[] partSearch)
	    {
		    BigInteger length = GetLength(list, itemSize);

		    for (int i = 0; i < length; i++)
		    {
			    byte[] partAtI = GetAt(list, itemSize, i);

			    for (int j = 0; j < itemSize +1 -partSearch.Length; j++)
			    {
				    byte[] searchAtJ = partAtI.Range(j, partSearch.Length);
				    
				    if (searchAtJ == partSearch)
				    {
					    return i;
				    }
			    }
		    }

		    return -1;
	    }

	    public static byte[] Search(byte[] list, BigInteger itemSize, byte[] partSearch)
	    {
		    return GetAt(list, itemSize, IndexOfSearch(list, itemSize, partSearch));
	    }
    }
}