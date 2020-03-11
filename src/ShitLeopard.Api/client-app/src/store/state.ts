import { PagedResult } from '@/models/PagedResult';
import {
  Character,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Season,
  Tag
} from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { SiteMetric } from '@/viewModels/SiteMetric';

export interface State {
  isBusy: boolean;
  timeToRefresh: boolean;
  episodes: Episode[];
  seasons: Season[];
  episode: Episode;
  answer: string;
  questionAnswer: QuestionAnswer;
  lines: ScriptLine[];
  characters: Character[];
  groupedEpisodes: EpisodeGroup[];
  tags: Tag[];
  quote: Quote;
  siteMetrics: PagedResult<SiteMetric>;
}

export const InitState = {
  isBusy: false,
  timeToRefresh: false,
  questionAnswer: {},
  episodes: [],
  seasons: [],
  episode: {},
  answer: '',
  lines: [],
  characters: [],
  groupedEpisodes: [],
  quote: {},
  tags: [],
  siteMetrics: new PagedResult<SiteMetric>(0, [])
};
