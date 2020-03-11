import { GridModel } from './grid.model';

export class QuestionGridModel extends GridModel {
  public get headers() {
    return [
      {
        text: 'Body',
        align: 'left',
        sortable: false,
        value: 'body'
      },
      {
        text: 'Season',
        align: 'left',
        sortable: false,
        value: 'seasonId'
      },
      {
        text: 'Title',
        align: 'left',
        sortable: false,
        value: 'episodeTitle'
      },
      {
        text: '',
        align: 'left',
        sortable: false,
        value: 'episodeId'
      }
    ];
  }
}
