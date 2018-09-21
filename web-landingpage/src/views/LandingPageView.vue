<template>
  <div>
    <section ref="main" class="main">
      <div class="container verti items-center">
        <div class="row horiz items-center" style="flex-direction: row-reverse">
          <div class="col">
            <div class="wow fadeInDownBig w-250 pal-black-50 bg p-20 text-center rounded mb-20">
              <div class="display huge pal-robin-egg-blue text text-uppercase mb-10">
                {{$t('view.landingPage.main.panelTitle')}}
              </div>

              <p class="pal-white text mb-10">
                {{$t('view.landingPage.main.panelText1')}}
              </p>

              <p class="pal-light-sage text mb-10">
                {{$t('view.landingPage.main.panelText2')}}
              </p>

              <img src="../assets/img/fishcoin@3x.png" alt="Fish Coin" class="w-100 mb-10">

              <button class="special">
                <!--TODO: 'Buy Fish Coins' Link-->
                {{$t('view.landingPage.main.buyFishCoins')}}
              </button>
            </div>
          </div>

          <div class="col weight-1 tab-w-full mob-w-full">
            <div class="horiz items-center">
              <div class="wow fadeInUpBig des-w-400 tab-w-300 mob-w-200">
                <img src="../assets/img/logo@3x.png" alt="Fish Effect" class="w-full">
              </div>
            </div>
          </div>

          <div class="col desktop">
            <div class="w-250"></div>
          </div>
        </div>

        <h1 class="wow fadeInLeftBig des-w-600 w-full shadow">
          {{$t('view.landingPage.main.title')}}
        </h1>

        <div class="wow fadeInRightBig row horiz items-center">
          <img src="../assets/img/neo@3x.png" alt="NEO" class="w-150">
        </div>
      </div>

      <div class="bg-contrast"></div>
    </section>

    <router-link tag="section" class="tutorials" to="/tutorials">
      <div class="container">
      <span>
        {{$t('view.landingPage.tutorials.title')}}
      </span>

        <i class="fa fa-arrow-circle-right"></i>
      </div>
    </router-link>

    <section ref="about" class="about">
      <div class="container">
        <div class="verti items-center">

          <div class="mb-30">
            <h2 class="shadow">
              {{$t('view.landingPage.about.title')}}
            </h2>

            <h3 class="shadow des-w-600 w-full">
              {{$t('view.landingPage.about.subtitle1')}}
              {{$t('view.landingPage.about.subtitle2')}}
            </h3>
          </div>

          <div class="row horiz items-space-around w-full">
            <div class="col mob-w-full">
              <div class="wow zoomIn mb-10">
                <img src="../assets/img/petshop.svg" alt="Pet Shop" class="h-100 mb-10">

                <p class="des-w-200 w-full">
                  {{$t('view.landingPage.about.feature1')}}
                </p>
              </div>
            </div>

            <div class="col mob-w-full">
              <div class="wow zoomIn mb-10">
                <img src="../assets/img/fishgreen@3x.png" alt="Fish Green" class="h-100 mb-10">

                <p class="des-w-200 w-full">
                  {{$t('view.landingPage.about.feature2')}}
                </p>
              </div>
            </div>

            <div class="col mob-w-full">
              <div class="wow zoomIn mb-10">
                <img src="../assets/img/fishfood@3x.png" alt="Fish Food" class="h-100 mb-10">

                <p class="des-w-200 w-full">
                  {{$t('view.landingPage.about.feature3')}}
                </p>
              </div>
            </div>
          </div>

        </div>
      </div>
    </section>

    <section ref="whitepaper" class="whitepaper">
      <div class="container">
        <h2>
          {{$t('view.landingPage.whitepaper.title')}}
        </h2>

        <div class="wow lightSpeedIn">
          <img src="../assets/img/whitepaper.svg" alt="White Paper" class="w-100 mb-20">

          <p>
            <a class="text-link" :href="$t('view.landingPage.whitepaper.linkHref')" target="_blank">
              {{$t('view.landingPage.whitepaper.linkTitle')}}
            </a>
          </p>
        </div>
      </div>
    </section>

    <section class="buy">
      <div class="container">
        <div class="row verti items-center">

          <h2>
            {{$t('view.landingPage.buy.title')}}
          </h2>

          <h3 class="des-w-600 w-full">
            {{$t('view.landingPage.buy.subtitle1')}}
            <br>
            {{$t('view.landingPage.buy.subtitle2')}}
          </h3>

          <div class="wow zoomIn">
            <img src="../assets/img/fishcoin@3x.png" alt="Fish Coin" class="w-200 mb-20">
          </div>

          <button class="special">
            <!--TODO: 'Buy Fish Coins' Link-->
            {{$t('view.landingPage.buy.buyFishCoins')}}
          </button>

        </div>
      </div>
    </section>

    <section ref="team" class="team">

      <div class="container">

        <div class="row verti items-center">
          <div class="mb-20">
            <h2 class="contrast">
              {{$t('view.landingPage.team.title')}}
            </h2>
          </div>

          <div class="row horiz items-space-around w-full">
            <div class="col mob-w-full">
              <div class="wow zoomIn mb-10">
                <img src="../assets/img/lingodo@3x.png" alt="Fish" class="h-120">
              </div>

              <p>
                <strong>
                  {{$t('view.landingPage.team.fish1.name')}}
                </strong>
                <br>
                {{$t('view.landingPage.team.fish1.role')}}
              </p>
            </div>

            <div class="col mob-w-full">
              <div class="wow zoomIn mb-10">
                <img src="../assets/img/piranoblue@3x.png" alt="Fish" class="h-120 mb-10">
              </div>

              <p>
                <strong>
                  {{$t('view.landingPage.team.fish2.name')}}
                </strong>
                <br>
                {{$t('view.landingPage.team.fish2.role')}}
              </p>
            </div>

            <div class="col mob-w-full">
              <div class="wow zoomIn mb-10">
                <img src="../assets/img/smthompson@3x.png" alt="Fish" class="h-120 mb-10">
              </div>

              <p>
                <strong>
                  {{$t('view.landingPage.team.fish3.name')}}
                </strong>
                <br>
                {{$t('view.landingPage.team.fish3.role')}}
              </p>
            </div>

            <div class="col mob-w-full">
              <div class="wow zoomIn mb-10">
                <img src="../assets/img/pinkyshark@3x.png" alt="Fish" class="h-120 mb-10">
              </div>

              <p>
                <strong>
                  {{$t('view.landingPage.team.fish4.name')}}
                </strong>
                <br>
                {{$t('view.landingPage.team.fish4.role')}}
              </p>
            </div>
          </div>

        </div>
      </div>

      <section class="contact">
        <div class="container">
          <div class="row verti items-center">

            <h2>
              {{$t('view.landingPage.contact.title')}}
            </h2>

            <p>
              {{$t('view.landingPage.contact.subtitle1')}}
              <br>
              {{$t('view.landingPage.contact.subtitle2')}}
            </p>

            <form @submit.prevent="sendMessage" class="row verti">
              <div class="col horiz">
                <label>
                  <input type="text"
                         class="weight-1"
                         :placeholder="$t('view.landingPage.contact.form.name')"
                         v-model="contact.name"
                         required/>
                </label>
              </div>
              <div class="col horiz">
                <label>
                  <input type="email"
                         class="weight-1"
                         :placeholder="$t('view.landingPage.contact.form.email')"
                         v-model="contact.email"
                         required/>
                </label>
              </div>
              <div class="col horiz">
                <label for="input-contact"></label>
                <textarea id="input-contact"
                          cols="30"
                          class="weight-1"
                          rows="6"
                          :placeholder="$t('view.landingPage.contact.form.message')"
                          v-model="contact.message"
                          required></textarea>
              </div>

              <div class="col horiz items-center">
                <button type="submit" class="secondary fluid px-40 my-10">
                  {{$t('view.landingPage.contact.form.send')}}
                </button>
              </div>
            </form>

          </div>
        </div>
      </section>
    </section>

    <footer>
      <div class="top-page">
        <button @click="scrollTo('header')">
          <img src="../assets/img/up.svg" alt="up">
        </button>
      </div>

      <div class="simpli">
        {{$t('view.landingPage.footer.copyright')}}
      </div>
    </footer>
  </div>
