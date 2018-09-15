using FishEffect.Helpers;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace FishEffect
{
	class ReefProcess
	{
		public static bool TransferFish(object[] args)
		{
			if (args.Length != 3) {
				return false;
			}
			
			byte[] from = (byte[]) args[0];
			byte[] to = (byte[])args[1];
			byte[] fishDna = (byte[])args[2];

			if (!Runtime.CheckWitness(from))
			{
				return false;
			}

			byte[] fishesOfFrom = ReefDao.GetReefFishesAlive(from);
			BigInteger indexToRemove = ListManager.IndexOfSearch(fishesOfFrom, FishManager.GetSize(), fishDna);

			if (indexToRemove == -1)
			{
				return false;
			}
			
			byte[] fishToAdd = ListManager.GetAt(fishesOfFrom, FishManager.GetSize(), indexToRemove);

			fishesOfFrom = ListManager.RemoveAt(fishesOfFrom, FishManager.GetSize(), indexToRemove);

			ReefDao.UpdateReefFishesAlive(from, fishesOfFrom);
			
			byte[] fishesOfTo = ReefDao.GetReefFishesAlive(to);
			fishesOfTo = ListManager.Add(fishesOfTo, fishToAdd);
			
			ReefDao.UpdateReefFishesAlive(to, fishesOfTo);

			return true;
		}

		public static byte[] GetReefFishesAlive(object[] args)
		{
			if (args.Length != 1)
			{
				return new byte[0];
			}

			byte[] reef = (byte[]) args[0];

			byte[] fishes = ReefDao.GetReefFishesAlive(reef);

			return fishes;
		}

		public static byte[] GetReefFishesDead(object[] args)
		{
			if (args.Length != 1)
			{
				return new byte[0];
			}

			byte[] reef = (byte[]) args[0];

			byte[] fishes = ReefDao.GetReefFishesDead(reef);

			return fishes;
		}

		public static bool FeedReef(object[] args)
		{
			if (args.Length != 1)
			{
				Runtime.Log("* No Args");
				return false;
			}

			byte[] reef = (byte[]) args[0];

			// I am the only one who can feed my reef
			if (!Runtime.CheckWitness(reef))
			{
				Runtime.Log("* Not the Owner");
				return false;
			}
			
			// variables used in the whole method
			BigInteger currentBlockHeight = BlockchainHelper.GetHeight();
			byte[] fishesAlive = ReefDao.GetReefFishesAlive(reef); // fishes alive on the reef
			byte[] fishesDead = ReefDao.GetReefFishesDead(reef); // fishes alive on the reef
			BigInteger fishesAliveLength = ListManager.GetLength(fishesAlive, FishManager.GetSize());
			
			Runtime.Log(" ");
			Runtime.Log("Fishes Alive:");
			Runtime.Log(fishesAliveLength.AsByteArray().AsString());
			Runtime.Log("------------");

			BigInteger balance = FishCoinDao.BalanceOf(reef) / FishCoinProcess.DecimalsFactor;
			
			// if there is not enough food, there is nothing to do, the food will not be consumed
			if (balance < fishesAliveLength || balance <= 0)
			{
				Runtime.Log("* Not enough food");
				return false;
			}

			// variables used to generate random
            BigInteger consensusData = BlockchainHelper.GetConsensusData();
            BigInteger randomStep = BlockchainHelper.GetRandomStep(); // a stepper to use a new random next time
            BigInteger aRandom = 0; // the random variable

            // if there is less than 2 fishes in the reef
            if (fishesAliveLength < 2)
			{
				Runtime.Log("* There is less than 2 fishes alive in the reef");
                aRandom = BlockchainHelper.Random(consensusData, randomStep);
                randomStep = randomStep + 1;
				
				// 25% probability to arrive a new fish that got interested by the food you give away
                if (aRandom % 4 == 0)
				{
					Runtime.Log("* Immigrant arrived");
					
                    byte[] newImmigrant = FishManager.BuildImmigrant(reef, consensusData, randomStep);
                    randomStep = randomStep + 4; // it was used 4 times inside the method
					
					fishesAlive = ListManager.Add(fishesAlive, newImmigrant);
					fishesAliveLength = fishesAliveLength + 1;
					Notifier.ImmigrantFish(reef, newImmigrant);
				}
                else
                {
	                Runtime.Log("* No Immigrant today");
                }
			}

			// if there is 2 or more fishes in the reef
			else
			{
				Runtime.Log("* There is 2 or more fishes in the reef");
                aRandom = BlockchainHelper.Random(consensusData, randomStep);
                randomStep = randomStep + 1;
				
				// 33.3% probability to fishes get laid
                if (aRandom % 3 == 0)
				{
					Runtime.Log("* Fishs in love");
					byte[] newBaby = new byte[0];
					
                    BigInteger indexFather;
					BigInteger indexMother;
					
					// if there is only 2 fishes they are the only possibility to mate
					if (fishesAliveLength == 2)
					{
						Runtime.Log("* Exactly 2 lovers");
						indexFather = 0;
						indexMother = 1;
					}
					else
					{
						Runtime.Log("* More than 2 possible lovers");
						// get a random fish to be the father
						aRandom = BlockchainHelper.Random(consensusData, randomStep);
						randomStep = randomStep + 1;
						
						indexFather = aRandom % fishesAliveLength;
						do
						{
							// get a random fish to be the mother, but a different fish of the father
							aRandom = BlockchainHelper.Random(consensusData, randomStep);
							randomStep = randomStep + 1;
							indexMother = aRandom % fishesAliveLength;
							
							Runtime.Log("* Looking for a pretty female");
						} while (indexFather == indexMother);
					}

					byte[] father = ListManager.GetAt(fishesAlive, FishManager.GetSize(), indexFather);
					byte[] mother =  ListManager.GetAt(fishesAlive, FishManager.GetSize(), indexMother);
							
					Runtime.Log("* Gestation");
					newBaby = FishManager.BuildFromParents(consensusData, randomStep, father, mother);
					randomStep = randomStep + father.Length; // tried random that many times
					
					Runtime.Log("* Its a Bo... Fish!");

					fishesAlive = ListManager.Add(fishesAlive, newBaby);
					
					fishesAliveLength = fishesAliveLength + 1;
					Notifier.NewBornFish(reef, newBaby);

					byte[] fatherFishType = FishManager.GetPropCarnivorous(father);
					byte[] motherFishType = FishManager.GetPropCarnivorous(mother);

					// if one of the parents are carnivorous and there is another silly fish ready to be a pray 
					if (fatherFishType[0] == 0 || motherFishType[0] == 0 || fishesAliveLength > 2)
					{
						Runtime.Log("* There is pray for the predator");
						BigInteger fathersFedWithFishBlockHeight = FishManager.GetFedWithFishBlockHeight(father).AsBigInteger();
						BigInteger mothersFedWithFishBlockHeight = FishManager.GetFedWithFishBlockHeight(mother).AsBigInteger();

						BigInteger indexPredator = -1;
						// zero means carnivorous
						// if has been more than 20 blocks since last time the father eat a fish
						if (fatherFishType[0] == 0 && currentBlockHeight - fathersFedWithFishBlockHeight > 20)
						{
							Runtime.Log("* The father is hungry");
							indexPredator = indexFather;
						}
						else if (motherFishType[0] == 0 && currentBlockHeight - mothersFedWithFishBlockHeight > 20)
						{
							Runtime.Log("* The mother is hungry");
							indexPredator = indexMother;
						}
						
						
						if (indexPredator > -1)
						{
							Runtime.Log("* It means that one of them is having a lunch");
							byte[] predator = ListManager.GetAt(fishesAlive, FishManager.GetSize(), indexPredator);
							
							BigInteger indexPrey = 0;
							do
							{
                                // get a random fish to be eaten, but a different fish of the father and the mother
                                aRandom = BlockchainHelper.Random(consensusData, randomStep);
                                randomStep = randomStep + 1;
                                indexPrey = aRandom % fishesAliveLength;
							} while (indexPrey == indexMother || indexPrey == indexFather);

							// saving the last blockheight when fish was fed with fish
							predator = FishManager.SetFedWithFishBlockHeight(predator, currentBlockHeight.AsByteArray());
							fishesAlive = ListManager.EditAt(fishesAlive, FishManager.GetSize(), indexPredator, predator);

							// saving the predator dna inside the prey
							byte[] prey =  ListManager.GetAt(fishesAlive, FishManager.GetSize(), indexPrey);
							prey = FishManager.SetPredatorDna(prey, predator);
							
							// removing the prey from alive list (after everything, to not lose the right position after removing)
							fishesAlive = ListManager.RemoveAt(fishesAlive, FishManager.GetSize(), indexPrey);
							fishesAliveLength = fishesAliveLength - 1;
							fishesDead = ListManager.Add(fishesDead, prey);
							
							Notifier.FishEaten(reef, prey, predator);
						}
						else
						{
							Runtime.Log("* The predators are not hungry");
						}
					}
					else
					{
						Runtime.Log("* No deaths today");
					}
				}
                else
                {
	                Runtime.Log("* They are not in the mood");
                }
			}
			
			Runtime.Log("* Let's go sleep");

			fishesAlive = IncrementQuantityOfFeeds(fishesAlive, fishesAliveLength);
			ReefDao.UpdateReefFishesAlive(reef, fishesAlive);
			ReefDao.UpdateReefFishesDead(reef, fishesDead);
			FishCoinDao.SetTotalSupply(balance - fishesAliveLength);

            BlockchainHelper.UpdateRandomStep(randomStep);
			
			Runtime.Log("* End :)");

			return true;
		}

		private static byte[] IncrementQuantityOfFeeds(byte[] fishes, BigInteger fishesLength)
		{
			byte[] fishesFinal = new byte[0];
			
			
			for (int i = 0; i < fishesLength; i++)
			{
				byte[] fish = ListManager.GetAt(fishes, FishManager.GetSize(), i);

				BigInteger qttOfFeds = FishManager.GetQuantityOfFeeds(fish).AsBigInteger();
				qttOfFeds = qttOfFeds + 1;
				fish = FishManager.SetQuantityOfFeeds(fish, qttOfFeds.AsByteArray());
				
				fishesFinal = ListManager.Add(fishesFinal, fish);
			}

			return fishesFinal;
		}
	}
}
