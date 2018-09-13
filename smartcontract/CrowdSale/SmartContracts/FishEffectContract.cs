﻿using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace CrowdSale
{
	public class FishEffectContract : SmartContract
	{

		//FishCoin is a NEP-5 
		#region Default Assets
		private const ulong neo_decimals = 100000000;
		private static readonly byte[] neo_asset_id = { 155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229, 254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };
		private static readonly byte[] gas_asset_id = { 96, 44, 121, 113, 139, 22, 228, 66, 222, 88, 119, 142, 20, 141, 11, 16, 132, 227, 178, 223, 253, 93, 230, 183, 177, 108, 238, 121, 105, 40, 45, 231 };
		#endregion

		#region FishCoin - NEP-5
		public static string Name() => "Fish Coin";
		public static string Symbol() => "FISH";
		public static readonly byte[] Owner = "ATrzHaicmhRj15C3Vv6e6gLfLqhSD2PtTr".ToScriptHash();
		public static byte Decimals() => 8;
		private static BigInteger _decimalsFactor = 10 ^ 8; //decided by Decimals()
		private static BigInteger _icoStartTime = 1506787200; //Unix Timestamp
		private static BigInteger _icoEndTime = 1538323200; //Unix Timestamp
		#endregion

		#region FishWorld
		//public static string Name() => "name of the token";
		//public static string Symbol() => "SymbolOfTheToken";
		//public static readonly byte[] Owner = "ATrzHaicmhRj15C3Vv6e6gLfLqhSD2PtTr".ToScriptHash();
		#endregion

		public delegate void MyAction<T, T1>(T p0, T1 p1);
		public delegate void MyAction<T, T1, T2>(T p0, T1 p1, T2 p2);

		//Token Settings
		//public static string Name() => "name of the token";
		//public static string Symbol() => "SymbolOfTheToken";
		//public static readonly byte[] Owner = "ATrzHaicmhRj15C3Vv6e6gLfLqhSD2PtTr".ToScriptHash();
		//public static byte Decimals() => 8;
		//private const ulong factor = 100000000; //decided by Decimals()
		//private const ulong neo_decimals = 100000000;
		private const ulong fish_coin_factor = 10000;

		//ICO Settings
		//private static readonly byte[] neo_asset_id = { 155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229, 254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };
		private static BigInteger total_amount = 100000000 * _decimalsFactor; // total token amount
		private static BigInteger pre_ico_cap = 30000000 * _decimalsFactor; // pre ico token amount
		private static BigInteger basic_rate = 1000 * _decimalsFactor;
		


		[DisplayName("transfer")]
		public static event MyAction<byte[], byte[], BigInteger> Transferred;
		[DisplayName("refund")]
		public static event MyAction<byte[], BigInteger> Refund;

		public static Object Main2(string operation, params object[] args)
		{
			if (Runtime.Trigger == TriggerType.Verification)
			{
				if (Owner.Length == 20)
				{
					// if param Owner is script hash
					return Runtime.CheckWitness(Owner);
				}
				else if (Owner.Length == 33)
				{
					// if param Owner is public key
					byte[] signature = operation.AsByteArray();
					return VerifySignature(signature, Owner);
				}
			}
			else if (Runtime.Trigger == TriggerType.Application)
			{
				if (operation == "mintAquarium")
				{
					// Minimo 2 parametros: address dono do aquario e o investimento que ele deseja fazer
					//if (args.Length != 2)
					//{
					//	return false;
					//}

					//byte[] owner = (byte[])args[0];
					//BigInteger investment = (BigInteger)args[1];
					//return MintAquarium(owner, investment);
				}
				if (operation == "deploy") return Deploy();
				if (operation == "mintTokens") return MintTokens();
				if (operation == "totalSupply") return TotalSupply();
				if (operation == "name") return Name();
				if (operation == "symbol") return Symbol();
				if (operation == "transfer")
				{
					if (args.Length != 3) return false;
					byte[] from = (byte[])args[0];
					byte[] to = (byte[])args[1];
					BigInteger value = (BigInteger)args[2];
					return Transfer(from, to, value);
				}
				if (operation == "balanceOf")
				{
					if (args.Length != 1) return 0;
					byte[] account = (byte[])args[0];
					return BalanceOf(account);
				}
				if (operation == "decimals") return Decimals();
			}
			//you can choice refund or not refund
			byte[] sender = GetSender();
			ulong contribute_value = GetContributeValue();
			if (contribute_value > 0 && sender.Length != 0)
			{
				Refund(sender, contribute_value);
			}
			return false;
		}

		// initialization parameters, only once
		// 初始化参数
		public static bool Deploy()
		{
			byte[] total_supply = Storage.Get(Storage.CurrentContext, "totalSupply");
			if (total_supply.Length != 0) return false;
			Storage.Put(Storage.CurrentContext, Owner, pre_ico_cap);
			Storage.Put(Storage.CurrentContext, "totalSupply", pre_ico_cap);
			Transferred(null, Owner, pre_ico_cap);
			return true;
		}

		// The function MintTokens is only usable by the chosen wallet
		// contract to mint a number of tokens proportional to the
		// amount of neo sent to the wallet contract. The function
		// can only be called during the tokenswap period
		// 将众筹的neo转化为等价的ico代币
		public static bool MintTokens()
		{
			byte[] sender = GetSender();
			// contribute asset is not neo
			if (sender.Length == 0)
			{
				return false;
			}
			ulong contribute_value = GetContributeValue();
			// the current exchange rate between ico tokens and neo during the token swap period
			// 获取众筹期间ico token和neo间的转化率
			ulong swap_rate = CurrentSwapRate();
			// crowdfunding failure
			// 众筹失败
			if (swap_rate == 0)
			{
				Refund(sender, contribute_value);
				return false;
			}
			// you can get current swap token amount
			ulong token = CurrentSwapToken(sender, contribute_value, swap_rate);
			if (token == 0)
			{
				return false;
			}
			// crowdfunding success
			// 众筹成功
			BigInteger balance = Storage.Get(Storage.CurrentContext, sender).AsBigInteger();
			Storage.Put(Storage.CurrentContext, sender, token + balance);
			BigInteger totalSupply = Storage.Get(Storage.CurrentContext, "totalSupply").AsBigInteger();
			Storage.Put(Storage.CurrentContext, "totalSupply", token + totalSupply);
			Transferred(null, sender, token);
			return true;
		}

		// get the total token supply
		// 获取已发行token总量
		public static BigInteger TotalSupply()
		{
			return Storage.Get(Storage.CurrentContext, "totalSupply").AsBigInteger();
		}


		public static bool MintAquarium(byte[] owner, ulong investmentValue)
		{
			if ((investmentValue % fish_coin_factor) != 0)
			{
				return false;
			}

			if (owner.Length != 20)
			{
				return false;
			}

			byte[] aquarium = Storage.Get(Storage.CurrentContext, owner);

			//Usuário só pode ter um aquario
			if (aquarium != null)
			{
				return false;
			}

			if (!Runtime.CheckWitness(owner))
			{
				return false;
			}

			//Maximum of fishs supported
			BigInteger aquariumSize = investmentValue / fish_coin_factor;

			//Informacoes do blockchain(aleatoriedade)
			Header header = Blockchain.GetHeader(Blockchain.GetHeight());
			BigInteger randomNumber = header.ConsensusData;
		    byte[] randomBytes = randomNumber.ToByteArray();

			byte[] genesisDna = new byte[20];
			genesisDna[0] = randomBytes[0];
			genesisDna[1] = randomBytes[1];
			genesisDna[19] = owner[0];
			genesisDna[20] = owner[1];

			for (int i = 2; i < genesisDna.Length - 2; i++)
			{
				byte randomByteInfluence = randomBytes[(int)(ulong)randomNumber % randomBytes.Length];
				ushort randomInfluence = randomByteInfluence;
				BigInteger dnaRandomNumber = randomInfluence;

				byte ownerByte = owner[i];
				ushort ownerInfluence = ownerByte;
				//Não é possível instanciar um big integer,
				//más é possível cria-lo usando um byte array usando a extension abaixo
				//BigInteger ownerDnaRandomNumber = new BigInteger(owner[i]);
				//BigInteger dnaResult = dnaRandomNumber * ownerDnaRandomNumber;
				//byte firstByte = dnaResult.ToByteArray()[0];
				//genesisDna[i] = firstByte;
			}

			Storage.Put(Storage.CurrentContext, owner, genesisDna);

			return true;
		}

		public static bool MintFish(byte[] owner, BigInteger investmentValue)
		{
			byte[] aquarium = Storage.Get(Storage.CurrentContext, owner);

			if (aquarium == null)
			{
				return false;
			}

			if (aquarium.Length != 20)
			{
				return false;
			}
			
			//Dos 20 bytes do aquario
			//pegamos 4 de cada ponta para fazer
			// Infulencia do DNA
			//latitude e longitude 8 bytes
			//idade do aquario 4 bytes

			//Do Blockchain
			//numero aleatorio 4 bytes
			//GetTimestamp 4 bytes
			Header blockchainHeader = Blockchain.GetHeader((Blockchain.GetHeight()));
			uint timestamp = blockchainHeader.Timestamp; // 4 bytes
			ulong numeroAleatorio = blockchainHeader.ConsensusData; // 4 bytes
			byte[] previousHash = blockchainHeader.PrevHash; // 6 bytes
			byte[] merkleRoot = blockchainHeader.MerkleRoot; // 6 bytes

			//DNA Do Sea Aquario
			//Aquario = 20 bytes
			byte[] latitudeBytes = new byte[4];
			byte[] longitudeBytes = new byte[4];

			for (int i = 0; i < 4; i++)
			{
				latitudeBytes[i] = aquarium[i];
				longitudeBytes[i] = aquarium[20 - i];
			}

			//float latitude = ConvertByteArrayToDouble(latitudeBytes);
			//float longitude = ConvertByteArrayToDouble(longitudeBytes);

			ulong eraDeNascimentoDaEspecieDoPeixe = numeroAleatorio % (ulong)300000000;
			//Nesse momento já sabemos onde o peixe nasceu e a era de origem nascimento.
			
			//Conforme o peixe vai ficando mais velho (seu dna existe a mais tempo),
			//mais importante ele é. Isso é representado por addons na cabeça do peixe.
			//A cabeça do peixe vai evoluindo conforme o tempo

			//O aquario tem que durar até 2023, depois disso não dá mais para nascer peixes.
			// Fazemos uma regra, da idade do aquario até 2023
			// A idade do peixe tem que seguir essa regra evolutiva.

			//Quanto mais velho o aquario, mais chances evoluido o peixe pode ser.
			//Ele nasce em uma era


			//Estrutura do peixe:
			//[lat,lat,lat,lat,
			// multiplicador, multiplicador, multiplicador, multiplicador
			//




			//Numero aleatório em relacao ao timestamp precisa ser balanceado
			//Resultado final precisa ser entre 0 a 300 milhoes de anos
			// Ou pode ser em percentuak
			// talvez em percentual seja mais facil
			//Idade do aquario pode definir a era
			//A era é um multiplicador




			byte[] fishDna = new byte[20];
			
			//for (int i = 1; i < genesisDna.Length - 1; i++)
			//{
			
			//}

			//Storage.Put(Storage.CurrentContext, owner, genesisDna);



			return true;
		}

		// function that is always called when someone wants to transfer tokens.
		// 流转token调用
		public static bool Transfer(byte[] from, byte[] to, BigInteger value)
		{
			if (value <= 0) return false;
			if (!Runtime.CheckWitness(from)) return false;
			if (to.Length != 20) return false;

			BigInteger from_value = Storage.Get(Storage.CurrentContext, from).AsBigInteger();
			if (from_value < value) return false;
			if (from == to) return true;
			if (from_value == value)
				Storage.Delete(Storage.CurrentContext, from);
			else
				Storage.Put(Storage.CurrentContext, from, from_value - value);
			BigInteger to_value = Storage.Get(Storage.CurrentContext, to).AsBigInteger();
			Storage.Put(Storage.CurrentContext, to, to_value + value);
			Transferred(from, to, value);
			return true;
		}

		// get the account balance of another account with address
		// 根据地址获取token的余额
		public static BigInteger BalanceOf(byte[] address)
		{
			return Storage.Get(Storage.CurrentContext, address).AsBigInteger();
		}

		// The function CurrentSwapRate() returns the current exchange rate
		// between ico tokens and neo during the token swap period
		private static ulong CurrentSwapRate()
		{
			//BigInteger icoDuration = _icoEndTime - _icoStartTime;
			//uint now = Runtime.Time;
			//int time = (int)now - icoStartTime;
			//if (time < 0)
			//{
			//	return 0;
			//}
			//else if (time < icoDuration)
			//{
			//	return basicRate;
			//}
			//else
			//{
			//	return 0;
			//}
			return 0;
		}

		//whether over contribute capacity, you can get the token amount
		private static ulong CurrentSwapToken(byte[] sender, ulong value, ulong swap_rate)
		{
			ulong token = value / neo_decimals * swap_rate;
			BigInteger total_supply = Storage.Get(Storage.CurrentContext, "totalSupply").AsBigInteger();
			BigInteger balance_token = total_amount - total_supply;
			if (balance_token <= 0)
			{
				Refund(sender, value);
				return 0;
			}
			else if (balance_token < token)
			{
				Refund(sender, (token - balance_token) / swap_rate * neo_decimals);
				token = (ulong)balance_token;
			}
			return token;
		}

		// check whether asset is neo and get sender script hash
		private static byte[] GetSender()
		{
			Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
			TransactionOutput[] reference = tx.GetReferences();
			// you can choice refund or not refund
			foreach (TransactionOutput output in reference)
			{
				if (output.AssetId == neo_asset_id) return output.ScriptHash;
			}
			return new byte[] { };
		}

		// get smart contract script hash
		private static byte[] GetReceiver()
		{
			return ExecutionEngine.ExecutingScriptHash;
		}

		// get all you contribute neo amount
		private static ulong GetContributeValue()
		{
			Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
			TransactionOutput[] outputs = tx.GetOutputs();
			ulong value = 0;
			// get the total amount of Neo
			// 获取转入智能合约地址的Neo总量
			foreach (TransactionOutput output in outputs)
			{
				if (output.ScriptHash == GetReceiver() && output.AssetId == neo_asset_id)
				{
					value += (ulong)output.Value;
				}
			}
			return value;
		}
	}
}
