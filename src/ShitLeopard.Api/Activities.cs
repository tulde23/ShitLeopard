using System.Diagnostics;

namespace ShitLeopard.Api
{
    public static class Activities
    {
        public static ActivitySource GlobalActivitySource { get; } = new ActivitySource(
       "shitleopard.io");
    }

    /**
     *  .AddOtlpExporter(opt => {
                          opt.Endpoint = new System.Uri("ingest.lightstep.com:443");
                          opt.Headers = JsonConvert.SerializeObject(new Dictionary<string, string>()
                            {
                                { "lightstep-access-token", "C+YfA59w2n2xprCNiKOcPGX10aEZ6e1WfvWAWYuIGGn6iX5TOpQ6b6PoSFudHrzNxfjgi892ntGIjBmtFdjel0Mbd7cwQCxX8a1ELPPc"}
                            });
                          //opt.Credentials = new SslCredentials();
                      })
     * */
}