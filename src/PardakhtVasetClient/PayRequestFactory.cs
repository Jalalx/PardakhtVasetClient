using PardakhtVasetServices;
using System.ServiceModel;

namespace Septa.PardakhtVaset.Client
{
    public class PayRequestFactory : IPayRequestFactory
    {
        const string PardakhtVasetServiceUrl = "https://service.pardakhtvaset.com/API/PayRequest.svc/IPayRequestSsl";

        public IPayRequest Create()
        {
            var basicHttp = new BasicHttpBinding();
            basicHttp.Security.Mode = BasicHttpSecurityMode.Transport;
            return new PayRequestClient(basicHttp, new EndpointAddress(PardakhtVasetServiceUrl));
        }
    }
}
