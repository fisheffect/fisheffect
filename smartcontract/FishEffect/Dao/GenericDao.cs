using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	class GenericDao
	{

		public static byte[] Get(byte[] key)
		{
			return Storage.Get(Storage.CurrentContext, key);
		}

		public static void Put(byte[] key, byte[] value)
		{
			Storage.Put(Storage.CurrentContext, key, value);
		}
	}
}
