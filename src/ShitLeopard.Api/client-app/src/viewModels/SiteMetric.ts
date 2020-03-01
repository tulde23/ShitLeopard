export class SiteMetric {
  constructor(
    public headers?: any,
    public ipaddress?: string,
    public agentString?: string,
    public route?: string,
    public lastAccessTime?: string
  ) {}
}
