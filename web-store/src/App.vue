<template>
  <div id="app">
    <div id="header" class="horiz p-10">
      <router-link to="/"><img src="./assets/logo.png" class="h-110"/></router-link>
      <div class="menu horiz weight-1 items-right-center gutter-20 pr-30">
        <a class="buyFood">Buy Fish Food</a>
        <a class="yourStore">Your Store</a>
      </div>
    </div>
    <router-view/>
  </div>
</template>

<style lang="scss">
  @import "~linelay/build.scss";
  @import "@/assets/variables.scss";

  * {
    font-family: sans-serif;
    box-sizing: border-box;
  }

  body {
    margin: 0;
  }

  a {
    cursor: pointer;
  }

  button {
    cursor: pointer;
    border-radius: 15px;
    border: none;
    background: $primaryDark;
    color: white;
    font-weight: bold;

    &:hover {
      background: white;
      color: $primaryDark;
    }
  }

  hr {
    height: 1px;
    border: 0;
    border-top: 1px solid $lightgray;
    padding: 0;
  }

  #app {
    @include verti();
    @include h-window();
  }

  #header {
    box-shadow: 0 2px 4px 0px rgba(0,0,0,0.2);
    position: relative;

    .menu {

      a {
        background-position: top center;
        background-repeat: no-repeat;
        padding-top: 84px;
        font-size: 14px;
        color: $primaryDark;
      }

      .buyFood {
        background-image: url('assets/food.png');
      }

      .yourStore {
        background-image: url('assets/store.png');
      }

    }
  }

</style>
