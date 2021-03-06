﻿using System;
using System.Diagnostics;

namespace Septa.PardakhtVaset.Client
{
    public class PaymentLinkNotificationService : TimerServiceBase, IPaymentLinkNotificationService
    {
        public PardakhtVasetClientOptions Options { get; }
        public IPaymentLinkRepository PaymentLinkRepository { get; }
        public PardakhtVasetServices.IPayRequestV2 PayRequestService { get; }

        public string ClusterId { get; set; }

        private event PaymentLinkChangedEventHandler _paymentLinkChanged;
        public event PaymentLinkChangedEventHandler PaymentLinkChanged
        {
            add
            {
                _paymentLinkChanged += value;
            }
            remove
            {
                _paymentLinkChanged -= value;
            }
        }

        public PaymentLinkNotificationService(PardakhtVasetClientOptions options, IPaymentLinkRepository paymentLinkRepository, PardakhtVasetServices.IPayRequestV2 payRequestService)
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
                var nextPaymentLink = PaymentLinkRepository.GetNextLinkForCheck(ClusterId);
                nextPaymentToken = nextPaymentLink.Token;
                Trace.TraceInformation("Next payment link fetched from database. Token: '{0}', FollowId: '{1}'", nextPaymentLink.Token, nextPaymentLink.FollowId);

                Trace.TraceInformation("Checking payment link status for token '{0}'", nextPaymentLink.Token);
                var result = PayRequestService.Check(Options.ApiKey, Options.Password, nextPaymentLink.Token);
                
                var e = new PaymentLinkChangedEventArgs();
                e.Success = result.Success;
                e.Message = result.Message;
                e.Token = nextPaymentLink.Token;
                e.FollowId = nextPaymentLink.FollowId;
                e.Status = result.RequestStatus;
                e.ResultDate = result.VerifyDate;

                if (result.Success)
                {
                    Trace.TraceInformation("IPayRequest.Check called successfully.");
                }
                else
                {
                    Trace.TraceError("IPayRequest.Check call was not successful. Error Type: {0}, Server Message: '{1}'", result.ExceptionType, result.Message);
                }

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
                _paymentLinkChanged?.Invoke(this, e);
                Trace.TraceInformation("Event handler for PaymentLinkChanged called.");
            }
            catch (Exception ex)
            {
                Trace.TraceError("An exception was thrown when trying to call PaymentLinkChanged handler: {0}", ex);
            }
        }

        public override void Dispose()
        {
            _paymentLinkChanged = null;
            base.Dispose();
        }
    }
}
