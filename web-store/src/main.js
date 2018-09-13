import Vue from 'vue';
import axios from 'axios';
import App from './App.vue';
import router from './router';
import $neoFetcher from './neoFetcher';

Vue.config.productionTip = false;

const $axios = axios.create({
  baseURL: 'http://localhost:58699/api/',
});

Object.assign(Vue.prototype, { $axios });
Object.assign(Vue, { $axios });

Object.assign(Vue.prototype, { $neoFetcher });
Object.assign(Vue, { $neoFetcher });

new Vue({
  router,
  render: h => h(App),
}).$mount('#app');
