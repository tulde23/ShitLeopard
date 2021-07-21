import { QuestionGridModel } from '@/models/QuestionGridModel';
import { QuestionModel } from '@/models/QuestionModel';
import { DialogModel } from '@/viewModels';
import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class Home extends Vue {
  public question = new QuestionModel('', false);
  public query: string = '';
  searchTimer: number | undefined = undefined;
  tagTimer: number | undefined = undefined;
  public viewModel = new QuestionGridModel();
  public distance = 2;
  tagsToShow = 5;

  public get busy() {
    return this.$store.getters.isBusy;
  }
  public get isOpen() {
    return this.$store.getters.isOpen;
  }
  public get selectedDialog(): DialogModel {
    return this.$store.getters.selectedDialog;
  }
  public get tags() {
    if (this.$store.getters.tags) {
      if (this.$vuetify.breakpoint.mdAndUp) {
        return this.$store.getters.tags;
      } else {
        let temp = this.$store.getters.tags;
        if (temp.length > 3) {
          return temp.slice(0, 3);
        }
        return temp;
      }
    }
  }
  public get dialogLines() {
    return this.$store.getters.dialogLines;
  }
  public get highlightedText() {
    return this.$store.getters.highlightedText;
  }
  public get adjacentText() {
    return this.$store.getters.adjacentText;
  }
  public isMobile = false;
  onResize() {
    this.isMobile = window.innerWidth < 600;
  }
  created() {}
  mounted() {
    this.fetchTagsTimer();
    window.addEventListener('resize', this.onResize, { passive: true });
  }
  beforeDestroy() {
    if (typeof window === 'undefined') return;

    window.removeEventListener('resize', this.onResize);
  }
  search() {
    //this.$api.askMe(this.question);
    this.$api.setHighlightedText(this.question.text ?? '');
    this.$api.search(this.question);
  }
  public searchByTag(tag: string) {
    this.question.text = tag;
    this.search();
  }
  public openDeails(item: DialogModel) {
    this.$api.getAdjacentText(item.id, this.distance);
    this.$api.setSelectedDialog(item);
    this.$api.isOpen(true);
    //  this.$router.push( { name: 'details', params: { id: item.id  ?? '' }} );
  }
  public closeDetails() {
    this.$api.isOpen(false);
    this.$api.setSelectedDialog({});
    this.$api.clearAdjacentText();
  }
  public searchTags(x: string) {
    if (!x || x === 'null') {
      return;
    }
    this.$api.searchTags(x, 'Search');
  }

  public get resultCount(): number {
    if (this.dialogLines) {
      return this.dialogLines.length;
    }
    return 0;
  }
  increase() {
    if (this.distance === 10) {
      return;
    }
    this.distance = this.distance + 1;
    this.$api.getAdjacentText(this.selectedDialog.id, this.distance);
  }
  decrease() {
    if (this.distance === 1) {
      return;
    }
    this.distance = this.distance - 1;
    this.$api.getAdjacentText(this.selectedDialog.id, this.distance);
  }
  public reset() {
    this.distance = 2;
    this.$api.getAdjacentText(this.selectedDialog.id, this.distance);
  }

  @Watch('query') onQueryChanged(o: any, n: any) {
    // this.fetchEntriesDebounced();
  }
  @Watch('question.text') onQuestionChanged(o: any, n: any) {
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
      this.fetchTagsTimer();
    }, 700); /* 500ms throttle */
  }
  fetchTagsTimer() {
    this.$api.getMostPopularTags('DialogSearch', this.tagsToShow);
  }
  public get shows() {
    return this.$store.getters.shows;
  }
}
