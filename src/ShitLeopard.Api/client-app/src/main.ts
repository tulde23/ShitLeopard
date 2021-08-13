import CustomFilters from '@/plugins/filters';
import {
  DataService,
  Helper,
  HttpService,
  SearchService,
  MetricService,
  SeasonService,
  EpisodeService,
  ShowService,
} from './services';
import Vue from 'vue';
import TextHighlight from 'vue-text-highlight';
import ViewContent from './components/content/ViewContent.vue';
import ViewHeader from './components/header/ViewHeader.vue';
import SiteNavigation from './components/navigation/SiteNavigation.vue';

import '@babel/polyfill';

import App from './App.vue';
import vuetify from './plugins/vuetify';
import router from './router';
import store from './store';

const classList = [
  'd-flex',
  'flex-column',
  'flex-grow-1',
  'justify-start',
  'align-stretch',
  'align-self-stretch',
  'align-content-stretch',
];
Vue.component('text-highlight', TextHighlight);
Vue.directive('force-top', {
  inserted: (el: Element) => {
    classList.map((x) => el.classList.add(x));
  },
});
Vue.component('sl-content', ViewContent);
Vue.component('sl-header', ViewHeader);
Vue.component('sl-nav', SiteNavigation);
Vue.config.productionTip = false;
const httpService = new HttpService(store);
Vue.prototype.$helper = new Helper();
Vue.prototype.$http = httpService;
Vue.prototype.$api = new DataService(store, httpService);
Vue.prototype.$metrics = new MetricService(store, httpService);
Vue.prototype.$search = new SearchService(store, httpService);
Vue.prototype.$episodes = new EpisodeService(store, httpService);
Vue.prototype.$seasons = new SeasonService(store, httpService);
Vue.prototype.$shows = new ShowService(store, httpService);

Vue.use(CustomFilters);
new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount('#app');
