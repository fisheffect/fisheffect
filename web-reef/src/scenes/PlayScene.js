import toastr from 'toastr'
import { Scene } from 'phaser'
import Fish from '../model/Fish'
import NeoFetcher from '../neoFetcher'
import FishByteArray from "../model/FishByteArray";

export default class PlayScene extends Scene {
  constructor () {
    super({ key: 'PlayScene' })
  }

  create () {
    this.previousFishes = null;
    this.fishes = [];
    const bg = this.add.image(this.game.canvas.width / 2, this.game.canvas.height / 2, 'bg');

    const rightrocks = this.add.image(this.game.canvas.width / 2, this.game.canvas.height / 2, 'rightrocks');
    const leftrocks = this.add.image(this.game.canvas.width / 2, this.game.canvas.height / 2, 'leftrocks');

    const food = this.add.image(this.game.canvas.width - 80, this.game.canvas.height - 80, 'food');
    food.alpha = 0.4;

    const store = this.add.image(this.game.canvas.width - 160, this.game.canvas.height - 80, 'store');
    store.alpha = 0.4;

    rightrocks.depth = 1;
    leftrocks.depth = 1;
    food.depth = 1;
    store.depth = 1;

    const urlParams = new URLSearchParams(window.location.search);
    this.reefAddress = urlParams.get('reef');

    if (this.reefAddress) {
      this.loadFishes();
    } else {
      this.showAskAddressWindow();
    }

    this.foodInteraction(food);
    this.storeInteraction(store);
    this.scaleBackground(bg, rightrocks, leftrocks);
    this.loginInteraction();
    this.askAddressInteraction();
  }

  async loadFishes() {
    const resp = await NeoFetcher.getReefFishesAlive(this.reefAddress);
    console.log(resp);

    if (resp) {
      const byteArr = this.parseHexString(resp);
      const chunk = FishByteArray.getArraySize();
      const fishesBytes = [];

      for (var i = 0; i < byteArr.length; i += chunk) {
        const slice = byteArr.slice(i, i + chunk);
        fishesBytes.push(slice);
      }

      if (this.previousFishesBytes) {

        const newFishesBytes = fishesBytes.filter(f => {
          const fDna = f.slice(FishByteArray.getIndexOfDna(), FishByteArray.getIndexOfDna() + FishByteArray.getSizeOfDna());
          const prevDnas = this.previousFishesBytes.map(fp => fp.slice(FishByteArray.getIndexOfDna(), FishByteArray.getIndexOfDna() + FishByteArray.getSizeOfDna()))

          return !this.arrayContainsSubArray(prevDnas, fDna);
        });

        const deadFishesBytes = this.previousFishesBytes.filter(fp => {
          const prevDna = fp.slice(FishByteArray.getIndexOfDna(), FishByteArray.getIndexOfDna() + FishByteArray.getSizeOfDna());
          const fDnas = fishesBytes.map(f => f.slice(FishByteArray.getIndexOfDna(), FishByteArray.getIndexOfDna() + FishByteArray.getSizeOfDna()))

          return !this.arrayContainsSubArray(fDnas, prevDna);
        });

        if (newFishesBytes.length > 0) {
          toastr["success"]("A new Fish!");

          newFishesBytes.forEach(f => {
            this.fishes.push(new Fish(this, f));
          });

          if (deadFishesBytes.length > 0) {
            toastr["warning"]("A Fish was eaten!");

            this.fishes
              .filter(item => deadFishesBytes.includes(item.bytes))
              .forEach(toDie => this.fishes[2].middleAndDie());
          }
        }
      } else {
        fishesBytes
          .forEach(f => this.fishes.push(new Fish(this, f)));
      }


      this.previousFishesBytes = fishesBytes;
    }
  }

  parseHexString(hex) {
    const bytes = [];
    for (let c = 0; c < hex.length; c += 2) {
      bytes.push(parseInt(hex.substr(c, 2), 16));
    }
    return bytes;
  }

