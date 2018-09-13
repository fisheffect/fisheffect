<template>
  <div class="FishDetails verti weight-1">
    <div class="reefArea horiz h-full">
      <fish-img
          :hash="hash"
          :fishHeight="500"
          class="weight-1"/>
      <div class="info verti h-140 m-5 p-10">
        <h1 class="m-0 mb-10">{{ hash }}</h1>
        <div class="bornWhen text-center">Born 4 blocks ago</div>
        <div class="eatWhat text-center">Ate 7 times, last time was 1 block ago</div>
        <div class="horiz self-center items-center mt-20">
          <div class="price mr-20">FF$ 5</div>
          <button class="w-100 h-30">Buy</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import FishImg from '@/components/FishImg.vue';

export default {
  name: 'fishDetails',
  components: {
    FishImg,
  },
  props: ['hash'],
  data() {
    return {
      fish: {
        hashFish: null,
      },
    };
  },

  mounted() {
    this.load();
  },

  methods: {
    async load() {
      // this.$neoFetcher.login();
    },
  },
};
</script>
<style lang="scss">
  @import "@/assets/variables.scss";

  .reefArea {
    background: url('../assets/reefbg.jpg') center center no-repeat;
    background-size: cover;

    .info {
      color: white;
      background: linear-gradient(to bottom,
          rgba(255,255,255,0.6) 0%,
          rgba(255,255,255,0.2) 40%,
          rgba(255,255,255,0.2) 100%);
      border: 1px solid rgba(255, 255, 255, 0.4);
      border-radius: 8px;
      box-shadow: 0 2px 4px 1px rgba(0,0,0,0.4);

      h1 {
        font-size: 20px;
        color: $primaryDark;
        word-wrap: break-word;
      }

      .eatWhat {
        font-size: 13px;
      }

      .price {
        font-size: 13px;
        font-style: italic;
      }
    }
  }
</style>
