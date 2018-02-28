using System;

namespace Septa.PardakhtVaset.Client
{
    public interface IPaymentLinkNotificationService
    {
        string ClusterId { get; set; }

        event PaymentLinkChangedEventHandler PaymentLinkChanged;

        void OnTick();

        void Start(TimeSpan interval);

        void Stop();
    }
}