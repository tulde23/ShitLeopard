import { ShowModel } from '@/models';
import { PagedResult } from '@/models/PagedResult';
import {
  Character,
  DialogModel,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Season,
  Tag,
} from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { SiteMetric } from '@/viewModels/SiteMetric';

export interface State {
  isBusy: boolean;
  isOpen: boolean;
  timeToRefresh: boolean;
  episodes: Episode[];
  highlightedText: string[];
  dialogLines: DialogModel[];
  adjacentText: DialogModel[];
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
  selectedDialog: DialogModel;
  distance: number;
  shows: ShowModel[];
  showIndex: number;
}

export const InitState = {
  isBusy: false,
  isOpen: false,
  timeToRefresh: false,
  distance: 2,
  highlightedText: [],
  adjacentText: [],
  questionAnswer: {},
  dialogLines: [],
  episodes: [],
  seasons: [],
  episode: {},
  answer: '',
  lines: [],
  characters: [],
  groupedEpisodes: [],
  quote: {},
  tags: [],
  siteMetrics: new PagedResult<SiteMetric>(0, []),
  selectedDialog: {},
  shows: [],
  showIndex: 1,
};
