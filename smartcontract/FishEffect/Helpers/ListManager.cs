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
			if (itemSize == 0)
	        {
		        return 0;
	        }

	        return list.Length / itemSize;
        }

		public static byte[] GetAt(byte[] list, BigInteger itemSize, BigInteger position)
		{
			BigInteger index = itemSize * position;
			return list.Range((int) index, (int) itemSize);
		}

		public static byte[] Add(byte[] list, byte[] newItem)
		{
			if (list == null)
			{
				list = new byte[0];
			}
			
			byte[] result = list.Concat(newItem);
			
			Runtime.Log(" ");
			Runtime.Log("L:item added:");
			Runtime.Log(newItem.AsString());
			Runtime.Log("L:is now:");
			Runtime.Log(result.AsString());
			Runtime.Log("------------");
			return result;
		}

		public static byte[] RemoveAt(byte[] list, BigInteger itemSize, BigInteger position)
		{
			Runtime.Log("L:item removed pos: "+position.AsByteArray().AsString());
			BigInteger start = 0;
			BigInteger middle1 = itemSize * position;
			BigInteger middle2 = (itemSize + 1) * position;
			return ByteArrHelper.InTheMiddle(list, new byte[0], start, middle1, middle2);
		}

		public static byte[] EditAt(byte[] list, BigInteger itemSize, BigInteger position, byte[] newPart)
		{
			Runtime.Log("L:item edited pos: "+position.AsByteArray().AsString() + " - value: "+ newPart.AsString());
			BigInteger start = 0;
			BigInteger middle1 = itemSize * position;
			BigInteger middle2 = (itemSize + 1) * position;
			return ByteArrHelper.InTheMiddle(list, newPart, start, middle1, middle2);
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