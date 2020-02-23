import { Episode } from './Episode';

export class EpisodeGroup {
  constructor(public season?: string, public seasonId?: number, public episodes?: Episode[]) {}
}
