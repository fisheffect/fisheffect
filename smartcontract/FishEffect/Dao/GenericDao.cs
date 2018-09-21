using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	/// <summary>
	/// Storage I/O
	/// </summary>
	class GenericDao
	{

		public static byte[] Get(byte[] key)
		{
			byte[] res = Storage.Get(Storage.CurrentContext, key);
			Runtime.Log(" ");
			Runtime.Log("GET key: ");
			Runtime.Log(key.AsString());
			Runtime.Log("GET value: ");
			Runtime.Log(res.AsString());
			Runtime.Log("----------");

			return res;
		}

		public static void Put(byte[] key, byte[] value)
		{
			Runtime.Log(" ");
			Runtime.Log("PUT key: ");
			Runtime.Log(key.AsString());
			Runtime.Log("PUT value: ");
			Runtime.Log(value.AsString());
			Runtime.Log("----------");
			Storage.Put(Storage.CurrentContext, key, value);
		}
	}
}
