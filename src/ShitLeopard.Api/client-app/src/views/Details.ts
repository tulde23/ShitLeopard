import { DialogModel } from '@/viewModels';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Details extends Vue {
  public distance = 2;
  created() {}

  mounted() {}
  public get id(): string {
    return this.$route.params.id;
  }
  public get selectedDialog(): DialogModel {
    return this.$store.getters.selectedDialog;
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
  public get busy() {
    return this.$store.getters.isBusy;
  }
}
