import { Component, Vue } from 'vue-property-decorator';

@Component({})
export default class App extends Vue {
  public isErrorAvailable = false;

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
  created() {
    // this.$vuetify.theme.dark = true;
    console.log(this.authorizedRoutes);
  }
}
