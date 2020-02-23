import { ScriptLine } from './ScriptLine';

export class ScriptWord {
  constructor(
    public id?: number,
    public scriptLineId?: number,
    public word?: string,
    public scriptLine?: ScriptLine
  ) {}
}
