import { GridModel } from '@/models';

export class UsersViewModel extends GridModel{
    constructor() {
        super();
    }
    public get headers() {
        return [
          {
            text: 'Username',
            align: 'left',
            sortable: false,
            value: 'requestId'
          },
          {
            text: 'Email',
            align: 'left',
            sortable: false,
            value: 'ngbid'
          },
          {
            text: '',
            align: 'left',
            sortable: false,
            value: 'ngbid'
          }
          
        ];
      }
}