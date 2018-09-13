import { Scene } from 'phaser'
import Fish from '../model/Fish'
import NeoFetcher from '../neoFetcher'

export default class PlayScene extends Scene {
  constructor () {
    super({ key: 'PlayScene' })
  }

  create () {
    this.fishs = [];
    const bg = this.add.image(this.game.canvas.width / 2, this.game.canvas.height / 2, 'bg');

    this.fishs.push(new Fish(this, '5566778899aabbcc000000000000000000000000'));
    this.fishs.push(new Fish(this, '445566778899aabb000000000000000000000000'));
    this.fishs.push(new Fish(this, '33445566778899aa000000000000000000000000'));
    this.fishs.push(new Fish(this, '2233445566778899000000000000000000000000'));
    this.fishs.push(new Fish(this, '1122334455667788000000000000000000000000'));

    const rightrocks = this.add.image(this.game.canvas.width / 2, this.game.canvas.height / 2, 'rightrocks');
    const leftrocks = this.add.image(this.game.canvas.width / 2, this.game.canvas.height / 2, 'leftrocks');
    rightrocks.depth = 1;
    leftrocks.depth = 1;

    const food = this.add.image(this.game.canvas.width - 80, this.game.canvas.height - 80, 'food');
    food.alpha = 0.4;

    const store = this.add.image(this.game.canvas.width - 160, this.game.canvas.height - 80, 'store');
    store.alpha = 0.4;

    food.depth = 1;
    store.depth = 1;

    this.foodInteraction(food);
    this.storeInteraction(store);
    this.scaleBackground(bg, rightrocks, leftrocks);
    this.loginInteraction();
  }

  fishsInLove() {
    this.fishs[0].disperse();
    this.fishs[1].disperse();
    this.fishs[2].middleAndDie();
    this.fishs[3].gonnaBeMommy();
    this.fishs[4].gonnaBeDaddy(() => {
      this.fishs[0].repeatMovement(true);
      this.fishs[1].repeatMovement(true);
      this.fishs[3].repeatMovement(true);
      this.fishs[4].repeatMovement(true);

      this.fishs.push(new Fish(this, '0011223344556677000000000000000000000000', true));
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

    food.on('pointerdown', () => {
      this.fishsInLove();

      // if (NeoFetcher.userWallet) {
      //
      // } else {
      //   document.querySelector("#loginWindow").style.visibility = 'visible';
      // }
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
        // await NeoFetcher.login(
        //   document.querySelector("#passphrase").value,
        //   document.querySelector("#encryptedKey").value)

        // NeoFetcher.feedReef();
      } catch (e) {

      }
    });

    document.querySelector("#closeLogin").addEventListener("click", async (e) => {
      document.querySelector("#loginWindow").style.visibility = 'hidden';
    });
  }
}
