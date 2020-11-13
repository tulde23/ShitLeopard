export class DialogModel {
  constructor(
    public id?: string,
    public dialogLineNumber?: number,
    public start?: string,
    public end?: string,
    public body?: string,
    public episodeNumber?: string,
    public episodeOffsetId?: string,
    public episodeTitle?: string,
    public seasonId?: string,
    public synopsis?: string
  ) {}
}
