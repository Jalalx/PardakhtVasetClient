using System;
using System.Diagnostics;

namespace Septa.PardakhtVaset.Client
{
    public class PaymentLinkNotificationService : TimerServiceBase, IPaymentLinkNotificationService
    {
        public PardakhtVasetClientOptions Options { get; }
        public IPaymentLinkRepository PaymentLinkRepository { get; }
        public PardakhtVasetServices.IPayRequest PayRequestService { get; }

        public event PaymentLinkChangedEventHandler PaymentLinkChanged;

        public PaymentLinkNotificationService(PardakhtVasetClientOptions options, IPaymentLinkRepository paymentLinkRepository, PardakhtVasetServices.IPayRequest payRequestService)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            PaymentLinkRepository = paymentLinkRepository ?? throw new ArgumentNullException(nameof(paymentLinkRepository));
            PayRequestService = payRequestService ?? throw new ArgumentNullException(nameof(payRequestService));
        }

        public override void OnTick()
        {
            Trace.TraceInformation("OnTick call started.");
            string nextPaymentToken = default(string);
            try
            {
                var nextPaymentLink = PaymentLinkRepository.GetNextLinkForCheck();
                nextPaymentToken = nextPaymentLink.Token;
                Trace.TraceInformation("Next payment link fetched from database. Token: '{0}', FollowId: '{1}'", nextPaymentLink.Token, nextPaymentLink.FollowId);

                Trace.TraceInformation("Checking payment link status for token '{0}'", nextPaymentLink.Token);
                var result = PayRequestService.Check(Options.ApiKey, nextPaymentLink.Token);

                if (result.Success)
                {
                    Trace.TraceInformation("IPayRequest.Check called successfully.");

                    var e = new PaymentLinkChangedEventArgs();
                    e.Token = nextPaymentLink.Token;
                    e.FollowId = nextPaymentLink.FollowId;
                    e.Status = result.RequestStatus;
                    e.ResultDate = result.VerifyDate;

                    OnPaymentLinkChanged(e);

                    if (e.Handled)
                    {
                        nextPaymentLink.PaymentStatus = (int)result.RequestStatus;
                        nextPaymentLink.BankReferenceId = result.BankReferenceId;
                        nextPaymentLink.ResultDate = result.VerifyDate;
                        nextPaymentLink.LastCheckForUpdateDate = DateTime.Now;
                        PaymentLinkRepository.Update(nextPaymentLink);
                    }
                }
                else
                {
                    Trace.TraceError("IPayRequest.Check call was not successful. Error Type: {0}, Server Message: '{1}'", result.ExceptionType, result.Message);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("An exception was thrown when trying to update payment link token '{0}'. See exception details: {1}", nextPaymentToken, ex);
            }

            Trace.TraceInformation("OnTick call ended.");
        }

        protected void OnPaymentLinkChanged(PaymentLinkChangedEventArgs e)
        {
            try
            {
                Trace.TraceInformation("Trying to call event handler for PaymentLinkChanged.");
                PaymentLinkChanged?.Invoke(this, e);
                Trace.TraceInformation("Event handler for PaymentLinkChanged called.");
            }
            catch (Exception ex)
            {
                Trace.TraceError("An exception was thrown when trying to call PaymentLinkChanged handler: {0}", ex);
            }
        }
    }
}
