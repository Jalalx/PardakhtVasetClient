using System;

namespace Septa.PardakhtVaset.Client
{
    public interface IPardakhtVasetClient
    {
        void Init();

        bool Test(string apiKey);

        PaymentLink Create(decimal amount, string followId, string invoiceNumber, DateTime invoiceDate, ushort expireAfterDays, string description);
    }
}
