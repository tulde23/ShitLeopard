import Vue from 'vue';
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
@Component({
  components: {}
})
export default class Metaphor extends Vue {
  @Prop() name: string;
  @Prop() type: string;
}