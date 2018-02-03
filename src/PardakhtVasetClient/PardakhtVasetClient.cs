using PardakhtVasetServices;
using System;
using System.ServiceModel;

namespace Septa.PardakhtVaset.Client
{
    public class PardakhtVasetClient : IPardakhtVasetClient
    {
        public PardakhtVasetClient(PardakhtVasetClientOptions options) :
            this(options, options == null ? throw new ArgumentNullException(nameof(options)) : new SqlServerDbInitializer(options.ConnectionString))
        {
        }

        public PardakhtVasetClient(PardakhtVasetClientOptions options, IDbInitializer dbInitializer)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            DbInitializer = dbInitializer ?? throw new ArgumentNullException(nameof(dbInitializer));
        }

        public IDbInitializer DbInitializer { get; }

        public IDbCommandExecutor DbCommandExecutor { get; }

        public PardakhtVasetClientOptions Options { get; }

        public void Init()
        {
            DbInitializer.Init(Options.DefaultSchema, Options.TablePrefix);
        }

        public bool Test(string apiKey)
        {
            var basicHttp = new BasicHttpBinding();
            basicHttp.Security.Mode = BasicHttpSecurityMode.Transport;
            var payRequestService = new PayRequestClient(basicHttp, new EndpointAddress("https://service.pardakhtvaset.com/API/PayRequest.svc/IPayRequestSsl"));
            return payRequestService.Verify(apiKey);
        }
    }
}
