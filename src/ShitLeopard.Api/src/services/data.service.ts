import * as A from '@/store/mutation-types';
import { Store } from 'vuex';

import { HttpService } from './http.service';

export class DataService {
  constructor(private store: Store<any>, private http: HttpService) {}

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
        resp.data.map(x => {
          x.title = `Season ${x.seasonId} - ${x.title}`;
          return x;
        })
      )
    );
  }
  getEpisode(id: number) {
    return this.http
      .get(`/api/Episode/${id}`)
      .then(resp => this.store.commit(A.SET_EPISODE, resp.data));
  }
}
