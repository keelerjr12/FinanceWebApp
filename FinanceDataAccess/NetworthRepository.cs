using FinanceWebLib;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Options;

namespace FinanceDataAccess
{
    public class NetworthRepository
    {
        public NetworthRepository(IOptions<FinanceDbOptions> dbOptions)
        {
            _connStr = dbOptions.Value.FinanceDatabase;
        }

        public IList<SurveyData> GetSamples(int minAge = 0, int maxAge = int.MaxValue)
        {
            var samples = new List<SurveyData>();

            using (var conn = new NpgsqlConnection(_connStr))
            {
                conn.Open();
                using var command = conn.CreateCommand();

                command.CommandText = "SELECT SUM(weight) AS weight, networth from survey_data where age between @ageStart and @ageEnd GROUP BY networth";
                command.Parameters.AddWithValue("@ageStart", minAge);
                command.Parameters.AddWithValue("@ageEnd", maxAge);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weight = reader.GetDouble(reader.GetOrdinal("weight"));
                    var networth = reader.GetDouble(reader.GetOrdinal("networth"));

                    samples.Add(new SurveyData { Weight = weight, Data = networth });
                }
            }

            return samples;
        }

        private readonly string _connStr;
    }
}
