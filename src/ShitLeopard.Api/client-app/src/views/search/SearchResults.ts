import { QuestionGridModel } from '@/models/QuestionGridModel';
import { QuestionModel } from '@/models/QuestionModel';
import { DialogModel } from '@/viewModels';
import Vue from 'vue';

import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class SearchResults extends Vue {
  public question = new QuestionModel('', false);
  searchTimer: number | undefined = undefined;

  private _searchActive: boolean = false;

  public currentIndex: number = -1;

  public get searchActive() {
    return this._searchActive;
  }
  public set searchActive(state: boolean) {
    this._searchActive = state;
  }

  public get query() {
    return this.$store.getters.question;
  }
  public get busy() {
    return this.$store.getters.isBusy;
  }

  public get dialogLines() {
    return this.$store.getters.dialogLines;
  }

  created() {}
  mounted() {
    this.searchActive = false;
    this.$search.setQuestion(this.$route.params.pattern);
    this.question.text = this.query;
    this.$search.search(this.question).then((_) => {
      this.searchActive = true;
      this.$search.setQuestion(this.question.text ?? '');
    });
  }
  public get medium(): boolean {
    return this.$vuetify.breakpoint.mdAndUp;
  }
  isVisible(index: number) {
    return this.currentIndex === index;
  }

  toggle(index: number) {
    if (this.currentIndex === index) {
      this.currentIndex = -1;
    } else {
      this.currentIndex = index;
    }
    this.$forceUpdate();
  }
  search() {
    this.searchActive = false;
    if (!this.question.text || this.question.text.length <= 1) {
      return;
    }

    if (this.query === this.question.text) {
      return;
    }
    const encoded = encodeURI(this.question.text ?? '');
    this.$search.setQuestion(this.question.text ?? '');
    document.location.href = `/#/query/${encoded}`;
    this.question.text = this.query;
    this.$search.search(this.question).then((_) => {
      this.searchActive = true;
      this.$search.setQuestion(this.question.text ?? '');
    });
  }

  public get resultCount(): number {
    if (this.dialogLines && this.dialogLines.length > 0) {
      return this.dialogLines.length ?? 0;
    }
    return 0;
  }

  @Watch('question.text') onQuestionChanged(o: any, n: any) {
    if (n && n.length > 2) {
      this.searchDebounce();
    }
  }

  searchDebounce() {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.search();
    }, 700); /* 500ms throttle */
  }
}
