import { ScriptLine } from './ScriptLine';

export class Character {
  constructor(
    public id?: number,
    public name?: string,
    public aliases?: string,
    public notes?: string,
    public playedBy?: string,
    public scriptLine?: ScriptLine[]
  ) {}
}
