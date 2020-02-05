import { Component, Watch, Vue } from 'vue-property-decorator';
import { Subject } from 'rxjs';
import { Message } from '@/models/message';
@Component({})
export default class App extends Vue {
  public errorMessage: Message;


  public get router(): any {
    return this.$router;
  }

  public get authorizedRoutes() {
    return this.router.options.routes.filter(x => x.meta.display === true && this.$authService.isInRole(x.meta.roles));
  }

  public get isLoggedIn() {
    return this.$authService.isLoggedIn;
  }
  public get buildVersion() {
    return this.$store.getters.buildVersion;
  }
  public get links() {
    return this.router.options.routes;
  }
  public logout() {
    this.$authService.logout().then(x => this.$authService.redirectToLogin());

  }
  created() {

    this.$router.beforeEach((to, from, next) => {
      console.log("to", to.path);
      console.log("roles", to);
      if (to.path === from.path) {
        return;
      }
      if (to.path === '/login') {
        next();
        return;
      }
      if (this.$authService.isLoggedIn && this.$authService.isInRole(to.meta.roles)) {
        next();

      }
      else {
        this.$authService.redirectToAccessDenied();
      }
    });

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
  mounted() {

  }
  @Watch("isLoggedIn") onAuthStatusChanged(o, n) {
    if (this.isLoggedIn) {
      this.$store.dispatch("setBuildVersion");
    }
  }
  public get isErrorAvailable() {
    if (this.errorMessage) {
      return true;
    }
    return false;
  }
}
