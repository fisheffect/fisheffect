---
id: block
title: Block
sidebar_label: Block
---

# Block
A Blockchain transaction can be defined as a small unit of task that is stored in public records. These records also knows as blocks. These blocks are executed, implemented and stored in blockchain only after the validation by all persons involved in the blockchain network. Each previous transaction can be reviewed at any time but cannot be updated.


# Block Header
A block header is where information about the block is stored, without the transaction list. Instead of keeping all the transactions in the header, we use a `Merkle Root` to ensure that the transactions in the block body are correct.
The information of a block header is always unique, and except for the header version, the probability of certain properties to repeat is very small.


## Merkle Tree / Root
A hash function always return the same value for the same input, and if we hash all the transactions in a block, in the same order, they will always result in the same hash.  This hash of hashes is called `Merkle Root`.
Using this property in the block header, we can assure that all the transactions in the block, and their order, are correct, since any change, in any transaction, would produce a different hash value.

##  Version
DataType : uint32
Description : version of the block

##  PreviousHash

DataType : uint256
Description : hash value of the previous block

##  ConsensusNumber

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque elementum dignissim ultricies. Fusce rhoncus ipsum tempor eros aliquam consequat. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus elementum massa eget nulla aliquet sagittis. Proin odio tortor, vulputate ut odio in, ultrices ultricies augue. Cras ornare ultrices lorem malesuada iaculis. Etiam sit amet libero tempor, pulvinar mauris sed, sollicitudin sapien.
