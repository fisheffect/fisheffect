using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect.Helpers
{
    public class FishManager
    {
	    public static byte[] BuildImmigrant(byte[] reef, BigInteger consensusData, BigInteger randomStep)
	    {
		    byte[] newFish = new byte[0];
		    BigInteger endOfDnaInfluence = _indexOfDna + _sizeOfRandomDnaInfluence;
			
		    // First 4 properties are random: Vegan-Carnivorous, Size, BellyType, BackType
		    for (int i = (int) _indexOfDna; i < endOfDnaInfluence; i++)
		    {
			    BigInteger bigStepPlusI = randomStep + i;
			    BigInteger randomNumber = BlockchainHelper.Random(consensusData, bigStepPlusI);
			    byte randomByteValue = (byte) randomNumber;
			    byte[] randomByteArray = new byte[] { randomByteValue };
			    newFish = newFish.Concat(randomByteArray);
		    }

		    BigInteger blockHeight = Blockchain.GetHeight();
		
		    newFish = newFish.Concat(reef.Range((int) endOfDnaInfluence, (int) _sizeOfReefDnaInfluence));
		    newFish = newFish.Concat(blockHeight.AsByteArray()); // 4 bytes
		    byte[] quantityOfFeeds = new byte[] {0};
		    newFish = newFish.Concat(quantityOfFeeds); // how many times ate
			

		    return newFish;
	    }

	    public static byte[] BuildFromParents(BigInteger consensusData, BigInteger randomStep, byte[] father, byte[] mother)
	    {
		    byte[] fishDna = new byte[0];
			
		    BigInteger endOfDna = _indexOfDna + _sizeOfDna;
		    for (int i = (int) _indexOfDna; i < endOfDna; i++)
		    {
			    // Mix DNA
			    BigInteger aRandom = BlockchainHelper.Random(consensusData, randomStep);
			    randomStep = randomStep + 1;
			    if (aRandom % 2 > 0)
			    {
				    fishDna = fishDna.Concat(father.Range(i, 1));
			    }
			    else
			    {
				    fishDna = fishDna.Concat(mother.Range(i, 1));
			    }				
		    }

		    BigInteger blockHeight = Blockchain.GetHeight();
			
		    fishDna = fishDna.Concat(blockHeight.AsByteArray()); // 4 bytes
		    byte[] quantityOfFeeds = new byte[] {0};
		    fishDna = fishDna.Concat(quantityOfFeeds); // how many times ate

		    return fishDna;
	    }
	    
	    private static readonly BigInteger _indexOfStart = 0;
	    
	    private static readonly BigInteger _indexOfRandomDnaInfluence = _indexOfStart;
        private static readonly BigInteger _sizeOfRandomDnaInfluence = 4;

	    public static byte[] GetRandomDnaInfluence(byte[] fish)
	    {
		    return fish.Range((int) _indexOfRandomDnaInfluence, (int) _sizeOfRandomDnaInfluence);
	    }

	    public static byte[] SetRandomDnaInfluence(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfRandomDnaInfluence;
		    BigInteger middle2 = _indexOfRandomDnaInfluence + _sizeOfRandomDnaInfluence;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    private static readonly BigInteger _indexOfReefDnaInfluence = _indexOfRandomDnaInfluence + _sizeOfRandomDnaInfluence;
        private static readonly BigInteger _sizeOfReefDnaInfluence = 16;

	    public static byte[] GetReefDnaInfluence(byte[] fish)
	    {
		    return fish.Range((int) _indexOfReefDnaInfluence, (int) _sizeOfReefDnaInfluence);
	    }

	    public static byte[] SetReefDnaInfluence(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfReefDnaInfluence;
		    BigInteger middle2 = _indexOfReefDnaInfluence + _sizeOfReefDnaInfluence;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    private static readonly BigInteger _indexOfDna = _indexOfStart;
	    private static readonly BigInteger _sizeOfDna = _sizeOfRandomDnaInfluence + _sizeOfReefDnaInfluence;
	    
	    public static byte[] GetDna(byte[] fish)
	    {
		    return fish.Range((int) _indexOfDna, (int) _sizeOfDna);
	    }

	    public static byte[] SetDna(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfDna;
		    BigInteger middle2 = _indexOfDna + _sizeOfDna;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    private static readonly BigInteger _indexOfBirthBlockHeight = _indexOfDna + _sizeOfDna;
	    private static readonly BigInteger _sizeOfBirthBlockHeight = 4;
	    
	    public static byte[] GetBirthBlockHeight(byte[] fish)
	    {
		    return fish.Range((int) _indexOfBirthBlockHeight, (int) _sizeOfBirthBlockHeight);
	    }

	    public static byte[] SetBirthBlockHeight(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfBirthBlockHeight;
		    BigInteger middle2 = _indexOfBirthBlockHeight + _sizeOfBirthBlockHeight;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
		
	    private static readonly BigInteger _indexOfQuantityOfFeeds = _indexOfBirthBlockHeight + _sizeOfBirthBlockHeight;
	    private static readonly BigInteger _sizeOfQuantityOfFeeds = 1;
	    
	    public static byte[] GetQuantityOfFeeds(byte[] fish)
	    {
		    return fish.Range((int) _indexOfQuantityOfFeeds, (int) _sizeOfQuantityOfFeeds);
	    }

	    public static byte[] SetQuantityOfFeeds(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfQuantityOfFeeds;
		    BigInteger middle2 = _indexOfQuantityOfFeeds + _sizeOfQuantityOfFeeds;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
		
	    private static readonly BigInteger _indexOfFedWithFishBlockHeight = _indexOfQuantityOfFeeds + _sizeOfQuantityOfFeeds;
	    private static readonly BigInteger _sizeOfFedWithFishBlockHeight = 4;
	    
	    public static byte[] GetFedWithFishBlockHeight(byte[] fish)
	    {
		    return fish.Range((int) _indexOfFedWithFishBlockHeight, (int) _sizeOfFedWithFishBlockHeight);
	    }

	    public static byte[] SetFedWithFishBlockHeight(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfFedWithFishBlockHeight;
		    BigInteger middle2 = _indexOfFedWithFishBlockHeight + _sizeOfFedWithFishBlockHeight;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
		
	    private static readonly BigInteger _indexOfPredatorDna = _indexOfFedWithFishBlockHeight + _sizeOfFedWithFishBlockHeight;
	    private static readonly BigInteger _sizeOfPredatorDna = _sizeOfDna;
	    
	    public static byte[] GetPredatorDna(byte[] fish)
	    {
		    return fish.Range((int) _indexOfPredatorDna, (int) _sizeOfPredatorDna);
	    }

	    public static byte[] SetPredatorDna(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPredatorDna;
		    BigInteger middle2 = _indexOfPredatorDna + _sizeOfPredatorDna;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo.Take((int) _sizeOfPredatorDna)).Concat(lastPart);
	    }
	    
        public static readonly BigInteger Size =
            _sizeOfDna + _sizeOfBirthBlockHeight + _sizeOfQuantityOfFeeds + _sizeOfFedWithFishBlockHeight + _sizeOfPredatorDna;
			
	    private static readonly BigInteger _indexOfPropCarnivorous = 0;
	    private static readonly BigInteger _indexOfPropSize = 1;
	    private static readonly BigInteger _indexOfPropBelly = 2;
	    private static readonly BigInteger _indexOfPropBack = 3;
	    private static readonly BigInteger _indexOfPropTopBottomFins = 4;
	    private static readonly BigInteger _indexOfPropHead = 5;
	    private static readonly BigInteger _indexOfPropTail = 6;
	    private static readonly BigInteger _indexOfPropSideFin = 7;
	    
	    public static byte[] GetPropCarnivorous(byte[] fish)
	    {
		    return fish.Range((int) _indexOfPropCarnivorous, 1);
	    }

	    public static byte[] SetPropCarnivorous(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropCarnivorous;
		    BigInteger middle2 = _indexOfPropCarnivorous + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    public static byte[] GetPropSize(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropSize, 1);
	    }

	    public static byte[] SetPropSize(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropSize;
		    BigInteger middle2 = _indexOfPropSize + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    public static byte[] GetPropBelly(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropBelly, 1);
	    }

	    public static byte[] SetPropBelly(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropBelly;
		    BigInteger middle2 = _indexOfPropBelly + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    public static byte[] GetPropBack(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropBack, 1);
	    }

	    public static byte[] SetPropBack(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropBack;
		    BigInteger middle2 = _indexOfPropBack + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    public static byte[] GetPropTopBottomFins(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropTopBottomFins, 1);
	    }

	    public static byte[] SetPropTopBottomFins(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropTopBottomFins;
		    BigInteger middle2 = _indexOfPropTopBottomFins + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    public static byte[] GetPropHead(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropHead, 1);
	    }

	    public static byte[] SetPropHead(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropHead;
		    BigInteger middle2 = _indexOfPropHead + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    public static byte[] GetPropTail(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropTail, 1);
	    }

	    public static byte[] SetPropTail(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropTail;
		    BigInteger middle2 = _indexOfPropTail + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
	    public static byte[] GetPropSideFin(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropSideFin, 1);
	    }

	    public static byte[] SetPropSideFin(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = _indexOfStart;
		    BigInteger middle1 = _indexOfPropSideFin;
		    BigInteger middle2 = _indexOfPropSideFin + 1;
		    BigInteger end = fish.Length;
			
		    byte[] firstPart = fish.Range((int) start, (int) middle1);
		    byte[] lastPart = fish.Range((int) middle2, (int) (end - middle2));

		    return firstPart.Concat(newInfo).Concat(lastPart);
	    }
	    
    }
}