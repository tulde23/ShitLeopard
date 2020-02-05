import App from '@/App';
import AceEditor from '@/components/Widgets/Ace';
import CustomFilters from '@/plugins/filters';
import {
  FileUploadService,
  Helper,
  HttpService,
  MessageService
} from '@/services';
import store from '@/store';
import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faBlind,
  faChild,
  faCoffee,
  faUsers
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Axios, { AxiosRequestConfig } from 'axios';
import IdleVue from 'idle-vue';
import Vue from 'vue';
import VueCytoscape from 'vue-cytoscape';
import EventHub from 'vue-event-hub';

import './plugins/vuetify';
import '@babel/polyfill';
import 'vue-cytoscape/dist/vue-cytoscape.css';

import router from './router';

// Plugins
const httpService = new HttpService(store);
Vue.prototype.$helper = new Helper();
Vue.prototype.$messageBus = new MessageService();
Vue.prototype.$http = httpService;
Vue.prototype.$upload = new FileUploadService(httpService);

Vue.use(CustomFilters);
Vue.use(EventHub);
// Tell Vue to use the plugi
const helper = new Helper();
Vue.use(VueCytoscape);
Vue.config.productionTip = false;
Vue.prototype.$http = Axios;
const eventsHub = new Vue();
// 15 minute timeout.
Vue.use(IdleVue, {
  eventEmitter: eventsHub,
  idleTime: 900000
});

library.add(faCoffee, faBlind, faChild, faUsers);

Vue.component('font-awesome-icon', FontAwesomeIcon);
Vue.component('ace-editor', AceEditor);

Axios.interceptors.request.use(
  function(request: AxiosRequestConfig) {
    request.withCredentials = true;
    return Promise.resolve(request);
  },
  function(error) {}
);
Axios.interceptors.response.use(
  response => {
    // Do something with response data
    return response;
  },
  error => {
    // Do something with response error

    return Promise.reject(error);
  }
);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
