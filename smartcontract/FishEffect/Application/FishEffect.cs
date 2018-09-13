using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	public class FishEffect : SmartContract
	{

		public static BigInteger _consensusData = 0;
		public static BigInteger _randomStep = 1;

		#region Enterprise Assets
		#endregion

		#region Database Prefix

		#endregion

		public static Object Main(string operation, params object[] args)
		{
			if (Runtime.Trigger == TriggerType.Verification)
			{
				return VerifyContract(operation, args);
			}
			else if (Runtime.Trigger == TriggerType.Application)
			{
				return RunContract(operation, args);
			}

			return false;
			
		}


		#region Contract Triggers
		public static bool VerifyContract(string signatureString, params object[] args)
		{
			bool returnValue = false;
			if (AppGlobals.FishEffectScriptHash.Length == 20)
			{
				returnValue = Runtime.CheckWitness(AppGlobals.FishEffectScriptHash);
			}
			
			return returnValue;
		}

		public static object RunContract(string operation, params object[] args)
		{
			_consensusData = 0;
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
				returnValue = AquariumProcess.TransferFish(args);
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
				returnValue = AquariumProcess.FeedReef(args);
			}
			else if (operation == "getFish")
			{
				//returnValue = AquariumProcess.GetFish(args);
			}
			else if (operation == "random")
			{
				return Random();
			}

			return returnValue;
		}


		public static object Random()
		{
			if (_consensusData == 0)
			{
				_consensusData = Blockchain.GetHeader(Blockchain.GetHeight()).ConsensusData;
				//ulong teste = consensusData;
				//BigInteger novoNumbero = teste;
				//BigInteger quebrou = novoNumbero.AsByteArray().Length;
				//_consensusData = novoNumbero;
			}

			//Runtime.Log("Consensus Data:" + _consensusData);
			//Runtime.Log("Consensus Data:" + _consensusData);
			//byte[] randomBytesArray = _consensusData.AsByteArray();
			//byte[] novoByteArray = new byte[2] {0x10, 0x11};

			//novoByteArray.Concat(randomBytesArray);
			//BigInteger randomNumberByteSize = 2;
			//BigInteger randomNumber = new byte[1] { novoByteArray[(uint)_randomStep % (uint)randomNumberByteSize] }.AsBigInteger();
			//_randomStep = _randomStep + 1;

			//Runtime.Log("Random Number: " + randomNumber);
			//return randomNumber;

			//if (_randomStep < 10)
			//{
			//	Random();
			//}

			//BigInteger novoNumero = _consensusData + 10;

			return _consensusData;
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
