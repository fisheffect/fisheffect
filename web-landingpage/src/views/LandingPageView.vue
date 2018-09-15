<template>
  <div>
    <section class="main">
      <div class="container verti items-center">
        <div class="row horiz items-center" style="flex-direction: row-reverse">
          <div class="col">
            <div class="w-250 pal-black-50 bg p-20 text-center rounded mb-20">
              <div class="display huge pal-robin-egg-blue text text-uppercase mb-10">
                Limited Offer
              </div>

              <p class="pal-white text mb-10">
                1 GAS = 500 + 10%
              </p>

              <p class="pal-light-sage text mb-10">
                This is NOT a Real ICO
                See below
              </p>

              <img src="../assets/img/fishcoin@3x.png" alt="Fish Coin" class="w-100 mb-10">

              <button class="primary">
                Buy Fish Coins
              </button>
            </div>
          </div>

          <div class="col weight-1 tab-w-full mob-w-full">
            <div class="horiz items-center">
              <div class="des-w-400 tab-w-300 mob-w-200">
                <img src="../assets/img/logo@3x.png" alt="Fish Effect" class="w-full">
              </div>
            </div>
          </div>

          <div class="col desktop">
            <div class="w-250"></div>
          </div>
        </div>

        <h1 class="des-w-600 w-full shadow">
          Be the hero of the oceans!
          Make your own blockchain aquarium
        </h1>

        <div class="row horiz items-center">
          <img src="../assets/img/neo@3x.png" alt="NEO" class="w-150">
        </div>
      </div>

      <div class="bg-contrast"></div>
    </section>

    <section class="tutorials">
      <div class="container">
        <span>
          This is not a real ICO. This is a tutorial for blockchain minds. Go to the tutorials
        </span>
        <i class="fa fa-arrow-circle-right"></i>
      </div>
    </section>

    <section class="about">
      <div class="container">
        <div class="verti items-center">

          <div class="mb-30">
            <h2 class="shadow">About</h2>

            <h3 class="shadow des-w-600 w-full">
              The first ever Blockchain aquarium!
              Dive into the Neo Blockchain and learn everything you need
              to make your aquarium!
            </h3>
          </div>

          <div class="row horiz items-space-around w-full">
            <div class="col mob-w-full">
              <div class="mb-10">
                <img src="../assets/img/petshop.svg" alt="Pet Shop" class="h-100 mb-10">

                <p class="des-w-200 w-full">
                  Buy and Sell fishes!
                </p>
              </div>
            </div>

            <div class="col mob-w-full">
              <div class="mb-10">
                <img src="../assets/img/fishgreen@3x.png" alt="Fish Green" class="h-100 mb-10">

                <p class="des-w-200 w-full">
                  More than 2 million
                  Fish Design Possibilities!
                </p>
              </div>
            </div>

            <div class="col mob-w-full">
              <div class="mb-10">
                <img src="../assets/img/fishfood@3x.png" alt="Fish Food" class="h-100 mb-10">

                <p class="des-w-200 w-full">
                  Feed the
                  Fish
                </p>
              </div>
            </div>
          </div>

        </div>
      </div>
    </section>

    <section class="whitepaper">
      <div class="container">
        <h2>
          WhiteFishPaper
        </h2>

        <img src="../assets/img/whitepaper.svg" alt="White Paper" class="w-100 mb-20">

        <p>
          <a class="text-link">
            Download the Whitepaper
          </a>
        </p>
      </div>
    </section>

    <section class="buy">
      <div class="container">
        <div class="row verti items-center">

          <h2>Buy Fish Coins and help us!</h2>

          <h3 class="des-w-600 w-full">
            Fish Coins are used to feed the fish!
            <br>
            Go ahead, have some fun!
          </h3>

          <img src="../assets/img/fishcoin@3x.png" alt="Fish Coin" class="w-200 mb-20">

          <button class="primary">
            Buy Fish Coins
          </button>

        </div>
      </div>
    </section>

    <section class="team">

      <div class="container">

        <div class="row verti items-center">
          <div class="mb-20">
            <h2 class="contrast">The Fish Effect Team</h2>
          </div>

          <div class="row horiz items-space-around w-full">
            <div class="col mob-w-full">
              <div class="mb-10">
                <img src="../assets/img/lingodo@3x.png" alt="Fish" class="h-120">
              </div>

              <p>
                <strong>
                  Lingodo Mares
                </strong>
                <br>
                CEO
              </p>
            </div>

            <div class="col mob-w-full">
              <div class="mb-10">
                <img src="../assets/img/piranoblue@3x.png" alt="Fish" class="h-120 mb-10">
              </div>

              <p>
                <strong>
                  Piraño Blue
                </strong>
                <br>
                CTO
              </p>
            </div>

            <div class="col mob-w-full">
              <div class="mb-10">
                <img src="../assets/img/smthompson@3x.png" alt="Fish" class="h-120 mb-10">
              </div>

              <p>
                <strong>
                  Small Mouth Thompson
                </strong>
                <br>
                CFO
              </p>
            </div>

            <div class="col mob-w-full">
              <div class="mb-10">
                <img src="../assets/img/pinkyshark@3x.png" alt="Fish" class="h-120 mb-10">
              </div>

              <p>
                <strong>
                  Pinky Shark
                </strong>
                <br>
                Graphics
              </p>
            </div>
          </div>

        </div>
      </div>

      <section class="contact">
        <div class="container">
          <div class="row verti items-center">

            <h2>Contact Us!</h2>

            <p>
              Yes! We do have internet in the middle of the ocean.
              <br>
              Send as a message!
            </p>

            <form class="row verti">
              <div class="col horiz">
                <input type="text" class="weight-1" placeholder="Name"
                       v-model="contact.name" required/>
              </div>
              <div class="col horiz">
                <input type="email" class="weight-1" placeholder="E-Mail"
                       v-model="contact.email" required/>
              </div>
              <div class="col horiz">
                <textarea cols="30" class="weight-1" rows="6" placeholder="Message"
                          v-model="contact.message" required></textarea>
              </div>

              <div class="col horiz items-center">
                <button type="submit" class="secondary fluid px-40 my-10">
                  Send
                </button>
              </div>
            </form>

          </div>
        </div>
      </section>
    </section>

    <footer>
      <div class="top-page">
        <button @click="scrollTo('main')">
          <img src="../assets/img/up.svg" alt="up">
        </button>
      </div>

      <div class="simpli">
        Simpli © 2018 // www.simpli.com.br // made in Brazil
      </div>
    </footer>
  </div>
