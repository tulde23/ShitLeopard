import { Component, Vue } from 'vue-property-decorator';

@Component({})
export default class App extends Vue {
  public isErrorAvailable = false;

  public get showIndex() {
    return this.$store.getters.showIndex;
  }
  public get shows() {
    return this.$store.getters.shows;
  }
  public get busy() {
    return this.$store.getters.isBusy;
  }
  public get expandOnHover() {
    return true;
  }
  public get isMini() {
    return true;
  }
  public get router(): any {
    return this.$router;
  }
  public get authorizedRoutes() {
    return this.router.options.routes;
  }

  created() {}
}
