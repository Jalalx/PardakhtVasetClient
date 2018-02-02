namespace Septa.PardakhtVaset.Client
{
    public interface IDbCommandExecutor
    {
        int Execute(string sql, object param);
    }
}
