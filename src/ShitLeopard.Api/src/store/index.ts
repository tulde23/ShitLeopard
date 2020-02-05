import Vue from 'vue';
import Vuex from 'vuex';
import { InitState } from '@/store/state';
import getters from '@/store/getters';
import actions from '@/store/actions';
import mutations from '@/store/mutations';

Vue.config.devtools = true;
Vue.use(Vuex);

const store = new Vuex.Store({
  state: InitState,
  getters,
  actions,
  mutations
});

export default store;
