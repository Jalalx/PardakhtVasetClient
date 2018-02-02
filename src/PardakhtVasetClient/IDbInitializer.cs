namespace Septa.PardakhtVaset.Client
{
    public interface IDbInitializer
    {
        void Init(string schema, string tablePrefix);
    }
}