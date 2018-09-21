---
id: configurationfiles
title: Script Configuration Files
sidebar_label: Configuration Files
---

## Neo-python
The configuration files used in neo-python are stored in `neo-python/neo/data/`. You can choose the network you want to connect using parameters during startup.
`np-prompt -p` will connect to your local network, while `np-prompt -m` will synchronize your files using the main network.
If you have setup a remote private network, you can connect to it using `np-prompt -p IP-ADDRESS`.
Example of neo-python `protocol.json` \(configured to access a local private network\):
```json
{
  "ProtocolConfiguration": {
    "Magic": 56753,
    "AddressVersion": 23,
    "StandbyValidators": [
        "02b3622bf4017bdfe317c58aed5f4c753f206b7db896046fa7d774bbc4bf7f8dc2",
        "02103a7f7dd016558597f7960d27c516a4394fd968b9e65155eb4b013e4040406e",
        "03d90c07df63e690ce77912e10ab51acc944b66860237b608c4f8f8309e71ee699",
        "02a7bc55fe8684e0119768d104ba30795bdcc86619e864add26156723ed185cd62"
    ],
    "SeedList": [
        "127.0.0.1:20333",
        "127.0.0.1:20334",
        "127.0.0.1:20335",
        "127.0.0.1:20336"
    ],
    "RPCList":[
      "http://127.0.0.1:30333"
    ],
    "SystemFee": {
        "EnrollmentTransaction": 1000,
        "IssueTransaction": 500,
        "PublishTransaction": 500,
        "RegisterTransaction": 10000
    }
  },

  "ApplicationConfiguration": {
    "DataDirectoryPath": "Chains/privnet",
    "NotificationDataPath": "Chains/privnet_notif",
    "RPCPort": 20332,
    "NodePort": 20333,
    "WsPort": 20334,
    "UriPrefix": [ "http://*:20332" ],
    "SslCert": "",
    "SslCertPassword": "",
    "BootstrapFile":"",
    "NotificationBootstrapFile":"",
    "DebugStorage":1
  }
}
```

## Neo-cli
In Neo-cli, we use two configuration files. One is used to define the protocol and network information, and the other is related to the neo-cli application.
Use `config.json` for neo-cli related configuration and `protocol.json` for neo protocol related configuration.
Example of `config.json`:
```json
{
  "ApplicationConfiguration": {
    "Paths": {
      "Chain": "Chain_{0}",
      "Index": "Index_{0}"
    },
    "P2P": {
      "Port": 10333,
      "WsPort": 10334
    },
    "RPC": {
      "Port": 10332,
      "SslCert": "",
      "SslCertPassword": ""
    },
    "UnlockWallet": {
      "Path": "",
      "Password": "",
      "StartConsensus": false,
      "IsActive": false
    }
  }
}
```
Example of `protocol.json`:
```json
{
  "ProtocolConfiguration": {
    "Magic": 7630401,
    "AddressVersion": 23,
    "SecondsPerBlock": 15,
    "StandbyValidators": [
      "03b209fd4f53a7170ea4444e0cb0a6bb6a53c2bd016926989cf85f9b0fba17a70c",
      "02df48f60e8f3e01c48ff40b9b7f1310d7a8b2a193188befe1c2e3df740e895093",
      "03b8d9d5771d8f513aa0869b9cc8d50986403b78c6da36890638c3d46a5adce04a",
      "02ca0e27697b9c248f6f16e085fd0061e26f44da85b58ee835c110caa5ec3ba554",
      "024c7b7fb6c310fccf1ba33b082519d82964ea93868d676662d4a59ad548df0e7d",
      "02aaec38470f6aad0042c6e877cfd8087d2676b0f516fddd362801b9bd3936399e",
      "02486fd15702c4490a26703112a5cc1d0923fd697a33406bd5a1c00e0013b09a70"
    ],
    "SeedList": [
      "seed1.ngd.network:10333",
      "seed2.ngd.network:10333",
      "seed3.ngd.network:10333",
      "seed4.ngd.network:10333",
      "seed5.ngd.network:10333",
      "seed6.ngd.network:10333",
      "seed7.ngd.network:10333",
      "seed8.ngd.network:10333",
      "seed9.ngd.network:10333",
      "seed10.ngd.network:10333",
      "seed1.neo.org:10333",
      "seed2.neo.org:10333",
      "seed3.neo.org:10333",
      "seed4.neo.org:10333",
      "seed5.neo.org:10333"
    ],
    "SystemFee": {
      "EnrollmentTransaction": 1000,
      "IssueTransaction": 500,
      "PublishTransaction": 500,
      "RegisterTransaction": 10000
    }
  }
}
```
