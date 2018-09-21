<template>
  <header>
    <nav>
      <div class="container">

        <div class="brand">
          <router-link to="/" class="btn" @click.native="scrollTo('header')">
            <img src="../assets/img/logo@3x.png" alt="fish-effect">
          </router-link>
        </div>

        <div class="slot-1">
          <ul>
            <li>
              <button class="special">
                <!--TODO: 'Buy Fish Coins' Link-->
                {{$t('navbar.buyFishCoins')}}
              </button>
            </li>
            <li>
              <router-link tag="button" to="/tutorials" class="flat primary">
                {{$t('navbar.tutorials')}}
              </router-link>
            </li>
            <li>
              <button @click="scrollTo('.about')" :class="{active: currentSection === 'about'}" class="flat contrast">
                {{$t('navbar.about')}}
              </button>
            </li>
            <li>
              <button @click="scrollTo('.whitepaper')" :class="{active: currentSection === 'whitepaper'}" class="flat contrast">
                {{$t('navbar.whitepaper')}}
              </button>
            </li>
            <li>
              <button @click="scrollTo('.team')" :class="{active: currentSection === 'team'}" class="flat contrast">
                {{$t('navbar.theTeam')}}
              </button>
            </li>
            <li>
              <button @click="scrollTo('.contact')" :class="{active: currentSection === 'team'}" class="flat contrast">
                {{$t('navbar.contact')}}
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
            <router-link to="/" class="btn" @click.native="scrollTo('header'); hideSidebar()">
              <img src="../assets/img/logo@3x.png" alt="fish-effect">
            </router-link>
          </div>

          <div class="slot-1">
            <ul>
              <li>
                <button class="special" @click="hideSidebar()">
                  {{$t('navbar.buyFishCoins')}}
                </button>
              </li>
              <li>
                <router-link tag="button" to="/tutorials" class="flat primary" @click.native="hideSidebar()">
                  {{$t('navbar.tutorials')}}
                </router-link>
              </li>
              <li>
                <button @click="scrollTo('.about'); hideSidebar()" :class="{active: currentSection === 'about'}" class="flat contrast">
                  {{$t('navbar.about')}}
                </button>
              </li>
              <li>
                <button @click="scrollTo('.whitepaper'); hideSidebar()" :class="{active: currentSection === 'whitepaper'}" class="flat contrast">
                  {{$t('navbar.whitepaper')}}
                </button>
              </li>
              <li>
                <button @click="scrollTo('.team'); hideSidebar()" :class="{active: currentSection === 'team'}" class="flat contrast">
                  {{$t('navbar.theTeam')}}
                </button>
              </li>
              <li>
                <button @click="scrollTo('.contact'); hideSidebar()" :class="{active: currentSection === 'team'}" class="flat contrast">
                  {{$t('navbar.contact')}}
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
  import {scrollTo} from '@/simpli'

  @Component
  export default class Navbar extends Vue {
    @Getter('currentSection') currentSection?: string | null

    scrollTo = scrollTo

    sidebar = false

    hideSidebar() {
      this.sidebar = false
    }

    showSidebar() {
      this.sidebar = true
    }
  }
</script>
