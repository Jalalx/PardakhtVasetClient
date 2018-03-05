using System;

namespace Septa.PardakhtVaset.Client
{
    public interface IPardakhtVasetClient : IDisposable
    {
        IPaymentLinkNotificationService PaymentLinkNotificationService { get; }

        IPaymentLinkRepository PaymentLinkRepository { get; }

        PardakhtVasetClientOptions Options { get; }

        void Init(string clusterId);

        bool Test(string apiKey);

        PaymentLink Create(decimal amount, string followId, string invoiceNumber, DateTime invoiceDate, ushort expireAfterDays, string description);
    }
}
