using PardakhtVasetServices;
using System;
using System.Collections;
using System.Collections.Generic;
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

        protected IDbInitializer DbInitializer { get; }

        public PardakhtVasetClientOptions Options { get; }

        public void Init()
        {
            DbInitializer.Init(Options.TablePrefix);
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
