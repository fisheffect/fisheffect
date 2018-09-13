using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	class AquariumDao
	{

		private static readonly byte[] _prefixOfAquariums = { (byte)'D', (byte)'.' };
		private static readonly byte[] _prefixOfAquariumFishs = { (byte)'E', (byte)'.' };
		private static readonly byte[] _prefixOfFishAquarium = { (byte)'F', (byte)'.' };
		private static readonly byte[] _prefixOfBlockHeightWhenFishWasBorn = { (byte)'G', (byte)'.' };
		private static readonly byte[] _prefixOfBlockHeightWhenFishEatAnotherFish = { (byte)'H', (byte)'.' };
		private static readonly byte[] _prefixOfFishWasEatenBy = { (byte)'I', (byte)'.' };

		
		#region KEY

		private static byte[] KeyOfAquariumFishs(byte[] scriptHash)
		{
			return _prefixOfAquariumFishs.Concat(scriptHash);
		}

		private static byte[] KeyOfFishAquarium(byte[] scriptHash)
		{
			return _prefixOfAquariumFishs.Concat(scriptHash);
		}

		private static byte[] KeyOfBlockHeightWhenFishWasBorn(byte[] scriptHash)
		{
			return _prefixOfBlockHeightWhenFishWasBorn.Concat(scriptHash);
		}

		private static byte[] KeyOfBlockHeightWhenFishEatAnotherFish(byte[] scriptHash)
		{
			return _prefixOfBlockHeightWhenFishEatAnotherFish.Concat(scriptHash);
		}

		private static byte[] KeyOfFishWasEatenBy(byte[] scriptHash)
		{
			return _prefixOfFishWasEatenBy.Concat(scriptHash);
		}

		
		
		#endregion

		#region METHODS

		public static byte[][] ListFish(byte[] from)
		{
			byte[] keyOfFromAquariumFishs = KeyOfAquariumFishs(from);
			byte[][] fromFishList = GenericDao.List(keyOfFromAquariumFishs);
			return fromFishList;
		}

		public static void AddFishToAquarium(byte[] newOwner, byte[] fish)
		{
			// put the fish in the aquarium list
			byte[] keyOfToAquariumFishs = KeyOfAquariumFishs(newOwner);
			GenericDao.PutUsingList(keyOfToAquariumFishs, fish);

			// save which aquarium the fish is at
			byte[] keyOfFishAquarium = KeyOfFishAquarium(fish);
			GenericDao.Put(keyOfFishAquarium, newOwner);
		}

		public static bool RemoveFishFromAquarium(byte[] from, byte[] fishId)
		{
			byte[][] fromFishList = ListFish(from);
			int indexOfFishToTransfer = -1;

			for (int i = 0; i < fromFishList.Length; i++)
			{
				if (fromFishList[i] == fishId)
				{
					indexOfFishToTransfer = i;
				}
			}

			if (indexOfFishToTransfer == -1)
			{
				return false;
			}

			byte[][] finalFromFishList = new byte[fromFishList.Length - 1][];

			int offset = 0;
			for (int i = 0; i < fromFishList.Length; i++)
			{
				if (i == indexOfFishToTransfer)
				{
					offset = offset + 1;
				}
				else
				{
					finalFromFishList[i - offset] = fromFishList[i];
				}
			}

			byte[] keyOfFromAquariumFishs = KeyOfAquariumFishs(from);
			GenericDao.Put(keyOfFromAquariumFishs, ListHelper.Join(finalFromFishList, GenericDao.DELIM));

			return true;
		}

		public static BigInteger GetBlockHeightWhenFishWasBorn(byte[] fish)
		{
			byte[] keyBlockHeightWhenFishWasBorn = KeyOfBlockHeightWhenFishWasBorn(fish);
			BigInteger blockHeight = GenericDao.Get(keyBlockHeightWhenFishWasBorn).AsBigInteger();
			return blockHeight;
		}

		public static void UpdateBlockHeightWhenFishWasBorn(byte[] fish, BigInteger height)
		{
			byte[] keyOfBlockHeightWhenFishWasBorn = KeyOfBlockHeightWhenFishWasBorn(fish);
			GenericDao.Put(keyOfBlockHeightWhenFishWasBorn, height.AsByteArray());
		}

		public static BigInteger ListBlockHeightWhenFishEatAnotherFish(byte[] fish)
		{
			byte[] keyBlockHeightWhenFishEatAnotherFish = KeyOfBlockHeightWhenFishEatAnotherFish(fish);
			BigInteger blockHeight = GenericDao.Get(keyBlockHeightWhenFishEatAnotherFish).AsBigInteger();
			return blockHeight;
		}

		public static void UpdateBlockHeightWhenFishEatAnotherFish(byte[] fish, BigInteger height)
		{
			byte[] keyOfBlockHeightWhenFishEatAnotherFish = KeyOfBlockHeightWhenFishEatAnotherFish(fish);
			GenericDao.PutUsingList(keyOfBlockHeightWhenFishEatAnotherFish, height.AsByteArray());
		}

		public static byte[] GetFishPreadator(byte[] fish)
		{
			byte[] keyOfEatenFish = KeyOfFishWasEatenBy(fish);
			byte[] eater = GenericDao.Get(keyOfEatenFish);
			return eater;
		}

		public static void UpdateFishWasEatenBy(byte[] fish, byte[] eater)
		{
			byte[] keyOfFishWasEatenBy = KeyOfFishWasEatenBy(fish);
			GenericDao.PutUsingList(keyOfFishWasEatenBy, eater);
		}


		#endregion
	}
}
