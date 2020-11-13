import { PagedResult } from '@/models/PagedResult';
import {
  Character,
  DialogModel,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Tag
} from '@/viewModels';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import { SiteMetric } from '@/viewModels/SiteMetric';

import { State } from './state';

const getters = {
  isBusy: (state: State): boolean => state.isBusy,
  isOpen: (state: State): boolean => state.isOpen,
  selectedEpisode: (state: State): Episode => state.episode,
  episodes: (state: State): Episode[] => state.episodes,
  answer: (state: State): string => state.answer,
  lines: (state: State): ScriptLine[] => state.lines,
  characters: (state: State): Character[] => state.characters,
  quote: (state: State): Quote => state.quote,
  groupedEpisodes: (state: State): EpisodeGroup[] => state.groupedEpisodes,
  tags: (state: State): Tag[] => state.tags,
  siteMetrics: (state: State): PagedResult<SiteMetric> => state.siteMetrics,
  questionAnswer: (state: State): QuestionAnswer => state.questionAnswer,
  selectedDialog: (state: State): DialogModel => state.selectedDialog,
  dialogLines: (state: State): DialogModel[] => state.dialogLines,
  highlightedText: (state: State): string[] => state.highlightedText,
  adjacentText: (state: State): DialogModel[] => state.adjacentText
};

export default getters;
