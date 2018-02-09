namespace Septa.PardakhtVaset.Client
{
    public class PardakhtVasetClientOptions
    {
        public PardakhtVasetClientOptions()
        {
            TablePrefix = string.Empty;
        }

        public string TablePrefix { get; set; }

        public string ConnectionString { get; set; }
    }
}
