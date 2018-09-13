using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	class GenericDao
	{
		
		public static readonly byte DELIM = (byte)'D';

		public static byte[] Get(byte[] key)
		{
			return Storage.Get(Storage.CurrentContext, key);
		}

		public static byte[][] List(byte[] key)
		{
			byte[] listTokenized = Get(key);
			byte[][] list = ListHelper.Separate(listTokenized, DELIM);
			return list;
		}

		public static void Put(byte[] key, byte[] value)
		{
			Storage.Put(Storage.CurrentContext, key, value);
		}

		public static void PutUsingList(byte[] key, byte[] newItem)
		{
			byte[] listTokenized = Get(key);
			byte[] listTokenizedWithNewItem = listTokenized.Concat(newItem);
			byte[] listTokenizedWithDelimiter = listTokenizedWithNewItem.Concat(new byte[] { DELIM });
			Put(key, listTokenizedWithDelimiter);
		}
	}
}
