import moment from 'moment';

export class Helper {
  public formatDateTime(value: any) {
    if (!value || value.length < 1) {
      return '-';
    }
    try {
      return (
        moment(value)
          // .local()
          .format('MM/DD/YYYY hh:mm:ss:SSS a')
      );
    } catch (e) {
      return '-';
    }
  }
  public formatDate(value: any) {
    try {
      return moment(value)
        .local()
        .format('MM/DD/YYYY');
    } catch (e) {
      return '-';
    }
  }
  public limit(message: string) {
    if (message) {
      return message.substring(0, 150) + '...';
    }
    return '';
  }
}
