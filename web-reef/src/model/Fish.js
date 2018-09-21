import NeoFetcher from "../neoFetcher";
import FishByteArray from './FishByteArray';

export default class PlayScene {


  constructor (scene, bytes, fromCenter) {
    this.scene = scene;
    this.bytes = bytes;
    this.dead = false;

    this.indexOfPropCarnivorous = 0;
    this.indexOfPropSize = 1;
    this.indexOfPropBelly = 2;
    this.indexOfPropBack = 3;
    this.indexOfPropTopBottomFins = 4;
    this.indexOfPropHead = 5;
    this.indexOfPropTail = 6;
    this.indexOfPropSideFin = 7;

    this.repeatIsLeftToRight = Math.random() >= 0.5;
    this.fishParts = [];
    this.tweens = [];
    this.timeouts = [];

    this.initRenderAndAnimation(fromCenter);
  }

  initRenderAndAnimation(fromCenter) {
    let fishCenterX = this.repeatIsLeftToRight ? -200 : (this.scene.game.canvas.width + 200);
    let fishCenterY = this.randomY();

    if (fromCenter) {
      fishCenterX = this.scene.game.canvas.width / 2;
      fishCenterY = 350;
    }

    this.scene.anims.create({
      key: `fish${this.getPropTail()}-tail-anim`,
      frames: this.scene.anims.generateFrameNumbers(`fish${this.getPropTail()}-tail`, {
        start: 0,
        end: 1
      }),
      repeat: -1,
      frameRate: 10
    });

    const tail = this.scene.add.sprite(fishCenterX, fishCenterY, `fish${this.getPropTail()}-tail-anim`);
    tail.anims.play(`fish${this.getPropTail()}-tail-anim`);

    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${this.getPropTopBottomFins()}-topbottomfins`));
    this.fishParts.push(tail);
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${this.getPropBack()}-back`));
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${this.getPropBelly()}-belly`));
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${this.getPropHead()}-${!this.getPropCarnivorous() ? 'head' : 'head_carnivorous'}`));
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${this.getPropSideFin()}-sidefin`));

    this.fishParts.forEach(image => {
      const scale = (0.25 * (this.getPropSize() / 8)) + 0.07;
      image.scaleX = scale;
      image.scaleY = scale;
    });

    this.repeatMovement(fromCenter);
  }

  moveTo(x, y, duration, delay, callback) {
    this.stop();

    this.timeouts.push(setTimeout(() => {
      this.fishParts.forEach((image, i) => {
        image.flipX = image.x < x;

        this.tweens.push(this.scene.tweens.add({
          targets: image,
          x,
          y,
          duration,
          onComplete: i != 0 ? null : () => {
            callback && callback();
          }
        }));
      });
    }, delay));
  }

  stop() {
    this.tweens.forEach(t => t.pause());
    this.tweens = [];

    this.timeouts.forEach(t => clearTimeout(t));
    this.timeouts = [];
  }

  randomY() {
    return Math.floor(Math.random() * this.scene.game.canvas.height);
  }

  range(hash, index, count) {
    return hash.slice(index, index + count);
  }

  repeatMovement(withoutDelay) {
    this.moveTo(
      this.repeatIsLeftToRight ? (this.scene.game.canvas.width + 200) : -200,
      this.randomY(),
      Math.floor(Math.random() * 18000) + 6000,
      withoutDelay ? 20 : Math.floor(Math.random() * 9000),
      () => {
        this.repeatIsLeftToRight = !this.repeatIsLeftToRight;
        if (!this.dead) {
          this.repeatMovement();
        }
      });
  }

  disperse() {
    const firstPart = this.fishParts[0];
    const x = firstPart.x;
    const screenCenter = this.scene.game.canvas.width / 2;

    this.moveTo(
      x > screenCenter ? (this.scene.game.canvas.width + 200) : -200,
      this.randomY(),
      1000,
      10);
  }

  gonnaBeDaddy(callback) {
    this.moveTo(
      (this.scene.game.canvas.width / 2) - 300,
      300,
      1000,
      10,
      () => {
        this.moveTo(
          (this.scene.game.canvas.width / 2) - 200,
          300,
          1000,
          10,
          () => this.hearts(callback));
      });
  }

  gonnaBeMommy() {
    this.moveTo(
      (this.scene.game.canvas.width / 2) + 300,
      300,
      1000,
      10,
      () => {
        this.moveTo(
          (this.scene.game.canvas.width / 2) + 200,
          300,
          1000,
          10);
      });
  }

  middleAndDie() {
    this.moveTo(
      this.scene.game.canvas.width / 2,
      400,
      1000,
      10,
      () => this.die());
  }

  die() {
    this.dead = true;
    const firstPart = this.fishParts[0];
    const x = firstPart.x;
    const y = firstPart.y;
    const scale = firstPart.scaleX;

    this.scene.anims.create({
      key: 'fishExplosionAnim',
      frames: this.scene.anims.generateFrameNumbers('fishExplosion', {
        start: 0,
        end: 2
      }),
      frameRate: 10
    });


    const explosion = this.scene.add.sprite(x, y, 'fishExplosionAnim');
    explosion.anims.play('fishExplosionAnim');

    explosion.scaleX = scale;
    explosion.scaleY = scale;

    explosion.on('animationcomplete', () => {
      explosion.destroy();

      this.deadFishFalling(x, y, scale);
    });

    this.fishParts.forEach(image => {
      image.destroy();
    });
  }

  deadFishFalling(x, y, scale) {
    this.scene.anims.create({
      key: 'fishDeadAnim',
      frames: this.scene.anims.generateFrameNumbers('fishDead', {
        start: 0,
        end: 1
      }),
      repeat: -1,
      frameRate: 3
    });

    const dead = this.scene.add.sprite(x, y, 'fishDeadAnim');
    dead.anims.play('fishDeadAnim');

    dead.scaleX = scale;
    dead.scaleY = scale;

    this.scene.tweens.add({
      targets: dead,
      y: this.scene.game.canvas.height + 300,
      duration: 10000,
      onComplete: () => {
        dead.destroy();
      }
    });
  }

  hearts(callback) {
    this.scene.anims.create({
      key: 'heartAnim',
      frames: this.scene.anims.generateFrameNumbers('heart', {
        start: 0,
        end: 12
      }),
      repeat: 2,
      frameRate: 12
    });


    const heart = this.scene.add.sprite(this.scene.game.canvas.width / 2, 300, 'heartAnim');
    heart.anims.play('heartAnim');

    heart.scaleX = 0.4;
    heart.scaleY = 0.4;

    heart.on('animationcomplete', () => {
      heart.destroy();

      callback && callback();
    });
  }

// region META GETTERS AND SETTERS

  getBirthBlockHeight()
  {
    return this.range(this.bytes, FishByteArray.getIndexOfBirthBlockHeight(), FishByteArray.sizeOfBirthBlockHeight());
  }

  getQuantityOfFeeds()
  {
    return this.range(this.bytes, FishByteArray.getIndexOfQuantityOfFeeds(), FishByteArray.sizeOfQuantityOfFeeds());
  }

  getFedWithFishBlockHeight()
  {
    return this.range(this.bytes, FishByteArray.getIndexOfFedWithFishBlockHeight(), FishByteArray.sizeOfFedWithFishBlockHeight());
  }

// endregion

// region DNA PROPS GETTERS AND SETTERS

  getPropCarnivorous()
  {
    return this.range(this.bytes, this.indexOfPropCarnivorous, 1)[0] % 8 === 0;
  }

  getPropSize()
  {
    return this.range(this.bytes, this.indexOfPropSize, 1) % 8;
  }

  getPropBelly()
  {
    return this.range(this.bytes, this.indexOfPropBelly, 1) % 8;
  }

  getPropBack()
  {
    return this.range(this.bytes, this.indexOfPropBack, 1) % 8;
  }

  getPropTopBottomFins()
  {
    return this.range(this.bytes, this.indexOfPropTopBottomFins, 1) % 8;
  }

  getPropHead()
  {
    return this.range(this.bytes, this.indexOfPropHead, 1) % 8;
  }

  getPropTail()
  {
    return this.range(this.bytes, this.indexOfPropTail, 1) % 8;
  }

  getPropSideFin()
  {
    return this.range(this.bytes, this.indexOfPropSideFin, 1) % 8;
  }

// endregion


}
