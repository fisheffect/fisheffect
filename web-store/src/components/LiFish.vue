<template>
  <div class="LiFish verti items-center p-8">
    <fish-img class="w-full" :hash="hash" :fishHeight="150"/>
    <h1 class="mb-2 w-full h-100">{{ hash }}</h1>
    <span class="value">FF$ {{ value }}</span>
  </div>
</template>

<script>
import FishImg from '@/components/FishImg.vue';

export default {
  name: 'liFish',
  components: {
    FishImg,
  },
  props: ['hash', 'value'],
};
</script>

<style lang="scss">
  @import "@/assets/variables.scss";

  .LiFish {
    border: 1px solid $mediumgray;
    border-radius: 8px;

    h1 {
      font-size: 16px;
      color: $primaryDark;
      word-wrap: break-word;
    }

    .value {
      font-size: 12px;
      color: $accent;
    }
  }
</style>

