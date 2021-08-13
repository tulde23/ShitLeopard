import { QuestionModel } from '@/models/QuestionModel';
import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class Shows extends Vue {
  public question = new QuestionModel('', false);
  searchTimer: number | undefined = undefined;
  public get busy() {
    return this.$store.getters.isBusy;
  }
  public get shows() {
    return this.$store.getters.shows;
  }
  mounted() {
    this.search();
  }
  search() {
    this.$shows.getShows();
  }

  public getSeasonPath(showId: string) {
    return `/seasons/${showId}`;
  }
  public getEpisodePath(showId: string) {
    return `/episodes/${showId}`;
  }

  @Watch('question.text') onQuestionChanged(o: any, n: any) {
    this.searchDebounce();
  }

  searchDebounce() {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.search();
    }, 700); /* 500ms throttle */
  }
}
