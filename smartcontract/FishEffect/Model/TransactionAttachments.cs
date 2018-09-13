using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.System;
using Neo.SmartContract.Framework.Services.Neo;
using System;
using System.ComponentModel;
using System.Numerics;

namespace FishEffect.Model
{
	class TransactionAttachments
	{
		public static BigInteger NeoSent { get; set; }
		public static BigInteger GasSent { get; set; }
		public static byte[] ReceiverScriptHash { get; set; }
		public static byte[] SenderScriptHash { get; set; }
	}
}
