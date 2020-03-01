export class PagedResult<T> {
  constructor(public count: number, public result: Array<T>) {}
}
