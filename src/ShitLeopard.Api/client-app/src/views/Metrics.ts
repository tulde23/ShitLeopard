import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import { SiteMetricGridModel } from '@/models/SiteMetricGridModel';
import { SiteMetric } from '@/viewModels/SiteMetric';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

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
  public footerProperties = {
    showFirstLastPage: true,
    firstIcon: 'mdi-arrow-collapse-left',
    lastIcon: 'mdi-arrow-collapse-right',
    prevIcon: 'mdi-minus',
    nextIcon: 'mdi-plus',
    'items-per-page-options': this.viewModel.rowsPerPageItems
  };
  created() {}

  mounted() {
    this.search();
  }

  public get busy() {
    return this.$store.getters.isBusy;
  }

  public formatHeaders(item: SiteMetric) {
    if (item.headers) {
      return JSON.stringify(item.headers);
    }
    return '-';
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
}
