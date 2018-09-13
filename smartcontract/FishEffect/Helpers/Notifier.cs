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

		public static void ImigrantFish(byte[] aquarium, byte[] fish)
		{
			NotificationFishAppeared(aquarium, fish);
		}

		public static void NewBornFish(byte[] aquarium, byte[] fish)
		{
			NotifyNewFishBaby(aquarium, fish);
		}

		public static void FishEaten(byte[] aquarium, byte[] prey, byte[] preadator)
		{
			NotifyFishEaten(aquarium, prey, preadator);
		}

		public static void FishForSale(byte[] aquarium, byte[] fish, BigInteger value)
		{
			NotifyFishForSale(aquarium, fish, value);
		}

		public static void FishExchanged(byte[] aquarium, byte[] fish, BigInteger value)
		{
			NotifyFishExchanged(aquarium, fish, value);
		}

	}
}