</template>

<script lang="ts">
  import {Component, Prop, Watch, Vue} from 'vue-property-decorator'
  import {Getter, Action} from 'vuex-class'

  const WOW = require('wow.js')

  @Component
  export default class LandingPageView extends Vue {
    @Action('queryLang') queryLang?: Function
    @Getter('year') year?: number
    @Getter('language') language?: number

    currentSection: string | null = null
    sections: string[] = [
      'ico',
      'about',
      'contact',
      'github',
      'donate',
    ]

    contact = {
      name: null,
      email: null,
      message: null,
    }

    async sendMessage() {
      /**/
    }

    scrollEvent() {
      const scrollPos = document.body.scrollTop || window.scrollY

      for (const section of this.sections) {
        const el = this.$refs[section] as HTMLElement

        if (!el) continue

        const height = el.offsetHeight
        const top = el.offsetTop - screen.height / 2
        const bottom = el.offsetTop + height - screen.height / 2

        if (scrollPos > top && scrollPos < bottom) {
          this.currentSection = section
          break
        } else this.currentSection = null
      }
    }

    created() {
      this.scrollEvent()
    }

    mounted() {
      if (window.innerWidth > 768) new WOW().init()
      window.addEventListener('scroll', this.scrollEvent)
    }

    beforeDestroy() {
      window.removeEventListener('scroll', this.scrollEvent)
    }
  }
</script>
