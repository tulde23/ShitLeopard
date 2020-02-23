import { Episode } from './Episode';
import { ScriptLine } from './ScriptLine';

export class Script {
  constructor(
    public id?: number,
    public episodeId?: number,
    public body?: string,
    public episode?: Episode,
    public scriptLine?: ScriptLine[]
  ) {}
}
