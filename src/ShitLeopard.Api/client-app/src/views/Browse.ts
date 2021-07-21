import { EpisodeGridModel } from '@/models/EpisodeGridModel';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class Browse extends Vue {
  public episode: any;
  public gridModel = new EpisodeGridModel();
  searchTimer: number | undefined = undefined;

  public get characters() {
    return this.$store.getters.characters;
  }
  created() {}

  mounted() {
    this.$api.getCharacters().then((x) => this.$api.getEpisodes());
  }
  public get busy() {
    return this.$store.getters.isBusy;
  }
  public get shows() {
    return this.$store.getters.shows;
  }
  public upvote(item: any) {
    this.$api.upvote(item.id);
  }
  public saveLine(line: any) {
    this.$api.saveLine(line);
  }
  public setEpisode(id: any) {
    this.$api.getEpisode(id);
  }
  public get episodes() {
    return this.$store.getters.episodes;
  }
  public get selectedEpisode() {
    return this.$store.getters.selectedEpisode;
  }

  saveDebounce(line: any) {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      if (!line || !line.body || line.body.length <= 1) {
        return;
      }
      this.saveLine(line);
    }, 500); /* 500ms throttle */
  }
}
