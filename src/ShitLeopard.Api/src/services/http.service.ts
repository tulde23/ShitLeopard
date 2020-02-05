import axios from 'axios';
import { Store } from 'vuex';

import * as ACTIONS from '../store/mutation-types';

let baseUrl: string = '';

export class HttpService {
  constructor(public store: Store<any>) {}
  public get(url: string) {
    this.store.commit(ACTIONS.EXECUTING);
    return axios({
      url: this.buildUrl(url),
      headers: this.setToken(),
      method: 'GET'
    })
      .then(resp => {
        this.store.commit(ACTIONS.COMPLETE);
        return resp;
      })
      .catch(err => {
        this.store.commit(ACTIONS.ERROR);
        return err;
      });
  }
  public post(url: string, data: any, contentType?: string) {
    this.store.commit(ACTIONS.EXECUTING);

    return axios({
      url: this.buildUrl(url),
      method: 'POST',
      headers: this.setToken(contentType),
      data
    })
      .then(resp => {
        this.store.commit(ACTIONS.COMPLETE);
        return resp;
      })
      .catch(err => {
        this.store.commit(ACTIONS.ERROR);
        return err;
      });
  }
  public put(url: string, data: any, contentType?: string) {
    this.store.commit(ACTIONS.EXECUTING);
    return axios({
      url: this.buildUrl(url),
      method: 'PUT',
      headers: this.setToken(contentType),
      data
    })
      .then(resp => {
        this.store.commit(ACTIONS.COMPLETE);
        return resp;
      })
      .catch(err => {
        this.store.commit(ACTIONS.ERROR);
        return err;
      });
  }
  public delete(url: string) {
    this.store.commit(ACTIONS.EXECUTING);
    return axios({
      url: this.buildUrl(url),
      headers: this.setToken(),
      method: 'DELETE'
    })
      .then(resp => {
        this.store.commit(ACTIONS.COMPLETE);
        return resp;
      })
      .catch(err => {
        this.store.commit(ACTIONS.ERROR);
        return err;
      });
  }
  public patch(url: string, data: any, contentType?: string) {
    this.store.commit(ACTIONS.EXECUTING);
    return axios({
      url: this.buildUrl(url),
      method: 'PATCH',
      headers: this.setToken(contentType),
      data
    })
      .then(resp => {
        this.store.commit(ACTIONS.COMPLETE);
        return resp;
      })
      .catch(err => {
        this.store.commit(ACTIONS.ERROR);
        return err;
      });
  }
  private setToken(contentType?: string) {
    const token = window.sessionStorage.getItem('token');
    const header = {};
    if (contentType) {
      header['Content-Type'] = contentType;
    } else {
      header['Content-Type'] = 'application/json';
    }
    if (token) {
      header['Authorization'] = `Bearer ${token}`;
    }

    return header;
  }

  private buildUrl(url: string): string {
    if (url.startsWith('/')) {
      url = url.substr(1);
    }

    return `${baseUrl}/${url}`;
  }
}
