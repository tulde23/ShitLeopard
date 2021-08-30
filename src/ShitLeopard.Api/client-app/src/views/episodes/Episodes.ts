import { QuestionModel } from '@/models/QuestionModel';
import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class Episodes extends Vue {
  public question = new QuestionModel('', false);
  searchTimer: number | undefined = undefined;
  public get busy() {
    return this.$store.getters.isBusy;
  }

  public get episodes() {
    return this.$store.getters.groupedEpisodes;
  }

  public get treeData() {
    return [];
  }
  public get showName() {
    if (this.episodes) {
      return this.episodes.showName;
    }
    return '';
  }
  search() {
    const encoded = encodeURI(this.question.text ?? '');
    this.$search.setQuestion(this.question.text ?? '');
    this.$router.push(`query/${encoded}`);
  }

  @Watch('question.text') onQuestionChanged(o: any, n: any) {
    this.searchDebounce();
  }
  mounted() {
    let showId = this.$route.params.showid;

    this.$episodes.getEpisodeGroups(showId);
  }

  searchDebounce() {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.search();
    }, 700); /* 500ms throttle */
  }
}
