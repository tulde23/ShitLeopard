import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Browse extends Vue {
  public episode: any;
  created() {}

  mounted() {
    this.$api.getEpisodes();
  }
  public setEpisode(id) {
    this.$api.getEpisode(id);
  }
  public get episodes() {
    return this.$store.getters.episodes;
  }
  public get selectedEpisode() {
    return this.$store.getters.selectedEpisode;
  }
}
