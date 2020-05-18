using FinanceWebLib;
using System.Collections.Generic;
using Npgsql;
using Microsoft.Extensions.Options;

namespace FinanceDataAccess
{
    public class NetworthRepository : BaseRepository
    {
        public NetworthRepository(IOptions<FinanceDbOptions> dbOptions) : base(dbOptions)
        {
        }

        protected override NpgsqlCommand GetCommand(NpgsqlConnection conn, int minAge, int maxAge)
        {
            var command = conn.CreateCommand();

            command.CommandText = "SELECT SUM(weight) AS weight, networth from survey_data where age between @ageStart and @ageEnd GROUP BY networth";
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
                var networth = reader.GetDouble(reader.GetOrdinal("networth"));

                samples.Add(new SurveyData { Weight = weight, Data = networth });
            }

            return samples;
        }
    }
}
