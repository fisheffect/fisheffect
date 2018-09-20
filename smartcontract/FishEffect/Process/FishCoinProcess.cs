using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.System;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;
using FishEffect.Dao;

namespace FishEffect
{
	class FishCoinProcess
	{
		public static readonly BigInteger DecimalsFactor = 100;
		public static readonly BigInteger MaxSupplyUnits = 10000000 * DecimalsFactor;
		public static readonly BigInteger MaxExchangeLimitPerRound = 500 * FishCoinPerGas;
		public static readonly BigInteger FishCoinPerGas = 500;
		public static readonly BigInteger IcoInitialBlock = 1;
		public static readonly BigInteger IcoFinalBlock = 9999999999;

		public static bool Deploy()
		{
			Runtime.Log("Deploy called");
			if (!Runtime.CheckWitness(AppGlobals.FishEffectScriptHash))
			{
				return false;
			}
			
			BigInteger totalSupply = FishCoinDao.GetTotalSupply();
			if (totalSupply != 0)
			{
				//Already deployed
				return false;
			}

			BigInteger maximumSupply = MaxSupplyUnits * DecimalsFactor;

			FishCoinDao.SetTotalSupply(maximumSupply);
			UtilityDao.UpdateRandomStep(1);

			FishCoinDao.UpdateBalance(AppGlobals.FishEffectScriptHash, maximumSupply);

			Notifier.Transfer(null, AppGlobals.FishEffectScriptHash, maximumSupply);

			Runtime.Log("Deploy ok");
			return true;

		}		

		public static bool Transfer(object[] args)
		{
			Runtime.Log("Transfer called");
			if (args.Length != 3)
			{
				return false;
			}

			byte[] from = (byte[]) args[0];

			byte[] to = (byte[]) args[1];
			BigInteger value = (BigInteger) args[2];
			if (from.Length == 0)
			{
				return false;
			}

			if (to.Length == 0)
			{
				return false;
			}

			if (!Runtime.CheckWitness(from))
			{
				return false;
			}

			BigInteger fromBalance = FishCoinDao.BalanceOf(from);
			BigInteger toBalance = FishCoinDao.BalanceOf(to);
			

			if (fromBalance < value)
			{
				return false;
			}

			BigInteger finalFromBalance = fromBalance - value;
			BigInteger finalToBalance = toBalance + value;

			FishCoinDao.UpdateBalance(from, finalFromBalance);
			FishCoinDao.UpdateBalance(to, finalToBalance);

			//Notifier.PublishTransferNotification(from, to, value);

			Runtime.Log("Transfer ok");
			return true;
		}

		public static BigInteger BalanceOf(object[] args)
		{
			if (args.Length != 1)
			{
				return -1;
			}

			byte[] ownerScriptHash = AppGlobals.FishEffectScriptHash;
			byte[] userAdressScriptHash = (byte[])args[0];
			return FishCoinDao.BalanceOf(userAdressScriptHash);
		}


		public static BigInteger TotalSupply()
		{
			Runtime.Log("Total Supply called");
			BigInteger returnValue = 0;
			byte[] totalSupplyInBytes = GenericDao.Get("totalSupply".AsByteArray());
			if (totalSupplyInBytes.Length != 0)
			{
				returnValue = totalSupplyInBytes.AsBigInteger();
			}
			Runtime.Log("total supply: "+returnValue);

			Runtime.Log("Total Supply ok");
			return returnValue;
		}

		
		public static BigInteger KycStatus(object[] args)
		{
			if (args.Length != 1)
			{
				return -1;
			}

			byte[] scriptHash = (byte[])args[0];

			return GetKycStatus(scriptHash);
		}


		public static BigInteger GetKycStatus(byte[] scriptHash)
		{
			BigInteger kycStatus = FishCoinDao.GetKycStatus(scriptHash);
			return kycStatus;
		}


		public static bool KycRegister(object[] args)
		{
			if (args.Length != 1)
			{
				return false;
			}
			
			byte[] scriptHash = (byte[])args[0];

			if (!Runtime.CheckWitness(AppGlobals.FishEffectScriptHash))
			{
				return false;
			}

			BigInteger status = (BigInteger)args[1];



			FishCoinDao.UpdateKYCStatus(scriptHash, 1);

			return true;
		}

		public bool CanExchangeGas()
		{
			return true;
		}

		public bool CalculateCanExchange(byte[] scriptHash, BigInteger amount)
		{
			BigInteger height = Blockchain.GetHeight();

			BigInteger currentInCirculation = FishCoinDao.GetCurrentInCirculation();

			BigInteger newAmount = currentInCirculation + amount;

			if (newAmount > FishCoinDao.GetTotalSupply())
			{
				return false;
			}

			if (height < IcoInitialBlock)
			{
				return false;
			}
			
			if (amount > MaxExchangeLimitPerRound)
			{
				return false;
			}

			if (height <= IcoFinalBlock)
			{
				BigInteger exchangedByThisScriptHash = FishCoinDao.GetExchangedGasByScriptHash(scriptHash);
				BigInteger finalAmount = exchangedByThisScriptHash + amount;

				if (finalAmount > MaxExchangeLimitPerRound)
				{
					return false;
				}
			}

			//Mudar de local
			//FishCoinDao.UpdateExchangeByScriptHash(scriptHash, finalAmount);
			
			return true;
		}


		//	exchange_ok = calculate_can_exchange(ctx, amount_requested, attachments[1], verify_only)

		//	return exchange_ok
	}
}
