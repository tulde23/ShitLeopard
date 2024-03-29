import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import * as A from '@/store/mutation-types';
import { Episode, Quote, ScriptLine } from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { Store } from 'vuex';

import { HttpService } from './http.service';

export class DataService {
  constructor(private store: Store<any>, private http: HttpService) {}

  updateRefreshInterval(state: boolean) {
    this.store.commit(A.SET_REFRESH, state);
  }

  getSeasons() {
    return this.http.get('/api/Season');
  }
  getScriptLines(scriptId: number) {
    return this.http.get(`api/Script/Lines/${scriptId}`);
  }
  getEpisodes() {
    return this.http.get('/api/Episode').then(resp =>
      this.store.commit(
        A.SET_EPISODES,
        resp.data.map((x: Episode) => {
          x.title = `Season ${x.seasonId} - ${x.title}`;
          return x;
        })
      )
    );
  }
  getEpisodeGroups() {
    //
    return this.http.get('/api/Episode/GroupBySeason').then(resp =>
      this.store.commit(
        A.SET_EPISODES_GROUPED,
        resp.data.map((x: any) => {
          return x;
        })
      )
    );
  }
  getCharacters() {
    return this.http
      .get('/api/Character')
      .then(resp => this.store.commit(A.SET_CHARACTERS, resp.data));
  }
  saveLine(line: ScriptLine) {
    return this.http.post(`/api/Script/ScripLine`, line);
  }

  askMe(question: string) {
    return this.http.post('/api/Question', { text: question }).then(resp => {
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
  search(term: string) {
    return this.http
      .post(`/api/Search/LinesContaining`, { text: term })
      .then(resp => this.store.commit(A.SET_LINES, resp.data));
  }
  getEpisode(id: number) {
    this.store.commit(A.SET_EPISODE, undefined);
    return this.http
      .get(`/api/Episode/${id}`)
      .then(resp => this.store.commit(A.SET_EPISODE, resp.data));
  }
  upvote(item: number | undefined) {
    return this.http.post(`/api/Quote/${item}`, {});
  }
  likeQuote(item: Quote) {
    return this.http.post(`/api/Quote`, item);
  }
  getRandomQuote() {
    return this.http.get('/api/Quote').then(resp => this.store.commit(A.SET_QUOTE, resp.data));
  }

  searchTags(filter: string | undefined, category: string | undefined) {
    return this.http
      .get(`/api/Tags?category=${category}&name=${filter}`)
      .then(resp => this.store.commit(A.SET_TAGS, resp.data));
  }
  getMostPopularTags(category: string | undefined, count: number) {
    return this.http
      .get(`/api/Tags/Popular/${category}/${count}`)
      .then(resp => this.store.commit(A.SET_TAGS, resp.data));
  }
  searchMetrics(command: SearchMetricsCommand) {
    return this.http
      .post(`/api/SiteMetrics`, command)
      .then(resp => this.store.commit(A.SET_METRICS, resp.data));
  }
}
