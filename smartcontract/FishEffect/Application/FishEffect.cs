using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	/// <summary>
	/// Front Door for the Smart Contract Operations
	/// </summary>
	public class FishEffect : SmartContract
	{

		#region Enterprise Assets
		#endregion

		public static Object Main(string operation, Object[] args)
		{
			if (Runtime.Trigger == TriggerType.Verification)
			{
				return VerifyContract(operation, args);
			}
			else if (Runtime.Trigger == TriggerType.Application)
			{
                object returnValue = null;

                if (operation == "symbol")
                {
                    returnValue = Symbol();
                }
                else if (operation == "name")
                {
                    returnValue = Name();
                }
                else if (operation == "decimals")
                {
                    returnValue = Decimals();
                }

                else if (operation == "totalSupply")
                {
                    returnValue = FishCoinProcess.TotalSupply();
                }
                else if (operation == "deploy")
                {
                    returnValue = FishCoinProcess.Deploy();
                }
                else if (operation == "balanceOf")
                {
                    returnValue = FishCoinProcess.BalanceOf(args);
                }
                else if (operation == "transfer")
                {
                    returnValue = FishCoinProcess.Transfer(args);
                }
                else if (operation == "transferFish")
                {
                    returnValue = ReefProcess.TransferFish(args);
                }
                else if (operation == "kycRegister")
                {
                    returnValue = FishCoinProcess.KycRegister(args);
                }
                else if (operation == "kycStatus")
                {
                    returnValue = FishCoinProcess.KycStatus(args);
                }
                else if (operation == "feedReef")
                {
                    returnValue = ReefProcess.FeedReef(args);
                }
                else if (operation == "getReefFishesAlive")
                {
                    returnValue = ReefProcess.GetReefFishesAlive(args);
                }
                else if (operation == "getReefFishesDead")
                {
                    returnValue = ReefProcess.GetReefFishesDead(args);
                } else if (operation == "test")
                {
	                returnValue = args[0];
                }

                return returnValue;
            }

			return false;
			
		}


		#region Contract Triggers
		public static bool VerifyContract(string signatureString, object[] args)
		{
			bool returnValue = false;
			if (AppGlobals.FishEffectScriptHash.Length == 20)
			{
				returnValue = Runtime.CheckWitness(AppGlobals.FishEffectScriptHash);
			}
			
			return returnValue;
		}

		public static object RunContract(string operation, object[] args)
		{
            return true;
		}
		#endregion

		#region FishCoin NEP-5 Methods

		public static string Name()
		{
			return "Fish Coin";
		}

		public static string Symbol()
		{
			return "FISH";
		}

		public static BigInteger Decimals()
		{
			return 2;
		}

		#endregion


	}
}
