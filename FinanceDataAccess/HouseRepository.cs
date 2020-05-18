using System.Collections.Generic;
using FinanceWebLib;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceDataAccess
{
    public class HouseRepository : BaseRepository
    {
        public HouseRepository(IOptions<FinanceDbOptions> dbOptions) : base(dbOptions)
        {
        }

        protected override NpgsqlCommand GetCommand(NpgsqlConnection conn, int minAge, int maxAge)
        {
            var command = conn.CreateCommand();

            command.CommandText = "SELECT SUM(weight) AS weight, house_value FROM survey_data WHERE house_owned = TRUE AND age BETWEEN @ageStart AND @ageEnd GROUP BY house_value";
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
                var houseValue = reader.GetDouble(reader.GetOrdinal("house_value"));

                samples.Add(new SurveyData { Weight = weight, Data = houseValue });
            }

            return samples;
        }
    }
}
