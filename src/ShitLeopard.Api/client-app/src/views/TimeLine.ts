import { EpisodeGridModel } from '@/models/EpisodeGridModel';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class TimeLine extends Vue {
  public episode: any;
  public gridModel = new EpisodeGridModel();

  public get episodes(): any[] {
    return this.$store.getters.groupedEpisodes;
  }

  created() {}

  mounted() {
    this.$api.getEpisodeGroups();
  }
}
