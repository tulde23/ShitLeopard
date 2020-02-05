import Vue from 'vue';
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';

const timer = {
  decision: null,
  interval: null
};
@Component({
  components: {}
})
export default class Confirmation extends Vue {
  @Prop({ default: false })
  show: boolean;

  public get dialogVisible() {
    return this.show || false;
  }
  public set dialogVisible(val: boolean) {
    this.show = val;
  }
  public open() {
    timer.decision = null;
    this.dialogVisible = true;
    return this.waitForDecision().then(x => {
      this.dialogVisible = false;
      return x;
    });
  }
  public waitForDecision(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      timer.interval = setInterval(function() {
        if (timer.decision === 1) {
          clearInterval(timer.interval);
          resolve(true);
        } else if (timer.decision === 0) {
          clearInterval(timer.interval);
          resolve(false);
        }
      }, 750);
    });
  }

  @Emit('ok') okHandler() {
    timer.decision = 1;
  }
  public cancelHandler() {
    timer.decision = 0;
  }
}
