import moment from 'moment';

export class Helper {
  public serialize(params: object) {
    const str = [];
    for (const p in params) {
      if (params.hasOwnProperty(p)) {
        if (params[p]) {
          str.push(encodeURIComponent(p) + '=' + encodeURIComponent(params[p]));
        }
      }
    }
    return str.join('&');
  }
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
