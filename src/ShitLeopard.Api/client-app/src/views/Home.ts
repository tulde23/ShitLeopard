import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Home extends Vue {
  public question: string = '';
  created() {}

  search() {
    //this.$api.askMe(this.question);
    this.$api.search(this.question);
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
}
