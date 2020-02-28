import { GridModel } from './grid.model';

export class BrowseGridModel extends GridModel {
  public get headers() {
    return [
      {
        text: 'Id Number',
        align: 'left',
        sortable: false,
        value: 'idNum'
      },
      {
        text: 'Player JsonPath',
        align: 'left',
        sortable: false,
        value: 'playerId'
      },
      {
        text: 'First Name',
        align: 'left',
        sortable: false,
        value: 'firstName'
      },
      {
        text: 'Last Name',
        align: 'left',
        sortable: false,
        value: 'lastName'
      },
      {
        text: 'Birthday',
        align: 'left',
        sortable: false,
        value: 'email'
      },
      {
        text: '',
        align: 'left',
        sortable: false,
        value: 'email'
      }
    ];
  }
}
