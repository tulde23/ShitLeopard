import { QuestionModel } from '@/models/QuestionModel';
import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import * as A from '@/store/mutation-types';
import { DialogModel, Episode, Quote, ScriptLine } from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { Store } from 'vuex';

import { HttpService } from './http.service';

export class SearchService {
  constructor(private store: Store<any>, private http: HttpService) {
    this.store.commit(A.SET_TEXT_MAP, new Map<string, DialogModel>());
  }

  search(question: QuestionModel) {
    this.store.commit(A.SET_DIALOG_LINES, []);
    return this.http.post(`/api/Search`, question).then((resp) => {
      this.store.commit(A.SET_DIALOG_LINES, resp.data);
    });
  }

  setQuestion(question: string) {
    this.store.commit(A.SET_QUESTION_, question);
  }
  setHighlightedText(text: string) {
    this.store.commit(A.SET_HIGHLIGHTED_TEXT, [text]);
  }
}
