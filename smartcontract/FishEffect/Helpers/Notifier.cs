using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	class Notifier
	{
		[DisplayName("transfer")]
		public static event SmartContractNotification<byte[], byte[], BigInteger> NotifyTransfer;

		[DisplayName("fishAppeared")]
		public static event SmartContractNotification<byte[], byte[]> NotificationFishAppeared;

		[DisplayName("newFishBaby")]
		public static event SmartContractNotification<byte[], byte[]> NotifyNewFishBaby;

		[DisplayName("fishEaten")]
		public static event SmartContractNotification<byte[], byte[], byte[]> NotifyFishEaten;

		[DisplayName("fishForSale")]
		public static event SmartContractNotification<byte[], byte[], BigInteger> NotifyFishForSale;

		[DisplayName("fishExchanged")]
		public static event SmartContractNotification<byte[], byte[], BigInteger> NotifyFishExchanged;

		public delegate void SmartContractNotification<T, T1>(T p0, T1 p1);
		public delegate void SmartContractNotification<T, T1, T2>(T p0, T1 p1, T2 p2);

		public static void Transfer(byte[] from, byte[] to, BigInteger amount)
		{
			NotifyTransfer(from, to, amount);
		}

		public static void ImmigrantFish(byte[] reef, byte[] fish)
		{
			NotificationFishAppeared(reef, fish);
		}

		public static void NewBornFish(byte[] reef, byte[] fish)
		{
			NotifyNewFishBaby(reef, fish);
		}

		public static void FishEaten(byte[] reef, byte[] prey, byte[] preadator)
		{
			NotifyFishEaten(reef, prey, preadator);
		}

		public static void FishForSale(byte[] reef, byte[] fish, BigInteger value)
		{
			NotifyFishForSale(reef, fish, value);
		}

		public static void FishExchanged(byte[] reef, byte[] fish, BigInteger value)
		{
			NotifyFishExchanged(reef, fish, value);
		}

	}
}
