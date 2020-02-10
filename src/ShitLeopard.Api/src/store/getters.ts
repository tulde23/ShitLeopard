const getters = {
  isBusy: state => state.isBusy,
  selectedEpisode: state => state.episode,
  episodes: state => state.episodes,
  answer: state => state.answer,
  lines: state => state.lines
};

export default getters;
