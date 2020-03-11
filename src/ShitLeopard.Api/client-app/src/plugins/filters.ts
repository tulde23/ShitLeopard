import { Helper } from '@/services/helper';
import moment from 'moment-timezone';

const helper = new Helper();

const CustomFilters = {
  install: (Vue: any, options: any) => {
    Vue.filter('formatDateTime', function(value: string) {
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
  }
};
export default CustomFilters;
