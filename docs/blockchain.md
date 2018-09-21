---
id: blockchain
title: Blockchain
sidebar_label: Blockchain
---

# Block
A Blockchain transaction can be defined as a small unit of task that is stored in public records. These records also knows as blocks. These blocks are executed, implemented and stored in blockchain only after the validation by all persons involved in the blockchain network. Each previous transaction can be reviewed at any time but cannot be updated.


# Block Header
A block header is where information about the block is stored, without the transaction list. Instead of keeping all the transactions in the header, we use a `Merkle Root` to ensure that the transactions in the block body are correct.
The information of a block header is always unique, and except for the header version, the probability of certain properties to repeat is very small.


## Block Header Content

####  Version
DataType : uint32
Description : version of the block

####  PreviousHash
DataType : UInt256
Description : Hash value of the previous block

#### Merkle Root
A hash function always return the same value for the same input, and if we hash all the transactions in a block, in the same order, they will always result in the same hash.  This hash of hashes is called Merkle Root.
Using this property in the block header, we can assure that all the transactions in the block, and their order, are correct, since any change, in any transaction, would produce a different hash value.

#### Timestamp
DataType : UInt32
Description : Block creation date in Unix Timestamp

#### Height
DataType: UInt32
Description: The block position in the blockchain. The height of the block must be exactly equal to the height of the previous block plus 1.

#### Nonce
DataType : UInt64
Description : A random number "selected" by the miner of the current block;

#### NextMiner
DataType : UInt160
Description : The scripthash of the next `consensus speaker`. In NEO, one of the `validators`('miners') is selected to be the speaker. The speaker is responsible for proposing the block, that will be later validated by the other `validators`, meaning that the speaker can propose a block, but it cannot propose invalid transactions, since they won't be accepted by the other `validators` in the network.  Check the `consensus` page for more information.

#### Validation Script
DataType : Byte Array (flexible length)
Description : A block to be considered valid must be accepted by the network validators. This validation happens through signature checking. Since the identity of the validators are known, a block to be considered valid must be signed by 2/3 + 1 of the validators. The script is composed of `NEO VM Opcodes`.  

# Transaction
