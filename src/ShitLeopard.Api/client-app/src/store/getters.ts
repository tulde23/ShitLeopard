import {
  Character,
  Episode,
  EpisodeGroup,
  Quote,
  ScriptLine,
  Tag
} from '@/viewModels';

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
  tags: (state: State): Tag[] => state.tags
};

export default getters;
