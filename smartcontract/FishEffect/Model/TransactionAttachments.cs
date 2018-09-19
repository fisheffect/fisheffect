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
		public static readonly BigInteger NeoSent = 0;
		public static readonly BigInteger GasSent = 0;
		public static byte[] ReceiverScriptHash { get; set; }
		public static byte[] SenderScriptHash { get; set; }
	}
}
