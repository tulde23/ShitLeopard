export interface State {
  isBusy: boolean;
  episodes: [];
  seasons: [];
  episode: any;
  answer: any;
  lines: [];
}

export const InitState = {
  isBusy: false,
  episodes: [],
  seasons: [],
  episode: null,
  answer: null,
  lines: []
};
