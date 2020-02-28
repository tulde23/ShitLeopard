import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class RandomQuote extends Vue {
  private timerId: any;
  public get quote() {
    return this.$store.getters.quote;
  }

  public get refreshQuote() {
    return this.$store.getters.timeToRefresh;
  }
  created() {}

  mounted() {
    this.$api.getRandomQuote();
    this.setInterval();
  }
  public refresh() {
    window.clearInterval(this.timerId);
    this.$api.getRandomQuote().then(x => this.setInterval());
  }
  public upvote() {
    this.quote.popularity++;
    this.$api.likeQuote(this.quote);
  }

  private setInterval() {
    this.timerId = window.setInterval(() => {
      this.refresh();
    }, 10000);
  }
}
