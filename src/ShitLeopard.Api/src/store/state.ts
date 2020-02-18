export interface State {
  isBusy: boolean;
  episodes: [];
  seasons: [];
  episode: any;
  answer: any;
  lines: [];
  characters: [];
  quote: any;
}

export const InitState = {
  isBusy: false,
  episodes: [],
  seasons: [],
  episode: null,
  answer: null,
  lines: [],
  characters: [],
  quote: null
};
