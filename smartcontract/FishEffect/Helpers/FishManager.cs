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
		    BigInteger endOfDnaInfluence = GetIndexOfDna() + _sizeOfRandomDnaInfluence;
			
		    // First 4 properties are random: Vegan-Carnivorous, Size, BellyType, BackType
		    for (int i = (int) GetIndexOfDna(); i < endOfDnaInfluence; i++)
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

		    do
		    {
			    newFish = newFish.Concat(new byte[] {0});
		    } while (newFish.Length < GetSize());

		    return newFish;
	    }

	    public static byte[] BuildFromParents(BigInteger consensusData, BigInteger randomStep, byte[] father, byte[] mother)
	    {
		    byte[] newFish = new byte[0];
			
		    BigInteger endOfDna = GetIndexOfDna() + GetSizeOfDna();
		    for (int i = (int) GetIndexOfDna(); i < endOfDna; i++)
		    {
			    // Mix DNA
			    BigInteger aRandom = BlockchainHelper.Random(consensusData, randomStep);
			    randomStep = randomStep + 1;
			    if (aRandom % 2 > 0)
			    {
				    newFish = newFish.Concat(father.Range(i, 1));
			    }
			    else
			    {
				    newFish = newFish.Concat(mother.Range(i, 1));
			    }				
		    }

		    BigInteger blockHeight = Blockchain.GetHeight();
			
		    newFish = newFish.Concat(blockHeight.AsByteArray()); // 4 bytes
		    byte[] quantityOfFeeds = new byte[] {0};
		    newFish = newFish.Concat(quantityOfFeeds); // how many times ate

		    do
		    {
			    newFish = newFish.Concat(new byte[] {0});
		    } while (newFish.Length < GetSize());

		    return newFish;
	    }
	    
	    private static BigInteger GetIndexOfStart()
	    {
		    return 0;
	    }

	    private static BigInteger GetIndexOfRandomDnaInfluence()
	    {
		    return GetIndexOfStart();
	    }
	    
        private static readonly BigInteger _sizeOfRandomDnaInfluence = 4;

	    public static byte[] GetRandomDnaInfluence(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfRandomDnaInfluence(), (int) _sizeOfRandomDnaInfluence);
	    }

	    public static byte[] SetRandomDnaInfluence(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfRandomDnaInfluence();
		    BigInteger middle2 = GetIndexOfRandomDnaInfluence() + _sizeOfRandomDnaInfluence;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    private static BigInteger GetIndexOfReefDnaInfluence()
	    {
		    return GetIndexOfRandomDnaInfluence() + _sizeOfRandomDnaInfluence;
	    }
	    
        private static readonly BigInteger _sizeOfReefDnaInfluence = 16;

	    public static byte[] GetReefDnaInfluence(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfReefDnaInfluence(), (int) _sizeOfReefDnaInfluence);
	    }

	    public static byte[] SetReefDnaInfluence(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfReefDnaInfluence();
		    BigInteger middle2 = GetIndexOfReefDnaInfluence() + _sizeOfReefDnaInfluence;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    private static BigInteger GetIndexOfDna()
	    {
		    return GetIndexOfStart();
	    }

	    private static BigInteger GetSizeOfDna()
	    {
		    return _sizeOfRandomDnaInfluence + _sizeOfReefDnaInfluence;
	    }
	    
	    public static byte[] GetDna(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfDna(), (int) GetSizeOfDna());
	    }

	    public static byte[] SetDna(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfDna();
		    BigInteger middle2 = GetIndexOfDna() + GetSizeOfDna();
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    private static BigInteger GetIndexOfBirthBlockHeight()
	    {
		    return GetIndexOfDna() + GetSizeOfDna();
	    }
	    
	    private static readonly BigInteger _sizeOfBirthBlockHeight = 4;
	    
	    public static byte[] GetBirthBlockHeight(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfBirthBlockHeight(), (int) _sizeOfBirthBlockHeight);
	    }

	    public static byte[] SetBirthBlockHeight(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfBirthBlockHeight();
		    BigInteger middle2 = GetIndexOfBirthBlockHeight() + _sizeOfBirthBlockHeight;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
		
	    private static readonly BigInteger _sizeOfQuantityOfFeeds = 1;

	    private static BigInteger GetIndexOfQuantityOfFeeds()
	    {
		    return GetIndexOfBirthBlockHeight() + _sizeOfBirthBlockHeight;
	    }
	    
	    public static byte[] GetQuantityOfFeeds(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfQuantityOfFeeds(), (int) _sizeOfQuantityOfFeeds);
	    }

	    public static byte[] SetQuantityOfFeeds(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfQuantityOfFeeds();
		    BigInteger middle2 = GetIndexOfQuantityOfFeeds() + _sizeOfQuantityOfFeeds;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    private static BigInteger GetIndexOfFedWithFishBlockHeight()
	    {
		    return GetIndexOfQuantityOfFeeds() + _sizeOfQuantityOfFeeds;
	    }
		
	    private static readonly BigInteger _sizeOfFedWithFishBlockHeight = 4;
	    
	    public static byte[] GetFedWithFishBlockHeight(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfFedWithFishBlockHeight(), (int) _sizeOfFedWithFishBlockHeight);
	    }

	    public static byte[] SetFedWithFishBlockHeight(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfFedWithFishBlockHeight();
		    BigInteger middle2 = GetIndexOfFedWithFishBlockHeight() + _sizeOfFedWithFishBlockHeight;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    private static BigInteger GetIndexOfPredatorDna()
	    {
		    return GetIndexOfFedWithFishBlockHeight() + _sizeOfFedWithFishBlockHeight;
	    }
		
	    private static readonly BigInteger _sizeOfPredatorDna = 0 + GetSizeOfDna();
	    
	    public static byte[] GetPredatorDna(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfPredatorDna(), (int) _sizeOfPredatorDna);
	    }

	    public static byte[] SetPredatorDna(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfPredatorDna();
		    BigInteger middle2 = GetIndexOfPredatorDna() + _sizeOfPredatorDna;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    public static BigInteger GetSize()
	    {
		    return GetSizeOfDna() + _sizeOfBirthBlockHeight + _sizeOfQuantityOfFeeds + _sizeOfFedWithFishBlockHeight +
		           _sizeOfPredatorDna;
	    }
			
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
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropCarnivorous;
		    BigInteger middle2 = _indexOfPropCarnivorous + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetPropSize(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropSize, 1);
	    }

	    public static byte[] SetPropSize(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropSize;
		    BigInteger middle2 = _indexOfPropSize + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetPropBelly(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropBelly, 1);
	    }

	    public static byte[] SetPropBelly(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropBelly;
		    BigInteger middle2 = _indexOfPropBelly + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetPropBack(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropBack, 1);
	    }

	    public static byte[] SetPropBack(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropBack;
		    BigInteger middle2 = _indexOfPropBack + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetPropTopBottomFins(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropTopBottomFins, 1);
	    }

	    public static byte[] SetPropTopBottomFins(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropTopBottomFins;
		    BigInteger middle2 = _indexOfPropTopBottomFins + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetPropHead(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropHead, 1);
	    }

	    public static byte[] SetPropHead(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropHead;
		    BigInteger middle2 = _indexOfPropHead + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetPropTail(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropTail, 1);
	    }

	    public static byte[] SetPropTail(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropTail;
		    BigInteger middle2 = _indexOfPropTail + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetPropSideFin(byte[] fish)
	    {
	    	return fish.Range((int) _indexOfPropSideFin, 1);
	    }

	    public static byte[] SetPropSideFin(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = _indexOfPropSideFin;
		    BigInteger middle2 = _indexOfPropSideFin + 1;
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
    }
}