using System.Collections.Generic;
using FinanceWebLib;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceDataAccess
{
    public class IncomeRepository : BaseRepository
    {
        public IncomeRepository(IOptions<FinanceDbOptions> dbOptions) : base(dbOptions)
        {
        }

        protected override NpgsqlCommand GetCommand(NpgsqlConnection conn, int minAge, int maxAge)
        {
            var command = conn.CreateCommand();

            command.CommandText = "SELECT SUM(weight) AS weight, income FROM survey_data WHERE age BETWEEN @ageStart AND @ageEnd GROUP BY income";
            command.Parameters.AddWithValue("@ageStart", minAge);
            command.Parameters.AddWithValue("@ageEnd", maxAge);

            return command;
        }

        protected override List<SurveyData> ReadData(NpgsqlCommand command)
        {
            var samples = new List<SurveyData>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var weight = reader.GetDouble(reader.GetOrdinal("weight"));
                var income = reader.GetDouble(reader.GetOrdinal("income"));

                samples.Add(new SurveyData { Weight = weight, Data = income });
            }

            return samples;
        }
    }
}
