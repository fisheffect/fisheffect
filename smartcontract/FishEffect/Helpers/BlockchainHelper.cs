using FishEffect.Dao;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect.Helpers
{
	class BlockchainHelper
	{
		public static BigInteger _consensusData = 0;
		
		public static BigInteger Random()
		{
			Runtime.Log("Random called");
			if (_consensusData == 0)
			{
				_consensusData = Blockchain.GetHeader(Blockchain.GetHeight()).ConsensusData;
			}

			BigInteger randomStep = UtilityDao.GetRandomStep();
			byte[] randomBytesArray = _consensusData.AsByteArray();
			BigInteger randomNumberByteSize = randomBytesArray.Length;
			byte randomByte = randomBytesArray[(int)randomStep % (int)randomNumberByteSize];
			BigInteger randomNumber = randomByte;

			randomStep = randomStep + 1;
			UtilityDao.UpdateRandomStep(randomStep);

			//randomNumber = randomNumber % byte.MaxValue;

			return randomNumber;
		}

		
	}
}
