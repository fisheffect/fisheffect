using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	class ReefDao
	{
		private static byte[] KeyOfReefFishesAlive(byte[] scriptHash)
		{
			return AppGlobals.PrefixOfReefFishsAlive.Concat(scriptHash);
		}

		public static byte[] GetReefFishesAlive(byte[] reef)
		{
			byte[] keyOfFromReefFishesAlive = KeyOfReefFishesAlive(reef);
			return GenericDao.Get(keyOfFromReefFishesAlive);
		}

		public static void UpdateReefFishesAlive(byte[] reef, byte[] fish)
		{
			byte[] keyOfToReefFishesAlive = KeyOfReefFishesAlive(reef);
			GenericDao.Put(keyOfToReefFishesAlive, fish);
		}
		
		private static byte[] KeyOfReefFishesDead(byte[] scriptHash)
		{
			return AppGlobals.PrefixOfReefFishsDead.Concat(scriptHash);
		}

		public static byte[] GetReefFishesDead(byte[] reef)
		{
			byte[] keyOfFromReefFishesDead = KeyOfReefFishesDead(reef);
			return GenericDao.Get(keyOfFromReefFishesDead);
		}

		public static void UpdateReefFishesDead(byte[] reef, byte[] fish)
		{
			byte[] keyOfToReefFishesDead = KeyOfReefFishesDead(reef);
			GenericDao.Put(keyOfToReefFishesDead, fish);
		}
	}
}
