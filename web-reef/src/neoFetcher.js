import Neon, { wallet, rpc, api } from '@cityofzion/neon-js';

const contractPath = 'http://18.191.236.185:30333';
const neoscan = 'http://18.191.236.185:4000/api/main_net';
const scriptHash = '9e156e91a703e6a0c05701d0e210f985ae9f5eef';

const privateNet = new rpc.Network({
  name: 'PrivateNet',
  extra: {
    neoscan
  }
});

Neon.add.network(privateNet);

export default {
  userWallet: null,

  hexstring2str(subj) {
    return subj && subj.length ? Neon.u.hexstring2str(subj) : '';
  },

  str2hexstring(subj) {
    return subj && subj.length ? Neon.u.str2hexstring(subj) : '';
  },

  reverseHex(subj) {
    return subj && subj.length ? Neon.u.reverseHex(subj) : '';
  },

  addressToScriptHash(subj) {
    return subj && subj.length ? this.reverseHex(wallet.getScriptHashFromAddress(subj)) : '';
  },

  hex2number(hex) {
    return hex && hex.length ? parseInt(hex, 16) : '';
  },

  async testInvoke(operation, ...args) {
    const script = Neon.create.script({ scriptHash, operation, args });

    const resp = await rpc.Query.invokeScript(script).execute(contractPath);

    return {
      result: resp.result.stack && resp.result.stack.length ? resp.result.stack[resp.result.stack.length - 1].value : null,
      gasConsumed: resp.result.gas_consumed
    };
  },

  async doInvoke(operation, ...args) {
    const resp = await this.testInvoke(operation, ...args);

    const opResult = await api.doInvoke({
      api: new api.neoscan.instance('PrivateNet'),
      net: neoscan,
      privateKey: this.userWallet.privateKey,
      address: this.userWallet.address,
      intents: [{
        assetId: Neon.CONST.ASSET_ID.GAS,
        value: resp.gasConsumed,
        scriptHash,
      }],
      script: { scriptHash, operation, args },
      account: this.userWallet,
      gas: 0,
    });

    return opResult.response.result ? resp.result : "error";
  },

  async login(passphrase, encryptedWIF) {
    if (!wallet.isNEP2(encryptedWIF)) {
      throw new Error('That is not a valid encrypted key');
    }

    const wif = await wallet.decrypt(encryptedWIF, passphrase);
    this.userWallet = new wallet.Account(wif);

  },

  async feedReef(reefAddress) {
    const resp = await this.doInvoke('feedReef',
      this.reverseHex(this.userWallet.scriptHash));

    return this.hexstring2str(resp);
  },

  async test(msg) {
    const resp = await this.testInvoke('test',
      this.str2hexstring(msg));

    console.log(this.hexstring2str(resp.result));
  },

  async getReefFishesAlive(reefAddress) {
    const resp = await this.testInvoke('getReefFishesAlive', this.addressToScriptHash(reefAddress));

    return resp.result;
  },
}