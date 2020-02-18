import * as ACTIONS from './mutation-types';
import { State } from './state';

const mutations = {
  [ACTIONS.EXECUTING](state: State) {
    state.isBusy = true;
  },
  [ACTIONS.COMPLETE](state) {
    state.isBusy = false;
  },
  [ACTIONS.ERROR](state, e) {
    state.isBusy = false;
    if (e && e.status === 401) {
      this._vm.$eventHub.$emit('authError', `An Exception Occurred. ${e}`);
    }
  },
  [ACTIONS.SET_EPISODE](state, e) {
    state.episode = e;
  },
  [ACTIONS.SET_EPISODES](state, e) {
    state.episodes = e;
  },
  [ACTIONS.SET_ANSWER](state, e) {
    console.log('setting answer', e);
    state.answer = e;
  },
  [ACTIONS.SET_LINES](state, e) {
    console.log('setting lines', e);
    state.lines = e || [];
  },
  [ACTIONS.SET_CHARACTERS](state, e) {
    state.characters = e || [];
  },
  [ACTIONS.SET_QUOTE](state, e) {
    state.quote = e;
  }
};

export default mutations;
