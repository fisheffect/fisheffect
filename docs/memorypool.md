---
id: memorypool
title: Memory Pool
sidebar_label: Memory Pool
---


# Memory Pool
A transaction is confirmed after the block it pertains is committed (confirmed), but before it is confirmed, it exists in what we call a `Memory Pool`. We add to this pool all transactions that are queued to be confirmed. The efficiency and performance of the memory pool infers directly in the performance of the network, since all the transactions pass through the memory pool before being committed.

In `neo-cli`, the memory pool can be found in `neo/Ledger/Blockchain.cs`:
```C#
private readonly ConcurrentDictionary<UInt256, Transaction> mem_pool = new ConcurrentDictionary<UInt256, Transaction>();
```
