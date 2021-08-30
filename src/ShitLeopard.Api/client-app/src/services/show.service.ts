import { QuestionModel } from '@/models/QuestionModel';
import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import * as A from '@/store/mutation-types';
import { DialogModel, Episode, Quote, ScriptLine } from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { Store } from 'vuex';

import { HttpService } from './http.service';

export class ShowService {
  constructor(private store: Store<any>, private http: HttpService) {}

  getShows() {
    return this.http.get('/api/Show').then((resp) => this.store.commit(A.SET_SHOWS, resp.data));
  }
}
