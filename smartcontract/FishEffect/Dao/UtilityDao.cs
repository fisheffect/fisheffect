using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Neo.SmartContract.Framework;

namespace FishEffect.Dao
{
	class UtilityDao
	{

		#region KEY
		private static byte[] KeyOfRandomStep()
		{
			return "randomStep".AsByteArray();
		}
		#endregion

		#region METHODS
		public static BigInteger GetRandomStep()
		{
			byte[] keyOfRandomStep = KeyOfRandomStep();
			BigInteger randomStep = GenericDao.Get(keyOfRandomStep).AsBigInteger();
			return randomStep;
		}

		public static void UpdateRandomStep(BigInteger randomStep)
		{
			byte[] keyOfRandomStep = KeyOfRandomStep();
			GenericDao.Put(keyOfRandomStep, randomStep.AsByteArray());
		}

		#endregion
	}
}
