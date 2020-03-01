import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import { SiteMetricGridModel } from '@/models/SiteMetricGridModel';
import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';

@Component({
  components: {}
})
export default class Metrics extends Vue {
  public question: string = '';
  public query: string = '';
  searchTimer: number | undefined = undefined;
  public searchModel: SearchMetricsCommand = new SearchMetricsCommand();
  public viewModel: SiteMetricGridModel = new SiteMetricGridModel();
  public expanded: Array<any> = [];

  public singleExpand: boolean = true;
  public options: any = {
    itemsPerPage: 50
  };
  created() {}

  mounted() {
    this.search();
  }

  public get busy() {
    return this.$store.getters.isBusy;
  }

  search() {
    //this.$api.askMe(this.question);
    this.$api.searchMetrics(this.searchModel);
  }

  public get model() {
    return this.$store.getters.siteMetrics;
  }

  searchDebounce() {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.search();
    }, 500); /* 500ms throttle */
  }
  @Watch('options') onOptionsChanged(e: any, n: any) {
    const { sortBy, sortDesc, page, itemsPerPage } = this.options;
    console.log('opitons changed', this.options);
    this.searchModel.pageSize = itemsPerPage;
    this.searchModel.pageNumber = page + 1;
    this.search();
  }
}
