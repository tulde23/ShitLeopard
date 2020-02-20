import { EpisodeGridModel } from '@/models/EpisodeGridModel';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Episodes extends Vue {
  public episode: any;
  public gridModel = new EpisodeGridModel();

  public get episodes() {
    return this.$store.getters.episodes;
  }

  public get groupedBySeason() {
    if (this.episodes) {
      return this.$helper.groupBy(this.episodes, 'seasonId');
    }
    return undefined;
  }

  created() {}

  mounted() {
    this.$api.getEpisodes();
  }
}
