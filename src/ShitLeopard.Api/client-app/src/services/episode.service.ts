import { QuestionModel } from '@/models/QuestionModel';
import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import * as A from '@/store/mutation-types';
import { DialogModel, Episode, Quote, ScriptLine } from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { Store } from 'vuex';

import { HttpService } from './http.service';

export class EpisodeService {
  constructor(private store: Store<any>, private http: HttpService) {}
  getEpisode(id: number) {
    this.store.commit(A.SET_EPISODE, undefined);
    return this.http
      .get(`/api/Episode/${id}`)
      .then((resp) => this.store.commit(A.SET_EPISODE, resp.data));
  }
  getEpisodeGroups(showId: string) {
    //
    return this.http
      .get(`/api/Episode/GroupBySeason/${showId}`)
      .then((resp) => this.store.commit(A.SET_EPISODES_GROUPED, resp.data));
  }
}