  arraysEqual(a, b) {
    if (a === b) return true;
    if (a == null || b == null) return false;
    if (a.length != b.length) return false;

    for (var i = 0; i < a.length; ++i) {
      if (a[i] !== b[i]) {
        return false;
      }
    }

    return true;
  }

  arrayContainsSubArray(arr, item) {
    for (let i = 0; i < arr.length; i++) {
      if (this.arraysEqual(arr[i], item)) {
        return true;
      }
    }
    return false;
  }

  async feedReef() {
    await NeoFetcher.feedReef();
    toastr["success"]("You Fed the Reef");
    await this.loadFishes();
  }

  fishsInLove() {
    this.fishes[0].disperse();
    this.fishes[1].disperse();
    this.fishes[2].middleAndDie();
    this.fishes[3].gonnaBeMommy();
    this.fishes[4].gonnaBeDaddy(() => {
      this.fishes[0].repeatMovement(true);
      this.fishes[1].repeatMovement(true);
      this.fishes[3].repeatMovement(true);
      this.fishes[4].repeatMovement(true);

      this.fishes.push(new Fish(this, '0011223344556677000000000000000000000000', true));
    });
  }

  foodInteraction(food) {
    food.setInteractive();

    food.on('pointerover', () => {
      food.alpha = 1;
    });

    food.on('pointerout', () => {
      food.alpha = 0.4;
    });

    food.on('pointerdown', async () => {

      if (NeoFetcher.userWallet) {
        await this.feedReef();
      } else {
        this.showLoginWindow();
      }
    });
  }

  storeInteraction(store) {
    store.setInteractive();

    store.on('pointerover', () => {
      store.alpha = 1;
    });

    store.on('pointerout', () => {
      store.alpha = 0.4;
    });

    store.on('pointerdown', () => {
      window.open("http://pudim.com.br");
    });
  }

  scaleBackground(bg, rightrocks, leftrocks) {
    const scaleX = this.game.canvas.width / bg.width;
    const scaleY = this.game.canvas.height / bg.height;
    const scaleBigger = Math.max(scaleX, scaleY);
    bg.scaleX = scaleBigger;
    bg.scaleY = scaleBigger;

    rightrocks.scaleX = scaleBigger;
    rightrocks.scaleY = scaleBigger;

    leftrocks.scaleX = scaleBigger;
    leftrocks.scaleY = scaleBigger;
  }

  loginInteraction() {
    document.querySelector("#login").addEventListener("submit", async (e) => {
      e.preventDefault();

      try {
        await NeoFetcher.login(
          document.querySelector("#passphrase").value,
          document.querySelector("#encryptedKey").value);

        toastr["success"]("Login successful");

        this.hideLoginWindow();

        await this.feedReef();
      } catch (e) {
        toastr["error"]("Login failed")
      }
    });

    document.querySelector("#closeLogin").addEventListener("click", async (e) => {
      this.hideLoginWindow();
    });
  }

  askAddressInteraction() {
    document.querySelector("#askAddress").addEventListener("submit", async (e) => {
      e.preventDefault();

      location.href = this.addParamToUrl(location.href, "reef",
        document.querySelector("#addressToGo").value);
    });

    document.querySelector("#closeAskAddress").addEventListener("click", async (e) => {
      this.hideAskAddressWindow();
    });
  }

  addParamToUrl(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
      return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
      return uri + separator + key + "=" + value;
    }
  }

  showAskAddressWindow() {
    document.querySelector("#askAddressWindow").style.visibility = 'visible';
  }

  hideAskAddressWindow() {
    document.querySelector("#askAddressWindow").style.visibility = 'hidden';
  }

  showLoginWindow() {
    document.querySelector("#loginWindow").style.visibility = 'visible';
  }

  hideLoginWindow() {
    document.querySelector("#loginWindow").style.visibility = 'hidden';
  }
}
