namespace Septa.PardakhtVaset.Client
{
    public class PardakhtVasetClientOptions
    {
        public PardakhtVasetClientOptions()
        {
            DefaultSchema = "dbo";
            TablePrefix = string.Empty;
        }

        public string TablePrefix { get; set; }

        public string DefaultSchema { get; set; }

        public string ConnectionString { get; set; }
    }
}
