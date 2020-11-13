import { QuestionGridModel } from '@/models/QuestionGridModel';
import { QuestionModel } from '@/models/QuestionModel';
import { DialogModel } from '@/viewModels';
import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Home extends Vue {
  public question = new QuestionModel('', false);
  public query: string = '';
  searchTimer: number | undefined = undefined;
  public viewModel = new QuestionGridModel();
  public distance = 2;

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
    return this.$store.getters.tags;
  }
  public get dialogLines() {
    return this.$store.getters.dialogLines;
  }
  public get highlightedText() {
    return this.$store.getters.highlightedText;
  }
  public get adjacentText(){
    return this.$store.getters.adjacentText;
  }
  created() {}
  mounted() {}
  search() {
    //this.$api.askMe(this.question);
    this.$api.setHighlightedText(this.question.text ?? '');
    this.$api.search(this.question);
  }
  public openDeails(item: DialogModel) {
    this.$api.getAdjacentText(item.id, this.distance );
    this.$api.setSelectedDialog(item);
    this.$api.isOpen(true);
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

  @Watch('query') onQueryChanged(o: any, n: any) {
    // this.fetchEntriesDebounced();
  }
  @Watch('question.text') onQuestionChanged(o: any, n: any) {
    this.searchDebounce();
  }
  @Watch('distance') onDistanceChange(o: any, n: any) {
    // this.fetchEntriesDebounced();
    if( this.selectedDialog){
  
      clearTimeout(this.searchTimer);
      this.searchTimer = setTimeout(() => {
        this.$api.getAdjacentText(this.selectedDialog.id, this.distance );
      }, 500); /* 500ms throttle */
    }
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
    }, 700); /* 500ms throttle */
  }
}
