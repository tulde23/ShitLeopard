import {
  Character,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Season,
  Tag
} from '@/viewModels';

export interface State {
  isBusy: boolean;
  timeToRefresh: boolean;
  episodes: Episode[];
  seasons: Season[];
  episode: Episode;
  answer: string;
  lines: ScriptLine[];
  characters: Character[];
  groupedEpisodes: EpisodeGroup[];
  tags: Tag[];
  quote: Quote;
}

export const InitState = {
  isBusy: false,
  timeToRefresh: false,
  episodes: [],
  seasons: [],
  episode: {},
  answer: '',
  lines: [],
  characters: [],
  groupedEpisodes: [],
  quote: {},
  tags: []
};
