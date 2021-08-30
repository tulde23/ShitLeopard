export class SiteMetric {
  constructor(
    public id?: number,
    public headers?: any,
    public ipaddress?: string,
    public agentString?: string,
    public route?: string,
    public lastAccessTime?: string,
    public body?: string
  ) {}
}
