import { QuestionGridModel } from '@/models/QuestionGridModel';
import { QuestionAnswer } from '@/viewModels/QuestionAnswer';
import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Home extends Vue {
  public question: string = '';
  public query: string = '';
  searchTimer: number | undefined = undefined;
  public viewModel = new QuestionGridModel();
  created() {}

  mounted() {}

  public get busy() {
    return this.$store.getters.isBusy;
  }

  search() {
    //this.$api.askMe(this.question);
    this.$api.askMe(this.question);
  }

  public get tags() {
    return this.$store.getters.tags;
  }
  public searchTags(x: string) {
    if (!x || x === 'null') {
      return;
    }
    this.$api.searchTags(x, 'Search');
  }
  public upvote(item: any) {
    this.$api.upvote(item.id);
  }
  public get response(): QuestionAnswer {
    return this.$store.getters.questionAnswer;
  }
  public get lines() {
    if (this.response) {
      return this.response.answer;
    }
  }
  public get answer() {
    if (this.response) {
      return this.response.answer;
    }
  }
  @Watch('query') onQueryChanged(o: any, n: any) {
    this.fetchEntriesDebounced();
  }
  @Watch('question') onQuestionChanged(o: any, n: any) {
    this.searchDebounce();
  }
  fetchEntriesDebounced() {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.searchTags(this.query);
    }, 500); /* 500ms throttle */
  }
  searchDebounce() {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.search();
    }, 500); /* 500ms throttle */
  }
}
