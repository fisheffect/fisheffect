using System;
using System.Linq;
using System.Text;
using Neo.Cryptography;

namespace FishEffectHelperProject
{
    class Program
    {
        static void Main(string[] args)
        {
			var byteArray = Helper.Base58CheckDecode("AYQvsVafPPkHSTdVYYGgz1B9CPsqpM2XVk");
			var stringRepresentation = PrintBytes(byteArray);
			Console.WriteLine(stringRepresentation);

		}

		public static string PrintBytes(byte[] byteArray)
		{
			var sb = new StringBuilder("new byte[] { ");
			for (var i = 0; i < byteArray.Length; i++)
			{
				var b = byteArray[i];
				sb.Append(b);
				if (i < byteArray.Length - 1)
				{
					sb.Append(", ");
				}
			}
			sb.Append(" }");
			return sb.ToString();
		}

		public static byte[] StringToByteArray(string hex)
		{
			return Enumerable.Range(0, hex.Length)
							 .Where(x => x % 2 == 0)
							 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
							 .ToArray();
		}
	}
}
