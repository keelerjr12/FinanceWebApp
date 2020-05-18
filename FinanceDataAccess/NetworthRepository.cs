using FinanceLib;
using Npgsql;
using Microsoft.Extensions.Options;

namespace FinanceDataAccess
{
    public class NetworthRepository : BaseRepository
    {
        public NetworthRepository(IOptions<FinanceDbOptions> dbOptions) : base(dbOptions)
        {
        }

        protected override NpgsqlCommand CreateCommand(NpgsqlConnection conn, int minAge, int maxAge)
        {
            var command = conn.CreateCommand();

            command.CommandText = "SELECT SUM(weight) AS weight, networth from survey_data where age between @ageStart and @ageEnd GROUP BY networth";
            command.Parameters.AddWithValue("@ageStart", minAge);
            command.Parameters.AddWithValue("@ageEnd", maxAge);

            return command;
        }

        protected override SurveyData ParseSample(NpgsqlDataReader reader)
        {
            var weight = reader.GetDouble(reader.GetOrdinal("weight"));
            var networth = reader.GetDouble(reader.GetOrdinal("networth"));

            return new SurveyData { Weight = weight, Data = networth };
        }
    }
}
