using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	class FishCoinDao
	{
		private static readonly byte[] _prefixOfBalances = { (byte)'A', (byte)'.' };
		private static readonly byte[] _prefixOfKnownScriptHashes = { (byte) 'B', (byte)'.' };
		private static readonly byte[] _prefixOfExchangedByScriptHash = { (byte)'C', (byte)'.' };

		private static byte[] KeyOfBalance(byte[] scriptHash)
		{
			return _prefixOfBalances.Concat(scriptHash);
		}

		private static byte[] KeyOfKyc(byte[] scriptHash)
		{
			return _prefixOfKnownScriptHashes.Concat(scriptHash);
		}

		private static byte[] KeyOfExchangedByScriptHash(byte[] scriptHash)
		{
			return _prefixOfExchangedByScriptHash.Concat(scriptHash);
		}

		private static byte[] KeyOfTotalSupply()
		{
			return "totalSupply".AsByteArray();
		}

		private static byte[] KeyOfCurrentInCirculation()
		{
			return "currentInCirculation".AsByteArray();
		}
		
		public static BigInteger GetKycStatus(byte[] scriptHash)
		{
			byte[] kycKey = KeyOfKyc(scriptHash);
			return GenericDao.Get(kycKey).AsBigInteger();
		}

		public static void UpdateKYCStatus(byte[] scriptHash, BigInteger status)
		{
			byte[] kycKey = KeyOfKyc(scriptHash);
			GenericDao.Put(kycKey, status.AsByteArray());
		}

		public static void UpdateBalance(byte[] from, BigInteger value)
		{
			byte[] balanceKey = KeyOfBalance(from);
			GenericDao.Put(balanceKey, value.AsByteArray());
		}

		public static BigInteger BalanceOf(byte[] addressScriptHash)
		{
			byte[] balanceKey = KeyOfBalance(addressScriptHash);
			BigInteger balance = GenericDao.Get(balanceKey).AsBigInteger();
			return balance;
		}

		public static void SetTotalSupply(BigInteger maximumSupply)
		{
			byte[] maximumSupplyByteArray = maximumSupply.AsByteArray();
			byte[] totalSupplyKey = KeyOfTotalSupply();
			GenericDao.Put(totalSupplyKey, maximumSupplyByteArray);
		}

		public static BigInteger GetTotalSupply()
		{
			byte[] totalSupplyKey = KeyOfTotalSupply();
			BigInteger totalSupply = GenericDao.Get(totalSupplyKey).AsBigInteger();
			return totalSupply;
		}

		public static BigInteger GetCurrentInCirculation()
		{
			byte[] currentInCirculationKey = KeyOfCurrentInCirculation();
			BigInteger currentInCirculation = GenericDao.Get(currentInCirculationKey).AsBigInteger();
			return currentInCirculation;

		}

		public static BigInteger GetExchangedGasByScriptHash(byte[] scriptHash)
		{
			byte[] exchangedKey = KeyOfExchangedByScriptHash(scriptHash);
			BigInteger exchangedValue = GenericDao.Get(exchangedKey).AsBigInteger();
			return exchangedValue;
			
		}

		internal static void UpdateExchangeByScriptHash(byte[] scriptHash, BigInteger value)
		{
			byte[] exchangedKey = KeyOfExchangedByScriptHash(scriptHash);
			GenericDao.Put(exchangedKey, value.AsByteArray());
		}
	}
}
