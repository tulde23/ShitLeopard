import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class RandomQuote extends Vue {
  public get quote() {
    return this.$store.getters.quote;
  }
  created() {}

  mounted() {
    this.$api.getRandomQuote();
  }
  public refresh() {
    this.$api.getRandomQuote();
  }
  public upvote() {
    this.quote.popularity++;
    this.$api.likeQuote(this.quote);
  }
}
