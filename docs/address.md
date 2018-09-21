---
id: address
title: Address
sidebar_label: Address
---


# NEO Address
An address in NEO is the result of the hash of your contract, with a '0x23' prefix (0x23 is the 'Address Version').
We present it in Base58 encoding. Example:
`3ff4cc2c6e81f63e48bfafd54295368caecaf742` converts into `AMc3UR534Kjc4eTwntpEMkebCiMinrk94J`

# Converting Address to ScriptHash
You can easily convert from an Address to a ScriptHash using NEO SDKs or Neo Eco Lab (https://neocompiler.io/#/ecolab).
In NEO Eco Lab, first convert the address into a ScriptHash, and then do a reverse-hex (Hex <-> xeH).
You usually need to do this conversion when interacting with the blockchain, since the VM operates with byte arrays and not base58 strings.

TODO: Explain why reverse hex.
