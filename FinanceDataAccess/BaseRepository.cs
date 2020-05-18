using System.Collections.Generic;
using FinanceWebLib;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceDataAccess
{
    public abstract class BaseRepository
    {
        protected BaseRepository(IOptions<FinanceDbOptions> dbOptions)
        {
            _connStr = dbOptions.Value.FinanceDatabase;
        }

        public IList<SurveyData> GetSamples(int minAge = 0, int maxAge = int.MaxValue)
        {
            using var conn = OpenConnection();
            using var command = GetCommand(conn, minAge, maxAge);
            return ReadData(command);
        }

        protected abstract NpgsqlCommand GetCommand(NpgsqlConnection conn, int minAge, int maxAge);

        protected abstract List<SurveyData> ReadData(NpgsqlCommand command);

        private NpgsqlConnection OpenConnection()
        {
            var conn = new NpgsqlConnection(_connStr);
            conn.Open();
            return conn;
        }

        private readonly string _connStr;
    }
}
