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
            BigInteger fixedRandomNumber = (ulong)83789237598;
            // return Blockchain.GetHeader(Blockchain.GetHeight()).ConsensusData;
            return fixedRandomNumber;
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
			Runtime.Log("Random called");

			byte[] randomBytesArray = consensusData.AsByteArray();

			BigInteger randomNumberByteSize = randomBytesArray.Length;

            BigInteger randomCursor = currentStep % randomNumberByteSize;

            byte randomByte = randomBytesArray[(int)randomCursor];

			BigInteger randomNumber = randomByte;

			Runtime.Log("randomNumber: ");
			Runtime.Log(randomNumber.AsByteArray().AsString());

			return randomNumber;
		}

		
	}
}
