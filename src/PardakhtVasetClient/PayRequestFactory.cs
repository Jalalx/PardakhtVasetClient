using PardakhtVasetServices;
using System.ServiceModel;
using System;

namespace Septa.PardakhtVaset.Client
{
    public class PayRequestFactory : IPayRequestFactory
    {
        const string PardakhtVasetServiceUrl = "https://service.pardakhtvaset.com/API/PayRequest.svc/IPayRequestSsl";
        const string PardakhtVasetServiceV2Url = "https://service.pardakhtvaset.com/API/PayRequestV2.svc/IPayRequestSsl";

        public IPayRequest Create()
        {
            var basicHttp = new BasicHttpBinding();
            basicHttp.Security.Mode = BasicHttpSecurityMode.Transport;
            return new PayRequestClient(basicHttp, new EndpointAddress(PardakhtVasetServiceUrl));
        }

        public IPayRequestV2 CreateV2()
        {
            var basicHttp = new BasicHttpBinding();
            basicHttp.Security.Mode = BasicHttpSecurityMode.Transport;
            return new PayRequestV2Client(basicHttp, new EndpointAddress(PardakhtVasetServiceV2Url));
        }
    }
}
