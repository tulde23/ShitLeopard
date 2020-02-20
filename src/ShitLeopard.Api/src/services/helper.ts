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
  public groupBy(OurArray, property) {
    return OurArray.reduce(function(accumulator, object) {
      // get the value of our object(age in our case) to use for group    the array as the array key
      const key = object[property];
      console.log('key', key);
      // if the current value is similar to the key(age) don't accumulate the transformed array and leave it empty
      if (!accumulator[key]) {
        accumulator[key] = [];
      }
      // add the value to the array
      accumulator[key].push(object);
      // return the transformed array
      return accumulator;
      // Also we also set the initial value of reduce() to an empty object
    }, {});
  }
}
