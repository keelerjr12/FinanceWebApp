using System;
using System.Collections.Generic;
using FinanceWebLib;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceDataAccess
{
    public class HouseRepository
    {
        public HouseRepository(IOptions<FinanceDbOptions> dbOptions)
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

                command.CommandText = "SELECT SUM(weight) AS weight, house_value FROM survey_data WHERE house_owned = TRUE AND age BETWEEN @ageStart AND @ageEnd GROUP BY house_value";
                command.Parameters.AddWithValue("@ageStart", minAge);
                command.Parameters.AddWithValue("@ageEnd", maxAge);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weight = reader.GetDouble(reader.GetOrdinal("weight"));
                    var houseValue = reader.GetDouble(reader.GetOrdinal("house_value"));

                    samples.Add(new SurveyData { Weight = weight, Data = houseValue });
                }
            }

            return samples;
        }

        private readonly string _connStr;
    }
}
