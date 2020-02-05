import Vue from 'vue';
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
@Component({
  components: {}
})
export default class StatusIcon extends Vue {
      @Prop() level: any;
}