import { Message } from '@/models/message';
import { Component, Vue } from 'vue-property-decorator';

@Component({})
export default class App extends Vue {
  public errorMessage: Message;

  public get router(): any {
    return this.$router;
  }

  public get buildVersion() {
    return this.$store.getters.buildVersion;
  }
  public get links() {
    return this.router.options.routes;
  }

  created() {
    // subscribe to home component messages
    this.$subscription = this.$messageBus.getMessage().subscribe(message => {
      if (message) {
        // add message to local state if not empty
        // this.messages.push(message);
        this.errorMessage = message;
      } else {
        // clear messages when empty message received
        // this.messages = [];
        this.errorMessage = null;
      }
    });
  }
  beforeDestroy() {
    // unsubscribe to ensure no memory leaks
    this.$subscription.unsubscribe();
  }
  mounted() {}

  public get isErrorAvailable() {
    if (this.errorMessage) {
      return true;
    }
    return false;
  }
}
