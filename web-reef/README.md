# FishEffect Web Reef

This project uses Neon-JS to connect with the Neo Smart Contract and Phaser to render the Reef.

### Configure your network
neoFetcher.js
```javascript
const contractPath = 'http://18.191.236.185:30333'; // contract path used on testInvoke
const neoscan = 'http://18.191.236.185:4000/api/main_net'; // neoscan path to get node informations and utxo information, without it you can't spend coins, this wont be a problem in Neo 3.0
const scriptHash = '9e156e91a703e6a0c05701d0e210f985ae9f5eef'; // Our Smart Contract script hash
```

### Running in Development

```bash
npm run start
```

### Using the Neon-js to read information
Unfornetly Neon-js is not able to retrieve information if the SC uses `CheckWitness` yet.
```javascript
import Neon, { wallet, rpc, api } from '@cityofzion/neon-js';

const script = Neon.create.script({ scriptHash, operation, args });
const resp = await rpc.Query.invokeScript(script).execute(contractPath);
```

### Using the Neon-js to save information
To execute a writting information you should use this method, and it works with `CheckWitness`.
```javascript
import Neon, { wallet, rpc, api } from '@cityofzion/neon-js';

const resp = await api.doInvoke({
  api: new api.neoscan.instance('PrivateNet'),
  net: neoscan,
  privateKey: this.userWallet.privateKey,
  address: this.userWallet.address,
  intents: [{
    assetId: Neon.CONST.ASSET_ID.GAS,
    value: 0.1, // here is where you put the gas you wanna send, it's a good idea to check the testInvoke to see how much gas you need to send
    scriptHash,
  }],
  script: { scriptHash, operation, args },
  account: this.userWallet,
  gas: 0,
});
```