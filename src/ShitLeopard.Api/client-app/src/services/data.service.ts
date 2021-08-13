import { QuestionModel } from '@/models/QuestionModel';
import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import * as A from '@/store/mutation-types';
import { DialogModel, Episode, Quote, ScriptLine } from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { Store } from 'vuex';

import { HttpService } from './http.service';

export class DataService {
  constructor(private store: Store<any>, private http: HttpService) {
    this.store.commit(A.SET_TEXT_MAP, new Map<string, DialogModel>());
  }

  updateRefreshInterval(state: boolean) {
    this.store.commit(A.SET_REFRESH, state);
  }
  isOpen(state: boolean) {
    this.store.commit(A.SET_IS_OPEN, state);
  }

  getSeasons() {
    return this.http.get('/api/Season');
  }
  getScriptLines(scriptId: number) {
    return this.http.get(`api/Script/Lines/${scriptId}`);
  }
  getEpisodes() {
    return this.http.get('/api/Episode').then((resp) =>
      this.store.commit(
        A.SET_EPISODES,
        resp.data.map((x: Episode) => {
          x.title = `Season ${x.seasonId} - ${x.title}`;
          return x;
        })
      )
    );
  }

  getCharacters() {
    return this.http
      .get('/api/Character')
      .then((resp) => this.store.commit(A.SET_CHARACTERS, resp.data));
  }
  saveLine(line: ScriptLine) {
    return this.http.post(`/api/Script/ScripLine`, line);
  }

  askMe(question: string, fuzzy?: boolean) {
    return this.http.post('/api/Question', { text: question, isFuzzy: fuzzy }).then((resp) => {
      const data = resp.data as QuestionAnswer;
      console.log('data', data);
      this.store.commit(A.SET_QUESTION_ANSWER, resp.data);
      if (data.isArray) {
        this.store.commit(A.SET_LINES, data.answer);
      } else {
        this.store.commit(A.SET_ANSWER, data.answer);
      }
    });
  }

  upvote(item: number | undefined) {
    return this.http.post(`/api/Quote/${item}`, {});
  }
  likeQuote(item: Quote) {
    return this.http.post(`/api/Quote`, item);
  }
  getRandomQuote() {
    return this.http.get('/api/Quote').then((resp) => this.store.commit(A.SET_QUOTE, resp.data));
  }

  searchTags(filter: string | undefined, category: string | undefined) {
    return this.http
      .get(`/api/Tags?category=${category}&name=${filter}`)
      .then((resp) => this.store.commit(A.SET_TAGS, resp.data));
  }
  getMostPopularTags(category: string | undefined, count: number) {
    return this.http
      .get(`/api/Tags/Popular/${category}/${count}`)
      .then((resp) => this.store.commit(A.SET_TAGS, resp.data));
  }
  searchMetrics(command: SearchMetricsCommand) {
    return this.http
      .post(`/api/SiteMetrics`, command)
      .then((resp) => this.store.commit(A.SET_METRICS, resp.data));
  }
  setSelectedDialog(dialog: DialogModel) {
    this.store.commit(A.SET_SELECTED_DIALOG, dialog);
  }

  getAdjacentText(id?: string, distance?: number) {
    if (!distance || distance <= 0) {
      return;
    }
    return this.http
      .get(`/api/Search/${id}?distance=${distance}`)
      .then((resp) => this.store.commit(A.SET_TEXT_ENTRY, { id: id, value: resp.data }));
  }

  setShowIndex(i: number) {
    this.store.commit(A.SET_SHOW_INDEX, i);
  }
  clearAdjacentText() {
    this.store.commit(A.SET_ADJACENT_TEXT, []);
  }
  public groupBy(xs: any, key: any) {
    return xs.reduce((rv: any, x: any) => {
      (rv[x[key]] = rv[x[key]] || []).push(x);
      return rv;
    }, {});
  }

  public getAdjacentTextFor(id: string): DialogModel[] {
    const map = this.store.getters.textMap as Map<string, DialogModel[]>;
    if (map && map.has(id)) {
      return map.get(id) as DialogModel[];
    }
    return [];
  }
}
