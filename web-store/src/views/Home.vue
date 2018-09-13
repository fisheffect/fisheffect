<template>
  <div class="Home verti m-20">
    <div class="horiz mb-10">
      <router-link
          v-for="fish in fishs" :key="fish.hashFish"
          :to="`/fish/${fish.hashFish}`" class="fishLink">
        <li-fish
          :hash="fish.hashFish"
          :value="fish.value"
          class="w-150 m-10"/>
      </router-link>
    </div>
    <div class="self-right mb-20">
      <a @click="previousPage" class="paginator p-10 ml-10"
         v-if="fishsCurrentPage > 0">Previous Page</a>
      <a @click="nextPage" class="paginator p-10 ml-10"
        v-if="fishs.length == 10">Next Page</a>
    </div>
    <hr class="w-full"/>
  </div>
</template>

<script>
import LiFish from '@/components/LiFish.vue';

export default {
  name: 'home',
  components: {
    LiFish,
  },
  data() {
    return {
      fishs: [],
      fishsCurrentPage: 0,
    };
  },

  mounted() {
    this.load();
  },

  methods: {
    async load() {
      const resp = await this.$axios.get('/FishForSale', { params: { page: this.fishsCurrentPage } });
      this.fishs = resp.data;
    },
    previousPage() {
      this.fishsCurrentPage = this.fishsCurrentPage - 1;
      this.load();
    },
    nextPage() {
      this.fishsCurrentPage = this.fishsCurrentPage + 1;
      this.load();
    },
  },
};
</script>
<style lang="scss">
  @import "@/assets/variables.scss";

  .paginator {
    font-size: 12px;
    color: $accent;
    background: $lightBackground;
    border-radius: 8px;
  }

  .fishLink {
    text-decoration: none;
  }
</style>
