using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect.Helpers
{
    public class FishManager
    {
	    #region BUILDERS
	    
	    public static byte[] BuildImmigrant(byte[] reef, BigInteger consensusData, BigInteger randomStep)
	    {
		    byte[] newFish = new byte[0];
		    BigInteger endOfDnaInfluence = GetIndexOfDna() + SizeOfRandomDnaInfluence();
			
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
		
		    newFish = newFish.Concat(reef.Range((int) endOfDnaInfluence, (int) SizeOfReefDnaInfluence()));
		    newFish = newFish.Concat(blockHeight.AsByteArray()); // 4 bytes
		    byte[] quantityOfFeeds = new byte[] {0};
		    newFish = newFish.Concat(quantityOfFeeds); // how many times ate

		    BigInteger remainingSize = newFish.Length - GetSize();
		    newFish = newFish.Concat(new byte[(int) remainingSize]);

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

		    BigInteger remainingSize = newFish.Length - GetSize();
		    byte[] emptyBytes = AppGlobals.EmptyBytes.Range(0, (int)remainingSize);
		    newFish = newFish.Concat(emptyBytes);

		    return newFish;
	    }
	    
	    #endregion
	    
	    #region META GETTERS AND SETTERS

	    public static byte[] GetRandomDnaInfluence(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfRandomDnaInfluence(), (int) SizeOfRandomDnaInfluence());
	    }

	    public static byte[] SetRandomDnaInfluence(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfRandomDnaInfluence();
		    BigInteger middle2 = GetIndexOfRandomDnaInfluence() + SizeOfRandomDnaInfluence();
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    public static byte[] GetReefDnaInfluence(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfReefDnaInfluence(), (int) SizeOfReefDnaInfluence());
	    }

	    public static byte[] SetReefDnaInfluence(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfReefDnaInfluence();
		    BigInteger middle2 = GetIndexOfReefDnaInfluence() + SizeOfReefDnaInfluence();
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
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
	    
	    public static byte[] GetBirthBlockHeight(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfBirthBlockHeight(), (int) SizeOfBirthBlockHeight());
	    }

	    public static byte[] SetBirthBlockHeight(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfBirthBlockHeight();
		    BigInteger middle2 = GetIndexOfBirthBlockHeight() + SizeOfBirthBlockHeight();
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }
	    
	    public static byte[] GetQuantityOfFeeds(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfQuantityOfFeeds(), (int) SizeOfQuantityOfFeeds());
	    }

	    public static byte[] SetQuantityOfFeeds(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfQuantityOfFeeds();
		    BigInteger middle2 = GetIndexOfQuantityOfFeeds() + SizeOfQuantityOfFeeds();
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    public static byte[] GetFedWithFishBlockHeight(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfFedWithFishBlockHeight(), (int) SizeOfFedWithFishBlockHeight());
	    }

	    public static byte[] SetFedWithFishBlockHeight(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfFedWithFishBlockHeight();
		    BigInteger middle2 = GetIndexOfFedWithFishBlockHeight() + SizeOfFedWithFishBlockHeight();
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    public static byte[] GetPredatorDna(byte[] fish)
	    {
		    return fish.Range((int) GetIndexOfPredatorDna(), (int) SizeOfPredatorDna());
	    }

	    public static byte[] SetPredatorDna(byte[] fish, byte[] newInfo)
	    {
		    BigInteger start = GetIndexOfStart();
		    BigInteger middle1 = GetIndexOfPredatorDna();
		    BigInteger middle2 = GetIndexOfPredatorDna() + SizeOfPredatorDna();
		    return ByteArrHelper.InTheMiddle(fish, newInfo, start, middle1, middle2);
	    }

	    public static BigInteger GetSize()
	    {
		    return GetSizeOfDna() + SizeOfBirthBlockHeight() + SizeOfQuantityOfFeeds() + SizeOfFedWithFishBlockHeight() +
		           SizeOfPredatorDna();
	    }
	    
	    #endregion

	    #region META INDEXES AND SIZES
	    
	    private static BigInteger GetIndexOfStart()
	    {
		    return 0;
	    }

	    private static BigInteger GetIndexOfRandomDnaInfluence()
	    {
		    return GetIndexOfStart();
	    }

	    private static BigInteger SizeOfRandomDnaInfluence()
	    {
		    return 4;
	    }

	    private static BigInteger GetIndexOfReefDnaInfluence()
	    {
		    return GetIndexOfRandomDnaInfluence() + SizeOfRandomDnaInfluence();
	    }

	    private static BigInteger SizeOfReefDnaInfluence()
	    {
		    return 16;
	    }

	    private static BigInteger GetIndexOfDna()
	    {
		    return GetIndexOfStart();
	    }

	    private static BigInteger GetSizeOfDna()
	    {
		    return SizeOfRandomDnaInfluence() + SizeOfReefDnaInfluence();
	    }

	    private static BigInteger GetIndexOfBirthBlockHeight()
	    {
		    return GetIndexOfDna() + GetSizeOfDna();
	    }

	    private static BigInteger SizeOfBirthBlockHeight()
	    {
		    return 4;
	    }

	    private static BigInteger SizeOfQuantityOfFeeds()
	    {
		    return 1;
	    }

	    private static BigInteger GetIndexOfQuantityOfFeeds()
	    {
		    return GetIndexOfBirthBlockHeight() + SizeOfBirthBlockHeight();
	    }

	    private static BigInteger GetIndexOfFedWithFishBlockHeight()
	    {
		    return GetIndexOfQuantityOfFeeds() + SizeOfQuantityOfFeeds();
	    }

	    private static BigInteger SizeOfFedWithFishBlockHeight()
	    {
		    return 4;
	    }

	    private static BigInteger GetIndexOfPredatorDna()
	    {
		    return GetIndexOfFedWithFishBlockHeight() + SizeOfFedWithFishBlockHeight();
	    }

	    private static BigInteger SizeOfPredatorDna()
	    {
		    return GetSizeOfDna();
	    }
	    
	    #endregion

	    #region DNA PROPS GETTERS AND SETTERS
	    
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

	    #endregion
	    
    }
}