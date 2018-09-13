import { Scene } from 'phaser'
import bg from '../assets/bg.jpg'
import rightrocks from '../assets/rightrocks.png'
import leftrocks from '../assets/leftrocks.png'
import food from '../assets/food.png'
import store from '../assets/store.png'
import fishExplosion from '../assets/fishExplosion.png'
import fishDead from '../assets/fishDead.png'
import heart from '../assets/heart.png'

export default class BootScene extends Scene {
  constructor () {
    super({ key: 'BootScene' })
  }

  preload () {
    this.load.image('bg', bg);
    this.load.image('rightrocks', rightrocks);
    this.load.image('leftrocks', leftrocks);
    this.load.image('food', food);
    this.load.image('store', store);
    this.load.spritesheet('fishExplosion', `../assets/fishExplosion.png`, {
      frameWidth: 1350, frameHeight: 854, endFrame: 2});
    this.load.spritesheet('fishDead', `../assets/fishDead.png`, {
      frameWidth: 1350, frameHeight: 854, endFrame: 1});
    this.load.spritesheet('heart', '../assets/heart.png', {
      frameWidth: 250, frameHeight: 250, endFrame: 12});
    this.preloadFishs(8, ['back', 'belly', 'head', 'head_carnivorous', 'sidefin', 'tail', 'topbottomfins']);
  }

  create () {
    this.scene.start('PlayScene')
  }

  preloadFishs(numberOfFishs, fishParts) {
    Array(numberOfFishs).fill().forEach((_, i) => {
      fishParts.forEach((name) => {
        if (name !== 'tail') {
          this.load.image(`fish${i}-${name}`, require(`../assets/fish${i}-${name}.png`));
        } else {
          this.load.spritesheet(`fish${i}-tail`, `../assets/fish${i}-tail.png`, {
            frameWidth: 1350, frameHeight: 854, endFrame: 1});
        }
      })
    })
  }
}
