import { QuestionModel } from '@/models/QuestionModel';
import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class Home extends Vue {
  public question = new QuestionModel('', false);
  searchTimer: number | undefined = undefined;
  public get busy() {
    return this.$store.getters.isBusy;
  }

  search() {
    const encoded = encodeURI(this.question.text ?? '');
    this.$router.push(`query/${encoded}`);
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
