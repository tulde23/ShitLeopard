import { SearchMetricsCommand } from '@/models/SearchMetricsCommand';
import { SiteMetricGridModel } from '@/models/SiteMetricGridModel';
import { SiteMetric } from '@/viewModels/SiteMetric';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
  components: {},
})
export default class Metrics extends Vue {
  public question: string = '';
  public query: string = '';
  searchTimer: number | undefined = undefined;
  public searchModel: SearchMetricsCommand = new SearchMetricsCommand();
  public viewModel: SiteMetricGridModel = new SiteMetricGridModel();

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
    this.$metrics.searchMetrics(this.searchModel);
  }

  public get model() {
    return this.$store.getters.siteMetrics;
  }
  public get metrics() {
    console.log(this.model.result);
    return this.model && this.model.result ? this.model.result : [];
  }

  searchDebounce() {
    clearTimeout(this.searchTimer);
    this.searchTimer = setTimeout(() => {
      this.search();
    }, 500); /* 500ms throttle */
  }
}
