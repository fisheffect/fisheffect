---
id: interfacing_ios
title: Swift for iOS
sidebar_label: Swift for iOS
---

# Using NEO in iOS
You can use neo-swift from O3Labs to connect your iOS app to the NEO Blockchain. You can find samples in the `ios-client` repository.

## Connecting to your private network;
To use your own network, instantiate your NeoClient using the URL from your server. The project is using the private network defaults.
We do this in the `InteractionViewController`.
```Swift
let neoClient = NeoClient(seedURL: "http://18.191.236.185:30333")
```

## Read only operations;
Some operations are read-only, so it is not necessary to sign or pay for this operations.
The operations "name", "symbol", "decimals", "totalSupply" are read only, so instead of sending a signed request to the blockchain, we can use the RPC `testinvoke`.

## Writing to the blockchain;
For other operations that require signature checking, we need to use the RPC `sendrawtransaction`. This is the only RPC call that actually changes the blockchain.
