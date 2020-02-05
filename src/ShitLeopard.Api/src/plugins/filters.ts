import { DataService } from '@/services/data.service';
import { Helper } from '@/services/helper';
import moment from 'moment-timezone';
import numeral from 'numeral';

const helper = new Helper();
const dataService = new DataService();

const CustomFilters = {
  install: (Vue, options) => {
    Vue.filter('formatCommand', function(value) {
      if (value) {
        const items = value.split(':');
        if (items.length > 0) {
          return items[0];
        }
      }
      return '-';
    });
    Vue.filter('formatNumber', function(value) {
      return numeral(value).format('0,0'); // displaying other groupings/separators is possible, look at the docs
    });
    Vue.filter('formatMoney', function(value) {
      return numeral(value).format('$0,0.00'); // displaying other groupings/separators is possible, look at the docs
    });
    Vue.filter('formatMB', function(value) {
      const mb = 0.000001;

      return numeral(value * mb).format('0.0000') + ' MB'; // displaying other groupings/separators is possible, look at the docs
    });
    Vue.filter('formatProgramType', function(value) {
      return helper.resolveProgramType(value);
    });
    Vue.filter('sport', value => {
      return helper.formatActivityType(value);
    });

    Vue.filter('formatGB', function(value) {
      const mb = 0.000001;
      const b = 1073741824;
      if (value >= b) {
        return numeral(value / b).format('0.000') + ' GB';
      } else {
        return numeral(value * mb).format('0.0000') + ' MB';
      }

      // displaying other groupings/separators is possible, look at the docs
    });
    Vue.filter('ngb', function(value) {
      if (!value) {
        return '--';
      }
      switch (value) {
        case 5:
          return `${value} Little League Baseball`;
        case 48:
          return `${value} Little League Softball`;
      }
      // displaying other groupings/separators is possible, look at the docs
    });
    Vue.filter('player', function(value) {
      if (value === true) {
        return 'Player';
      }
      return 'Volunteer';
      // displaying other groupings/separators is possible, look at the docs
    });
    Vue.filter('category', function(value) {
      const ct = dataService.controlTypes();
      const item = ct.find(x => x.value === value);
      if (item) {
        return item.title;
      }
      return value;
      // displaying other groupings/separators is possible, look at the docs
    });

    Vue.filter('formatDateTime', function(value) {
      try {
        const offset = moment().utcOffset();
        return moment
          .utc(value)
          .utcOffset(offset)
          .format('L LTS');
      } catch (e) {
        return '-';
      }
    });
    Vue.filter('emptyString', function(value) {
      try {
        if (value) {
          return value.toString();
        }
      } catch (e) {
        return '-';
      }
    });
    Vue.filter('formatDate', function(value) {
      try {
        return moment(value)
          .local()
          .format('MM/DD/YYYY');
      } catch (e) {
        return '-';
      }
    });
    Vue.filter('limit', function(value) {
      return helper.limit(value);
    });
    Vue.filter('formatLevel', function(value) {
      return helper.formatLogLevel(value);
    });
    Vue.filter('truncate', function(value) {
      if (value && value.length > 50) {
        try {
          return value.substring(0, 50) + '...';
        } catch (e) {
          return value;
        }
      }
      return value;
    });
    Vue.filter('formatMS', function(value) {
      if (value > 1000 && value < 60000) {
        const seconds = value / 1000;
        return numeral(seconds).format('0.0000') + 's';
      } else if (value >= 60000) {
        const seconds = value / 60000;
        return numeral(seconds).format('0.0000') + 'm';
      }
      return numeral(value).format('0.0000') + 'ms'; // displaying other groupings/separators is possible, look at the docs
    });
    Vue.filter('pretty', function(value) {
      try {
        return JSON.stringify(JSON.parse(value), null, 2);
      } catch (e) {
        return value;
      }
    });
  }
};
export default CustomFilters;
