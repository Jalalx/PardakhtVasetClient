using System;

namespace Septa.PardakhtVaset.Client
{
    public class PardakhtVasetClient
    {
        public PardakhtVasetClient(PardakhtVasetClientOptions options) :
            this(options, options == null ? throw new ArgumentNullException(nameof(options)) : new SqlServerDbInitializer(options.ConnectionString))
        {
        }

        public PardakhtVasetClient(PardakhtVasetClientOptions options, IDbInitializer dbInitializer)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            DbInitializer = dbInitializer ?? throw new ArgumentNullException(nameof(dbInitializer));
        }

        public IDbInitializer DbInitializer { get; }
        public IDbCommandExecutor DbCommandExecutor { get; }
        public PardakhtVasetClientOptions Options { get; }

        public void Init()
        {
            DbInitializer.Init(Options.DefaultSchema, Options.TablePrefix);
        }
    }
}
