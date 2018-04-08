namespace PardakhtVasetServices
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ServiceReference.IPayRequestV2")]
    public interface IPayRequestV2
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPayRequestV2/Create", ReplyAction = "http://tempuri.org/IPayRequestV2/CreateResponse")]
        PardakhtVasetServices.EPayRequestResult Create(string apiKey, string password, PardakhtVasetServices.EPayRequestModel request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPayRequestV2/Check", ReplyAction = "http://tempuri.org/IPayRequestV2/CheckResponse")]
        PardakhtVasetServices.EPayRequestCheckResult Check(string apiKey, string password, string token);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPayRequestV2/Verify", ReplyAction = "http://tempuri.org/IPayRequestV2/VerifyResponse")]
        bool Verify(string apiKey, string password);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPayRequestV2/CreateDividedEPayRequest", ReplyAction = "http://tempuri.org/IPayRequestV2/CreateDividedEPayRequestResponse")]
        PardakhtVasetServices.EPayRequestResult CreateDividedEPayRequest(string dividerApiKey, string password, PardakhtVasetServices.DividedEPayRequestModel request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPayRequestV2/UnblockAmount", ReplyAction = "http://tempuri.org/IPayRequestV2/UnblockAmountResponse")]
        PardakhtVasetServices.CapitalUnblockResult UnblockAmount(string dividerApiKey, string dividerPassword, string apiKey, decimal amount, decimal dividerAmount, string description, string followId);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPayRequestV2/CancelAmount", ReplyAction = "http://tempuri.org/IPayRequestV2/CancelAmountResponse")]
        PardakhtVasetServices.CapitalCancelResult CancelAmount(string dividerApiKey, string dividerPassword, string apiKey, decimal amount, decimal dividerAmount, string shebaNO, string description, string followId);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPayRequestV2Channel : PardakhtVasetServices.IPayRequestV2, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PayRequestV2Client : System.ServiceModel.ClientBase<PardakhtVasetServices.IPayRequestV2>, PardakhtVasetServices.IPayRequestV2
    {

        public PayRequestV2Client()
        {
        }

        public PayRequestV2Client(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public PayRequestV2Client(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public PayRequestV2Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public PayRequestV2Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public PardakhtVasetServices.EPayRequestResult Create(string apiKey, string password, PardakhtVasetServices.EPayRequestModel request)
        {
            return base.Channel.Create(apiKey, password, request);
        }
        
        public PardakhtVasetServices.EPayRequestCheckResult Check(string apiKey, string password, string token)
        {
            return base.Channel.Check(apiKey, password, token);
        }
                
        public bool Verify(string apiKey, string password)
        {
            return base.Channel.Verify(apiKey, password);
        }
        
        public PardakhtVasetServices.EPayRequestResult CreateDividedEPayRequest(string dividerApiKey, string password, PardakhtVasetServices.DividedEPayRequestModel request)
        {
            return base.Channel.CreateDividedEPayRequest(dividerApiKey, password, request);
        }
        
        public PardakhtVasetServices.CapitalUnblockResult UnblockAmount(string dividerApiKey, string dividerPassword, string apiKey, decimal amount, decimal dividerAmount, string description, string followId)
        {
            return base.Channel.UnblockAmount(dividerApiKey, dividerPassword, apiKey, amount, dividerAmount, description, followId);
        }
        
        public PardakhtVasetServices.CapitalCancelResult CancelAmount(string dividerApiKey, string dividerPassword, string apiKey, decimal amount, decimal dividerAmount, string shebaNO, string description, string followId)
        {
            return base.Channel.CancelAmount(dividerApiKey, dividerPassword, apiKey, amount, dividerAmount, shebaNO, description, followId);
        }
    }
}
