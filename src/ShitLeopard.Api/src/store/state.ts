export interface State {
  isBusy: boolean;
  episodes: [];
  seasons: [];
  episode: any;
}

export const InitState = {
  isBusy: false,
  episodes: [],
  seasons: [],
  episode: null
};
