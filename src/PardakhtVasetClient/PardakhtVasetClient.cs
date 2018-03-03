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
            this(options, options == null ? throw new ArgumentNullException(nameof(options)) : new SqlServerDbInitializer(options.ConnectionString),
                options == null ? throw new ArgumentNullException(nameof(options)) : new SqlPaymentLinkRepository(options), new PayRequestFactory())
        {
        }

        public PardakhtVasetClient(PardakhtVasetClientOptions options, IDbInitializer dbInitializer, IPaymentLinkRepository paymentLinkRepository, IPayRequestFactory payRequestFactory)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            DbInitializer = dbInitializer ?? throw new ArgumentNullException(nameof(dbInitializer));
            PaymentLinkRepository = paymentLinkRepository ?? throw new ArgumentNullException(nameof(paymentLinkRepository));
            PayRequestFactory = payRequestFactory ?? throw new ArgumentNullException(nameof(payRequestFactory));

            PaymentLinkNotificationService = new PaymentLinkNotificationService(options, paymentLinkRepository, payRequestFactory.Create());
        }

        protected IDbInitializer DbInitializer { get; }

        public IPaymentLinkRepository PaymentLinkRepository { get; }

        protected IPayRequestFactory PayRequestFactory { get; }

        public PardakhtVasetClientOptions Options { get; }

        public IPaymentLinkNotificationService PaymentLinkNotificationService
        {
            get;
        }

        public void Init(string clusterId)
        {
            DbInitializer.Init(Options.TablePrefix);
            PaymentLinkNotificationService.ClusterId = clusterId;
        }

        public bool Test(string apiKey)
        {
            return PayRequestFactory.Create().Verify(apiKey);
        }

        public PaymentLink Create(decimal amount, string followId, string invoiceNumber, DateTime invoiceDate, ushort expireAfterDays, string description)
        {
            var service = PayRequestFactory.Create();
            var request = new EPayRequestModel();
            request.Amount = amount;
            request.Description = description;
            request.ExpiresAfterDays = expireAfterDays;
            request.InvoiceNumber = invoiceNumber;
            request.InvoiceDate = invoiceDate;
            request.IsAutoRedirect = false;

            var result = service.Create(Options.ApiKey, request);
            if (result.Success)
            {
                var link = new PaymentLink();
                link.Amount = amount;
                link.Description = description;
                link.ExpireDays = expireAfterDays;
                link.FollowId = followId;
                link.CreateDate = DateTime.Now;
                link.LastCheckForUpdateDate = DateTime.Now;
                link.PaymentStatus = (int)RequestStatus.Initiated;
                link.Token = result.RequestToken;
                link.Url = result.PaymentUrl;
                link.ResultDate = null;
                link.Id = Guid.NewGuid();
                link.ClusterId = PaymentLinkNotificationService.ClusterId;
                PaymentLinkRepository.Insert(link);

                return link;
            }
            else
            {
                throw new InvalidOperationException(result.Message);
            }
        }
    }
}
