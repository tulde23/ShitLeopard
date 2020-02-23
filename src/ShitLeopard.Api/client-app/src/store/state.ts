import {
  Character,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Season
} from '@/viewModels';

export interface State {
  isBusy: boolean;
  episodes: Episode[];
  seasons: Season[];
  episode: Episode;
  answer: string;
  lines: ScriptLine[];
  characters: Character[];
  groupedEpisodes: EpisodeGroup[];
  quote: Quote;
}

export const InitState = {
  isBusy: false,
  episodes: [],
  seasons: [],
  episode: {},
  answer: '',
  lines: [],
  characters: [],
  groupedEpisodes: [],
  quote: {}
};
