export class Quote {
  constructor(
    public id?: number,
    public characterId?: number,
    public scriptLineId?: number,
    public body?: string,
    public popularity?: number
  ) {}
}
