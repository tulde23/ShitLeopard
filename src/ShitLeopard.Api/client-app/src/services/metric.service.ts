import { QuestionModel } from '@/models/QuestionModel';
import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import * as A from '@/store/mutation-types';
import { DialogModel, Episode, Quote, ScriptLine } from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { Store } from 'vuex';

import { HttpService } from './http.service';

export class MetricService {
  constructor(private store: Store<any>, private http: HttpService) {}

  searchMetrics(command: SearchMetricsCommand) {
    return this.http.post(`/api/SiteMetrics`, command).then((resp) => {
      this.store.commit(A.SET_METRICS, resp.data);
      return resp.data;
    });
  }
}
