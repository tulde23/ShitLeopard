import { Character } from './Character';
import { Script } from './Script';
import { ScriptWord } from './ScriptWord';

export class ScriptLine {
  constructor(
    public id?: number,
    public body?: string,
    public scriptId?: number,
    public characterId?: number,
    public character?: Character,
    public script?: Script,
    public scriptWord?: ScriptWord[],
    public episodeId?: number,
    public seasonId?: number,
    public episodeTitle?: string
  ) {}
}
