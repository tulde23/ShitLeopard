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
  }
};

export default mutations;
