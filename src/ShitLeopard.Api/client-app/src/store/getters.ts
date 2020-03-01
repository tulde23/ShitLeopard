import { PagedResult } from '@/models/PagedResult';
import {
  Character,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Tag
} from '@/viewModels';
import { SiteMetric } from '@/viewModels/SiteMetric';

import { State } from './state';

const getters = {
  isBusy: (state: State): boolean => state.isBusy,
  selectedEpisode: (state: State): Episode => state.episode,
  episodes: (state: State): Episode[] => state.episodes,
  answer: (state: State): string => state.answer,
  lines: (state: State): ScriptLine[] => state.lines,
  characters: (state: State): Character[] => state.characters,
  quote: (state: State): Quote => state.quote,
  groupedEpisodes: (state: State): EpisodeGroup[] => state.groupedEpisodes,
  tags: (state: State): Tag[] => state.tags,
  siteMetrics: (state: State): PagedResult<SiteMetric> => state.siteMetrics
};

export default getters;
