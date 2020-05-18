using System.Collections.Generic;
using FinanceLib;
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
            using var command = CreateCommand(conn, minAge, maxAge);
            return ReadData(command);
        }

        protected abstract NpgsqlCommand CreateCommand(NpgsqlConnection conn, int minAge, int maxAge);

        protected abstract SurveyData ParseSample(NpgsqlDataReader reader);

        private NpgsqlConnection OpenConnection()
        {
            var conn = new NpgsqlConnection(_connStr);
            conn.Open();

            return conn;
        }

        private List<SurveyData> ReadData(NpgsqlCommand command)
        {
            var samples = new List<SurveyData>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var sample = ParseSample(reader);
                samples.Add(sample);
            }

            return samples;
        }

        private readonly string _connStr;
    }
}
