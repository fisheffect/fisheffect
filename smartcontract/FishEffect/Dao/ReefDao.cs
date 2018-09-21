using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	/// <summary>
	/// Responsible for Reef and Fishes Persistence
	/// </summary>
	class ReefDao
	{
		private static byte[] KeyOfReefFishesAlive(byte[] scriptHash)
		{
			return AppGlobals.PrefixOfReefFishesAlive.Concat(scriptHash);
		}
		
		private static byte[] KeyOfReefFishesDead(byte[] scriptHash)
		{
			return AppGlobals.PrefixOfReefFishesDead.Concat(scriptHash);
		}
		
		private static byte[] KeyOfBlockHeightFedReef(byte[] scriptHash)
		{
			return AppGlobals.PrefixOfBlockHeightFedReef.Concat(scriptHash);
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

		public static byte[] GetBlockHeightFedReef(byte[] reef)
		{
			byte[] keyOfBlockHeightFedReef = KeyOfBlockHeightFedReef(reef);
			return GenericDao.Get(keyOfBlockHeightFedReef);
		}

		public static void UpdateBlockHeightFedReef(byte[] reef, byte[] fish)
		{
			byte[] keyOfBlockHeightFedReef = KeyOfBlockHeightFedReef(reef);
			GenericDao.Put(keyOfBlockHeightFedReef, fish);
		}
	}
}
