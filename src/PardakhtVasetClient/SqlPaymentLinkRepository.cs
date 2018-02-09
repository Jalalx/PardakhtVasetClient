using Septa.PardakhtVaset.Client.Internals;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Septa.PardakhtVaset.Client
{
    public class SqlPaymentLinkRepository : IPaymentLinkRepository
    {
        public SqlPaymentLinkRepository(PardakhtVasetClientOptions options)
        {
            Options = options;
        }

        public PardakhtVasetClientOptions Options { get; }

        protected string PaymentLinkTableName
        {
            get
            {
                return $"dbo.[{Options.TablePrefix}PaymentLink]";
            }
        }

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(Options.ConnectionString);
        }

        public bool Delete(PaymentLink link)
        {
            int result = 0;
            using (var connection = CreateConnection())
            {
                if (!string.IsNullOrEmpty(link.Token))
                {
                    var command = $"DELETE p FROM {PaymentLinkTableName} AS p WHERE p.[Token] = @Token";
                    result = connection.Execute(command, new { Token = link.Token });
                }
                else if (!string.IsNullOrEmpty(link.FollowId))
                {
                    var command = $"DELETE p FROM {PaymentLinkTableName} AS p WHERE p.[FollowId] = @FollowId";
                    result = connection.Execute(command, new { FollowId = link.FollowId });
                }
            }

            return result > 0;
        }

        public PaymentLink FindByFollowId(string followId)
        {
            using (var connection = CreateConnection())
            {
                var q = connection.Query<PaymentLink>($"SELECT p FROM ${PaymentLinkTableName} AS p WHERE p.[FollowId] = @FollowId", new { FollowId = followId });
                return q.FirstOrDefault();
            }
        }

        public PaymentLink FindByToken(string token)
        {
            using (var connection = CreateConnection())
            {
                var q = connection.Query<PaymentLink>($"SELECT p.* FROM ${PaymentLinkTableName} AS p WHERE p.[Token] = @Token", new { Token = token });
                return q.FirstOrDefault();
            }
        }

        public IEnumerable<PaymentLink> GetAll(int pageIndex, int pageSize, out int total)
        {
            total = 0;
            using (var connection = CreateConnection())
            {
                var query = $"SELECT ROWNUMBER(), p.* FROM {PaymentLinkTableName} AS p";

                return connection.Query<PaymentLink>(query);
            }
        }

        public int Insert(PaymentLink link)
        {
            using (var connection = CreateConnection())
            {
                var sql = $"INSERT INTO {PaymentLinkTableName}(Id, Amount, FollowId, Description, ExpireDays, CreateDate, Url, Token, PaymentStatus)VALUES(@Id, @Amount, @FollowId, @Description, @ExpireDays, @CreateDate, @Url, @Token, @PaymentStatus);";
                return connection.Execute(sql, link);
            }
        }

        public int Update(PaymentLink link)
        {
            using (var connection = CreateConnection())
            {
                var sql = $"UPDATE {PaymentLinkTableName} SET Amount=@Amount, FollowId=@FollowId, Description=@Description, ExpireDays=@ExpireDays, CreateDate=@CreateDate, Url=@Url, Token=@Token, PaymentStatus=@PaymentStatus WHERE Id=@Id;";
                return connection.Execute(sql, link);
            }
        }
    }
}
