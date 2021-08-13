import { GridModel } from './grid.model';

export class SiteMetricGridModel extends GridModel {
  public get headers() {
    return [
      {
        text: 'IP',
        align: 'left',
        sortable: false,
        value: 'ipaddress',
      },

      {
        text: 'Route',
        align: 'left',
        sortable: false,
        value: 'route',
      },
      {
        text: 'Access Time',
        align: 'left',
        sortable: false,
        value: 'lastAccessTime',
      },
      {
        text: 'Term',
        align: 'left',
        sortable: false,
        value: 'body',
      },
    ];
  }
}
