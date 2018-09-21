---
id: contracttrigger
title: Contract Triggers
sidebar_label: Contract Triggers
---

#Smart Contract Triggers
In NEO, everything is a contract. A contract is composed of NEO VM instructions, it can be a very simple contract, like the one you use in an address, or it can be a complex one, like a smart contract.
This is how a simple contract is built:
```C#
using (ScriptBuilder sb = new ScriptBuilder())
{
    sb.EmitPush(publicKey.EncodedData);
    sb.Emit(EVMOpCode.CHECKSIG);
    contractHexCode = sb.ToArray().ToHexString();
}
```
Note: NEO uses a stack-based virtual machine, meaning that the first instruction added to the script, will be the last one used. In this case, `CheckSig` expects a second operand, the public key, pushed to the stack before the instruction.

What this contract is saying is: "Check if the signature matches this public key".
Here is the result you would see in neo-scan:
![Alt CheckSig](images/CheckSig.png?raw=true "CheckSig")
