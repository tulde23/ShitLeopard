import { EpisodeGridModel } from '@/models/EpisodeGridModel';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Browse extends Vue {
  public episode: any;
  public gridModel = new EpisodeGridModel();

  public get characters() {
    return this.$store.getters.characters;
  }
  created() {}

  mounted() {
    this.$api.getCharacters().then(x => this.$api.getEpisodes());
  }
  public upvote(item: any) {
    this.$api.upvote(item.id);
  }
  public saveLine(line: any) {
    this.$api.saveLine(line.id, line.characterId);
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
}
