import actions from '@/store/actions';
import getters from '@/store/getters';
import mutations from '@/store/mutations';
import { InitState } from '@/store/state';
import Vue from 'vue';
import Vuex from 'vuex';

Vue.config.devtools = true;
Vue.use(Vuex);

const store = new Vuex.Store({
  state: InitState,
  getters,
  actions,
  mutations
});

export default store;
