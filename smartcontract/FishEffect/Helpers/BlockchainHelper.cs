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
        public static BigInteger GetConsensusData()
        {
            return Blockchain.GetHeader(Blockchain.GetHeight()).ConsensusData;
        }

        public static BigInteger GetRandomStep()
        {
            return UtilityDao.GetRandomStep();
        }

        public static void UpdateRandomStep(BigInteger currentStep)
        {
            UtilityDao.UpdateRandomStep(currentStep);
        }

        public static BigInteger Random(BigInteger consensusData, BigInteger currentStep)
		{
			byte[] randomBytesArray = consensusData.AsByteArray();
			BigInteger randomNumberByteSize = randomBytesArray.Length;
            BigInteger randomCursor = currentStep % randomNumberByteSize;
            byte randomByte = randomBytesArray[(int)randomCursor];
			BigInteger randomNumber = randomByte;

			if (randomNumber < 0)
			{
				randomNumber = randomNumber * -1;
			}

			Runtime.Log("rnd: " + randomNumber.AsByteArray().AsString());
			return randomNumber;
		}

		public static BigInteger GetHeight()
		{
			return Blockchain.GetHeight();
		}
	}
}
