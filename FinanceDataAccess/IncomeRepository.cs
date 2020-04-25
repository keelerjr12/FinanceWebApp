using System.Collections.Generic;
using FinanceWebLib;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceDataAccess
{
    public class IncomeRepository
    {
        public IncomeRepository(IOptions<FinanceDbOptions> dbOptions)
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

                command.CommandText = "SELECT SUM(weight) AS weight, income FROM survey_data WHERE age BETWEEN @ageStart AND @ageEnd GROUP BY income";
                command.Parameters.AddWithValue("@ageStart", minAge);
                command.Parameters.AddWithValue("@ageEnd", maxAge);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weight = reader.GetDouble(reader.GetOrdinal("weight"));
                    var income = reader.GetDouble(reader.GetOrdinal("income"));

                    samples.Add(new SurveyData { Weight = weight, Data = income });
                }
            }

            return samples;
        }

        private readonly string _connStr;
    }
}
