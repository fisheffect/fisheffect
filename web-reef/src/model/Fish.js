import NeoFetcher from "../neoFetcher";

export default class PlayScene {


  constructor (scene, hash, fromCenter) {
    this.dead = false;

    this.scene = scene;
    this.repeatIsLeftToRight = Math.random() >= 0.5;
    let fishCenterX = this.repeatIsLeftToRight ? -150 : (this.scene.game.canvas.width + 150);
    let fishCenterY = this.randomY();

    if (fromCenter) {
      fishCenterX = this.scene.game.canvas.width / 2;
      fishCenterY = 350;
    }

    const normalizedDna = this.normalizeDna(hash);
    this.fishParts = [];
    this.tweens = [];
    this.timeouts = [];

    this.scene.anims.create({
      key: `fish${normalizedDna.tail}-tail-anim`,
      frames: this.scene.anims.generateFrameNumbers(`fish${normalizedDna.tail}-tail`, {
        start: 0,
        end: 1
      }),
      repeat: -1,
      frameRate: 10
    });

    const tail = this.scene.add.sprite(fishCenterX, fishCenterY, `fish${normalizedDna.tail}-tail-anim`);
    tail.anims.play(`fish${normalizedDna.tail}-tail-anim`);

    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${normalizedDna.topbottomfins}-topbottomfins`));
    this.fishParts.push(tail);
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${normalizedDna.back}-back`));
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${normalizedDna.belly}-belly`));
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${normalizedDna.head}-${!normalizedDna.carnivorous ? 'head' : 'head_carnivorous'}`));
    this.fishParts.push(this.scene.add.image(fishCenterX, fishCenterY, `fish${normalizedDna.sidefin}-sidefin`));

    this.fishParts.forEach(image => {
      const scale = (0.25 * (normalizedDna.size / 8)) + 0.07;
      image.scaleX = scale;
      image.scaleY = scale;
    });

    this.repeatMovement(fromCenter);
  }

  repeatMovement(withoutDelay) {
    this.moveTo(
      this.repeatIsLeftToRight ? (this.scene.game.canvas.width + 150) : -150,
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
      x > screenCenter ? (this.scene.game.canvas.width + 150) : -150,
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
          (this.scene.game.canvas.width / 2) - 150,
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
          (this.scene.game.canvas.width / 2) + 150,
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

  stop() {
    this.tweens.forEach(t => t.pause());
    this.tweens = [];

    this.timeouts.forEach(t => clearTimeout(t));
    this.timeouts = [];
  }

  randomY() {
    return Math.floor(Math.random() * this.scene.game.canvas.height);
  }

  normalizeDna(hash) {
    if (!hash) {
      return;
    }
    const asBytes = this.parseHexString(hash);
    return {
      carnivorous: asBytes[0] % 8 === 7,
      size: asBytes[1] % 8,
      belly: asBytes[2] % 8,
      back: asBytes[3] % 8,
      topbottomfins: asBytes[4] % 8,
      head: asBytes[5] % 8,
      tail: asBytes[6] % 8,
      sidefin: asBytes[7] % 8
    };
  }

  parseHexString(hex) {
    const bytes = [];
    for (let c = 0; c < hex.length; c += 2) {
      bytes.push(parseInt(hex.substr(c, 2), 16));
    }
    return bytes;
  }
}
