import { ShowModel } from '@/models';
import { PagedResult } from '@/models/PagedResult';
import {
  Character,
  DialogModel,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Tag,
} from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { SiteMetric } from '@/viewModels/SiteMetric';

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
  [ACTIONS.SET_QUESTION_ANSWER](state: State, e: QuestionAnswer) {
    state.questionAnswer = e;
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
  },
  [ACTIONS.SET_METRICS](state: State, e: PagedResult<SiteMetric>) {
    state.siteMetrics = e;
  },
  [ACTIONS.SET_DIALOG_LINES](state: State, e: DialogModel[]) {
    state.dialogLines = e;
  },
  [ACTIONS.SET_SELECTED_DIALOG](state: State, e: DialogModel) {
    state.selectedDialog = e;
  },
  [ACTIONS.SET_IS_OPEN](state: State, e: boolean) {
    state.isOpen = e;
  },
  [ACTIONS.SET_HIGHLIGHTED_TEXT](state: State, e: string[]) {
    state.highlightedText = e;
  },
  [ACTIONS.SET_ADJACENT_TEXT](state: State, e: DialogModel[]) {
    state.adjacentText = e;
  },
  [ACTIONS.SET_SHOWS](state: State, e: ShowModel[]) {
    state.shows = e;
  },
  [ACTIONS.SET_SHOW_INDEX](state: State, e: number) {
    state.showIndex = e;
  },
};

export default mutations;
