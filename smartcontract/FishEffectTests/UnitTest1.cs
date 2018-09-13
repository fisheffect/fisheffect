using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FishEffectTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var byteArray = new byte[]{ 155, 124, 255, 218, 166, 116, 190, 174, 15, 147, 14, 190, 96, 133, 175, 144, 147, 229, 254, 86, 179, 74, 92, 34, 12, 205, 207, 110, 252, 51, 111, 197 };
			PrintBytes(byteArray);
			var teste = 20;
		}


		public string PrintBytes(byte[] byteArray)
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
	}
}
