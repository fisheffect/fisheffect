import Neon, { wallet, rpc, api } from '@cityofzion/neon-js';

const contractPath = 'http://18.191.236.185:30333';
const neoscan = 'http://18.191.236.185:4000/api/main_net';
const scriptHash = '13c05d1ff69d3ad1cbdb89f729da9584893303a9';

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

  // toByteArray(subj) {
  //   return subj && subj.length ? Neon.u.toByteArray(subj) : '';
  // },

  hex2number(hex) {
    return hex && hex.length ? parseInt(hex, 16) : '';
  },

  async testInvoke(operation, ...args) {
    const script = Neon.create.script({ scriptHash, operation, args });

    const resp = await rpc.Query.invokeScript(script).execute(contractPath);

    return resp.result.stack && resp.result.stack.length ? resp.result.stack[1].value : null;
  },

  async doInvoke(operation, gas, ...args) {
    const resp = await api.doInvoke({
      api: new api.neoscan.instance('PrivateNet'),
      net: neoscan,
      privateKey: this.userWallet.privateKey,
      address: this.userWallet.address,
      intents: [{
        assetId: Neon.CONST.ASSET_ID.GAS,
        value: gas,
        scriptHash,
      }],
      script: { scriptHash, operation, args },
      gas: 0,
      account: this.userWallet,
    });

    return resp.response;
  },

  async login(passphrase, encryptedWIF) {
    if (!wallet.isNEP2(encryptedWIF)) {
      throw new Error('That is not a valid encrypted key');
    }

    const wif = await wallet.decrypt(encryptedWIF, passphrase);
    this.userWallet = new wallet.Account(wif);

  },

  async name() {
      const resp = await this.testInvoke('name');
      console.log(this.hexstring2str(resp));
  },

  async buyFishFood(reefAddress) {
    const resp = await this.doInvoke('feedReef', 3,
      this.userWallet.publicKey);

    return resp;
  },

  async feedReef(reefAddress) {
    const resp = await this.doInvoke('feedReef', 3,
      this.reverseHex(this.userWallet.scriptHash));

    return resp;
  },

  async getReefFishesAlive(reefAddress) {
    return await this.testInvoke('getReefFishesAlive', this.str2hexstring(reefAddress));
  },
}