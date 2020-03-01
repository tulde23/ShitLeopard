import { GridModel } from './grid.model';

export class SiteMetricGridModel extends GridModel {
  public get headers() {
    return [
      {
        text: 'IP',
        align: 'left',
        sortable: false,
        value: 'ipaddress'
      },
      {
        text: 'Use-Agent',
        align: 'left',
        sortable: false,
        value: 'agentString'
      },
      {
        text: 'Route',
        align: 'left',
        sortable: false,
        value: 'route'
      },
      {
        text: 'Access Time',
        align: 'left',
        sortable: false,
        value: 'lastAccessTime'
      }
    ];
  }
}
