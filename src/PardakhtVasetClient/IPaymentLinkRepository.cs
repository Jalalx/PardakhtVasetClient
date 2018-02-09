using System.Collections.Generic;

namespace Septa.PardakhtVaset.Client
{
    public interface IPaymentLinkRepository
    {
        IEnumerable<PaymentLink> GetAll(int pageIndex, int pageSize, out int total);

        PaymentLink FindByToken(string token);

        PaymentLink FindByFollowId(string followId);

        int Insert(PaymentLink link);

        int Update(PaymentLink link);

        bool Delete(PaymentLink link);
    }
}