</template>

<script lang="ts">
  import {Component, Prop, Watch, Vue} from 'vue-property-decorator'
  import {Getter, Action} from 'vuex-class'
  import {$, success, HttpBody, scrollTo} from '@/simpli'

  const WOW = require('wow.js')

  @Component
  export default class LandingPageView extends Vue {
    @Getter('year') year?: number
    @Getter('language') language?: number
    @Getter('currentSection') currentSection?: string | null

    @Action('queryLang') queryLang?: Function
    @Action('setCurrentSection') setCurrentSection?: Function

    scrollTo = scrollTo

    sections: string[] = [
      'main',
      'about',
      'whitepaper',
      'team',
      'contact',
    ]

    contact = {
      name: null,
      email: null,
      message: null,
    }

    async sendMessage() {
      const payload = {
        name: this.contact.name,
        email: this.contact.email,
        message: `\`\`\`fix\n${this.contact.message}\`\`\``,
        source: 'Sent via Fix Effect LandingPage',
      }

      const options = {
        headers: {
          'Content-Type': 'application/json',
          'Accept-Language': this.language!,
          'x-api-key': process.env.VUE_APP_API_CONTACT_KEY,
        },
      }

      await new HttpBody(Object).POST(process.env.VUE_APP_API_CONTACT_URI || '', payload, options, false)

      this.contact = {
        name: null,
        email: null,
        message: null,
      }

      success('system.success.message')
    }

    scrollEvent() {
      const scrollPos = document.body.scrollTop || window.scrollY

      for (const section of this.sections) {
        const el = this.$refs[section] as HTMLElement

        if (!el) continue

        const height = el.offsetHeight + 90
        const top = el.offsetTop - screen.height / 2
        const bottom = el.offsetTop + height - screen.height / 2

        if (scrollPos > top && scrollPos < bottom) {
          this.setCurrentSection!(section)
          break
        } else this.setCurrentSection!(null)
      }
    }

    created() {
      if (window.innerWidth > 768) new WOW().init()
      this.scrollEvent()
    }

    mounted() {
      window.addEventListener('scroll', this.scrollEvent)
    }

    beforeDestroy() {
      window.removeEventListener('scroll', this.scrollEvent)
    }
  }
</script>
