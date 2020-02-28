import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Home extends Vue {
  public question: string = '';
  public query: string = '';
  searchTimer: number | undefined = undefined;
  created() {}

  mounted() {
    this.$api.getMostPopularTags('Search', 5);
  }

  public get busy() {
    return this.$store.getters.isBusy;
  }

  search() {
    //this.$api.askMe(this.question);
    this.$api.search(this.question);
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
  public get answer(): any {
    return this.$store.getters.answer;
  }
  public get lines() {
    return this.$store.getters.lines;
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
