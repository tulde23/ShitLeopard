import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class ViewHeader extends Vue {
  public get busy() {
    return this.$store.getters.isBusy;
  }
}
