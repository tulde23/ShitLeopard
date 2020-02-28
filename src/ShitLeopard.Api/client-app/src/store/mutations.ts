import {
  Character,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Tag
} from '@/viewModels';

import * as ACTIONS from './mutation-types';
import { State } from './state';

const mutations = {
  [ACTIONS.EXECUTING](state: State) {
    state.isBusy = true;
  },
  [ACTIONS.SET_REFRESH](state: State, e: boolean) {
    state.timeToRefresh = e;
  },
  [ACTIONS.COMPLETE](state: State) {
    state.isBusy = false;
  },
  [ACTIONS.ERROR](state: State, e: any) {
    state.isBusy = false;
  },
  [ACTIONS.SET_EPISODE](state: State, e: Episode) {
    state.episode = e;
  },
  [ACTIONS.SET_EPISODES_GROUPED](state: State, e: EpisodeGroup[]) {
    state.groupedEpisodes = e;
  },
  [ACTIONS.SET_EPISODES](state: State, e: Episode[]) {
    state.episodes = e;
  },
  [ACTIONS.SET_ANSWER](state: State, e: any) {
    console.log('setting answer', e);
    state.answer = e;
  },
  [ACTIONS.SET_LINES](state: State, e: ScriptLine[]) {
    console.log('setting lines', e);
    state.lines = e || [];
  },
  [ACTIONS.SET_CHARACTERS](state: State, e: Character[]) {
    state.characters = e || [];
  },
  [ACTIONS.SET_QUOTE](state: State, e: Quote) {
    state.quote = e;
  },
  [ACTIONS.SET_TAGS](state: State, e: Tag[]) {
    state.tags = e;
  }
};

export default mutations;
