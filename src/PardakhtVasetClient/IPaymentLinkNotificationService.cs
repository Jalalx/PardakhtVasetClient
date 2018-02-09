using System;

namespace Septa.PardakhtVaset.Client
{
    public interface IPaymentLinkNotificationService
    {
        event PaymentLinkChangedEventHandler PaymentLinkChanged;

        void Start(TimeSpan interval);

        void Stop();
    }
}