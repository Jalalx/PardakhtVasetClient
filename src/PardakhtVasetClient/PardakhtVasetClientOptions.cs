﻿namespace Septa.PardakhtVaset.Client
{
    public class PardakhtVasetClientOptions
    {
        public PardakhtVasetClientOptions()
        {
            TablePrefix = string.Empty;
        }

        public string TablePrefix { get; set; }

        public string ConnectionString { get; set; }

        public string ApiKey { get; set; }

        public string Password { get; set; }
    }
}
