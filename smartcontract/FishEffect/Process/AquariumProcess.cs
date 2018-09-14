﻿using FishEffect.Helpers;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect
{
	class AquariumProcess
	{
		public static bool TransferFish(object[] args)
		{
			if (args.Length != 3) {
				return false;
			}
			
			byte[] from = (byte[]) args[0];
			byte[] to = (byte[])args[1];
			byte[] fishId = (byte[])args[2];

			if (!Runtime.CheckWitness(from))
			{
				return false;
			}

			bool fishRemoved = AquariumDao.RemoveFishFromAquarium(from, fishId);
			if (!fishRemoved)
			{
				return false;
			}

			AquariumDao.AddFishToAquarium(to, fishId);

			return true;
		}

		public static byte[] MintNewFish(byte[] from, BigInteger consensusData, BigInteger randomStep)
		{
			byte[] fishDna = new byte[0];
            // First 4 proprierties are random: Vegan-Carnivorous, Size, BellyType, BackType
            for (int i = 0; i < 4; i++)
			{
                BigInteger bigStepPlusI = randomStep + i;
                BigInteger randomNumber = BlockchainHelper.Random(consensusData, bigStepPlusI);
                byte randomByteValue = (byte) randomNumber;
                byte[] randomByteArray = new byte[] { randomByteValue };
                fishDna.Concat(randomByteArray);
            }
			
			
			Runtime.Log("from:");
			Runtime.Log(from.AsString());
			BigInteger fromBigInteger = from.AsBigInteger();
			// The other properties comes from the aquarium: FinType, HeadType
			for (int i = 4; i < from.Length; i++)
			{
                byte fromI = fromBigInteger.AsByteArray()[i];
				Runtime.Log("fromI:");
				Runtime.Log(new byte[] { (byte)fromI }.AsString());
                fishDna.Concat(new byte[] { (byte)fromI });
                Runtime.Log(fishDna.AsString());
            }

			return fishDna;

		}

		//public static byte[] MintFishFromParents(byte[] mother, byte[] father)
		//{

		//}

		//public static byte[] GetFish(byte[] fishDna)
		//{
		//	//fishDna = hash.Range(0, 20);
		//}

		public static bool FeedReef(object[] args)
		{
			if (args.Length != 1)
			{
				return false;
			}

			byte[] from = (byte[])args[0];

			// só eu posso alimentar meu aquario
			if (!Runtime.CheckWitness(from))
			{
				return false;
			}

			BigInteger balance = FishCoinDao.BalanceOf(from) / FishCoinProcess.DecimalsFactor;

			byte[][] fishs = AquariumDao.ListFish(from);

			// se não tem comida suficiente no estoque não irá fazer nada, mas a comida não será consumida
			if (balance < fishs.Length || balance <= 0)
			{
				return false;
			}


            //FISH DNA
            // 6 propriedades
            // Cada propriedade utilizará 1 byte como tipo
            // O valor poderá ser entre 0 a 7
            //Vegetariano 0 a 6, 7 carnivoro
            //Size
            //BellyType
            //BackType
            //TopBottomFins
            //HeadType
            //Tail
            //SideFin

            BigInteger consensusData = BlockchainHelper.GetConsensusData();
            BigInteger randomStep = BlockchainHelper.GetRandomStep();
            BigInteger aRandom = 0;

            // if there is less then 2 fishs in the aquarium
            if (fishs.Length < 2)
			{
				Runtime.Log("Menos de 2 peixes");
                //25% de chance de surgir um peixe ao acaso que estava querendo comida e achou um aquario vazio com comida fácil
                aRandom = BlockchainHelper.Random(consensusData, randomStep);
                randomStep = randomStep + 1;
                //if (aRandom % 4 == 0)
				//{
                    aRandom = BlockchainHelper.Random(consensusData, randomStep);
                    randomStep = randomStep + 1;
                    byte[] fishDna = MintNewFish(from, consensusData, randomStep);
                    randomStep = randomStep + 4;
					NewFish(from, fishDna);
					Notifier.ImigrantFish(from, fishDna);
				//}
			}

			// if there is 2 or more fishs in the aquarium
			else
			{
                //16.6% de chance de 2 peixes se acasalarem
                aRandom = BlockchainHelper.Random(consensusData, randomStep);
                randomStep = randomStep + 1;
                if (aRandom % 6 == 0)
				{
					byte[] fishDna = new byte[20];

                    // get a random fish to be the father
                    aRandom = BlockchainHelper.Random(consensusData, randomStep);
                    randomStep = randomStep + 1;
                    int indexFather = (int) (aRandom % fishs.Length);
					int indexMother;

					do
					{
                        // get a random fish to be the mother, but a different fish of the father
                        aRandom = BlockchainHelper.Random(consensusData, randomStep);
                        randomStep = randomStep + 1;
                        indexMother = (int) (aRandom % fishs.Length);
					} while (indexFather == indexMother);

					byte[] father = fishs[indexFather];
					byte[] mother = fishs[indexMother];

					for (int i = 0; i < from.Length; i++)
					{
                        // Mix DNA
                        aRandom = BlockchainHelper.Random(consensusData, randomStep);
                        randomStep = randomStep + 1;
                        if (aRandom % 2 > 0)
						{
							fishDna[i] = father[i];
						}
						else
						{
							fishDna[i] = mother[i];
						}
					}

					NewFish(from, fishDna);
					Notifier.NewBornFish(from, fishDna);

					byte fatherFishType = father[0];
					byte motherFishType = mother[0];

					// if one of the parents are carnivorous
					if ((fatherFishType == 0 || motherFishType == 0) || fishs.Length > 2)
					{
						BigInteger blockHeightWhenFatherEatAnotherFish = AquariumDao.ListBlockHeightWhenFishEatAnotherFish(father);
						BigInteger blockHeightWhenMotherEatAnotherFish = AquariumDao.ListBlockHeightWhenFishEatAnotherFish(mother);
						byte[] eater = new byte[0];

						// if has been more than 20 blocks since last time the father eat a fish
						if (fatherFishType == 0 && Blockchain.GetHeight() - blockHeightWhenFatherEatAnotherFish > 20)
						{
							eater = father;

						// if has been more than 20 blocks since last time the mother eat a fish
						} else if (motherFishType == 0 && Blockchain.GetHeight() - blockHeightWhenMotherEatAnotherFish > 20)
						{
							eater = mother;
						}

						if (eater.Length != 0)
						{
							int indexEatenFish;
							do
							{
                                // get a random fish to be eaten, but a different fish of the father and the mother
                                aRandom = BlockchainHelper.Random(consensusData, randomStep);
                                randomStep = randomStep + 1;
                                indexEatenFish = (int)(aRandom % fishs.Length);
							} while (indexEatenFish == indexMother || indexEatenFish == indexFather);

							byte[] eatenFish = fishs[indexEatenFish];

							AquariumDao.UpdateFishWasEatenBy(eatenFish, eater);
							AquariumDao.UpdateBlockHeightWhenFishEatAnotherFish(eater, Blockchain.GetHeight());
							Notifier.FishEaten(from, eatenFish, eater);
						}
					}

				}
			}

            BlockchainHelper.UpdateRandomStep(randomStep);

			return false;
		}

		//internal static object GetFishStatus(object[] args)
		//{
		//	if (args.Length != 1)
		//	{
		//		return new byte[0];
		//	}

			

		//}

		private static void NewFish(byte[] aquarium, byte[] fishId)
		{
			AquariumDao.AddFishToAquarium(aquarium, fishId);
			AquariumDao.UpdateBlockHeightWhenFishWasBorn(fishId, Blockchain.GetHeight());
		}
	}
}