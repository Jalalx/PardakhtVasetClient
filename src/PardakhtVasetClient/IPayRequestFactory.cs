using PardakhtVasetServices;

namespace Septa.PardakhtVaset.Client
{
    public interface IPayRequestFactory
    {
        IPayRequest Create();

        IPayRequestV2 CreateV2();
    }
}