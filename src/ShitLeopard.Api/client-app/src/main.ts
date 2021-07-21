import CustomFilters from '@/plugins/filters';
import { DataService, Helper, HttpService } from '@/services';
import Vue from 'vue';
import TextHighlight from 'vue-text-highlight';

import '@babel/polyfill';

import App from './App.vue';
import vuetify from './plugins/vuetify';
import router from './router';
import store from './store';

Vue.component('text-highlight', TextHighlight);

Vue.config.productionTip = false;
const httpService = new HttpService(store);
Vue.prototype.$helper = new Helper();
Vue.prototype.$http = httpService;
Vue.prototype.$api = new DataService(store, httpService);
Vue.use(CustomFilters);
new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app');
