import Neon, { wallet, rpc, api } from '@cityofzion/neon-js';

const contractPath = 'http://18.231.135.105:30333';
const neoScanPath = 'http://18.231.135.105:4000/api/main_net';
const scriptHash = '7d01448da1f02b28d93938baab9f8ba07be05fea';

export default {
  userWallet: null,

  async login(passphrase, encryptedWIF) {
    if (!wallet.isNEP2(encryptedWIF)) {
      throw new Error('That is not a valid encrypted key');
    }

    const wif = await wallet.decrypt(encryptedWIF, passphrase);
    this.userWallet = new wallet.Account(wif);
  }
}