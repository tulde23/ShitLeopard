const getters = {
  isBusy: state => state.isBusy,
  selectedEpisode: state => state.episode,
  episodes: state => state.episodes,
  answer: state => state.answer,
  lines: state => state.lines,
  characters: state => state.characters,
  quote: state => state.quote
};

export default getters;
