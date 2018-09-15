using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect.Helpers
{
    public class ByteArrHelper
    {
        public static byte[] InTheMiddle(byte[] subject, byte[] newInfo, BigInteger start, BigInteger middle1, BigInteger middle2)
        {
            BigInteger end = subject.Length;

            if (start > end)
            {
                start = end;
            }

            if (middle1 > end)
            {
                middle1 = end;
            }

            if (middle2 > end)
            {
                middle2 = end;
            }
			
            byte[] firstPart = subject.Range((int) start, (int) middle1);
            byte[] lastPart = subject.Range((int) middle2, (int) (end - middle2));

            return firstPart.Concat(newInfo).Concat(lastPart);
        }
    }
}