<template>
  <header>
    <nav>
      <div class="container">

        <div class="brand">
          <div class="btn">
            <img src="../assets/img/logo@3x.png" alt="fish-effect">
          </div>
        </div>

        <div class="slot-1">
          <ul>
            <li>
              <button>
                {{$t('navbar.buyFishCoins')}}
              </button>
            </li>
            <li>
              <button>
                {{$t('navbar.about')}}
              </button>
            </li>
            <li>
              <button>
                {{$t('navbar.contact')}}
              </button>
            </li>
            <li>
              <button>
                {{$t('navbar.theTeam')}}
              </button>
            </li>
            <li>
              <button>
                {{$t('navbar.whitepaper')}}
              </button>
            </li>
            <li>
              <button>
                {{$t('navbar.tutorials')}}
              </button>
            </li>
          </ul>
        </div>

        <div class="slot-2">
          <ul>
            <li>
              <button class="icon">
                <img src="../assets/img/insta.svg" alt="instagram">
              </button>
            </li>
            <li>
              <button class="icon">
                <img src="../assets/img/fb.svg" alt="facebook">
              </button>
            </li>
            <li>
              <button class="icon">
                <img src="../assets/img/linkedin.svg" alt="linkedin">
              </button>
            </li>
            <li>
              <button class="icon">
                <img src="../assets/img/github.svg" alt="github">
              </button>
            </li>
          </ul>
        </div>

        <div class="menu-icon">
          <button class="icon flat" @click="showSidebar">
            <img src="@/assets/img/menu.svg" alt="menu">
          </button>
        </div>

      </div>
    </nav>

    <aside>
      <transition name="fade" mode="out-in">
        <div class="outbound" @click="hideSidebar" v-if="sidebar"></div>
      </transition>

      <transition name="fade-left-big" mode="out-in">
        <nav class="sidebar" v-if="sidebar">
          <div class="brand">
            <div class="btn">
              <img src="../assets/img/logo@3x.png" alt="fish-effect">
            </div>
          </div>

          <div class="slot-1">
            <ul>
              <li>
                <button>
                  {{$t('navbar.buyFishCoins')}}
                </button>
              </li>
              <li>
                <button>
                  {{$t('navbar.about')}}
                </button>
              </li>
              <li>
                <button>
                  {{$t('navbar.contact')}}
                </button>
              </li>
              <li>
                <button>
                  {{$t('navbar.theTeam')}}
                </button>
              </li>
              <li>
                <button>
                  {{$t('navbar.whitepaper')}}
                </button>
              </li>
              <li>
                <button>
                  {{$t('navbar.tutorials')}}
                </button>
              </li>
            </ul>

          </div>

          <div class="slot-2">
            <ul>
              <li>
                <button class="icon">
                  <img src="../assets/img/insta.svg" alt="instagram">
                </button>
              </li>
              <li>
                <button class="icon">
                  <img src="../assets/img/fb.svg" alt="facebook">
                </button>
              </li>
              <li>
                <button class="icon">
                  <img src="../assets/img/linkedin.svg" alt="linkedin">
                </button>
              </li>
              <li>
                <button class="icon">
                  <img src="../assets/img/github.svg" alt="github">
                </button>
              </li>
            </ul>
          </div>
        </nav>
      </transition>
    </aside>

  </header>
</template>

<script lang="ts">
  import {Component, Prop, Watch, Vue} from 'vue-property-decorator'
  import {Action, Getter} from 'vuex-class'

  @Component
  export default class Navbar extends Vue {
    sidebar = false

    hideSidebar() {
      this.sidebar = false
    }

    showSidebar() {
      this.sidebar = true
    }
  }
</script>
