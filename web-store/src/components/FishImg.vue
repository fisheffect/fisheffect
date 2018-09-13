<template>
  <div class="verti items-center" :style="{ height: `${fishHeight}px` }">
    <img :src="`fish/fish${fish.topBottomFins}-topbottomfins.png`"
         :style="{ width: `${fishImageSize}px` }" class="absolute"/>
    <img :src="`fish/fish${fish.tail}-tail.png`"
         :style="{ width: `${fishImageSize}px` }" class="absolute"/>
    <img :src="`fish/fish${fish.back}-back.png`"
         :style="{ width: `${fishImageSize}px` }" class="absolute"/>
    <img :src="`fish/fish${fish.belly}-belly.png`"
         :style="{ width: `${fishImageSize}px` }" class="absolute"/>
    <img :src="`fish/fish${fish.head}-${!fish.carnivorous ? 'head' : 'head_carnivorous'}.png`"
         :style="{ width: `${fishImageSize}px` }" class="absolute"/>
    <img :src="`fish/fish${fish.sideFin}-sidefin.png`"
         :style="{ width: `${fishImageSize}px` }" class="absolute"/>
  </div>
</template>

<script>
export default {
  name: 'FishImg',
  props: ['hash', 'fishHeight'],
  data() {
    return {
      fish: {
        topBottomFins: null,
        tail: null,
        back: null,
        belly: null,
        head: null,
        sideFin: null,
        carnivorous: null,
        size: null,
      },
    };
  },

  computed: {
    fishImageSize() {
      const size = this.fish && this.fish.size ? this.fish.size : 1;
      return (this.fishHeight / 3) + ((size / 8) * ((this.fishHeight * 2) / 3));
    },
  },

  mounted() {
    this.normalizeHash();
  },

  methods: {
    normalizeHash() {
      if (!this.hash) {
        return;
      }
      const asBytes = this.parseHexString(this.hash);
      this.fish.carnivorous = asBytes[0] % 8 === 7;
      this.fish.size = asBytes[1] % 8;
      this.fish.belly = asBytes[2] % 8;
      this.fish.back = asBytes[3] % 8;
      this.fish.topBottomFins = asBytes[4] % 8;
      this.fish.head = asBytes[5] % 8;
      this.fish.tail = asBytes[6] % 8;
      this.fish.sideFin = asBytes[7] % 8;
    },
    parseHexString(hex) {
      const bytes = [];
      for (let c = 0; c < hex.length; c += 2) {
        bytes.push(parseInt(hex.substr(c, 2), 16));
      }
      return bytes;
    },
  },
};
</script>
