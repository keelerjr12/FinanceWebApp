using FinanceLib;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceDataAccess
{
    public class HouseRepository : BaseRepository
    {
        public HouseRepository(IOptions<FinanceDbOptions> dbOptions) : base(dbOptions)
        {
        }

        protected override NpgsqlCommand CreateCommand(NpgsqlConnection conn, int minAge, int maxAge)
        {
            var command = conn.CreateCommand();

            command.CommandText = "SELECT SUM(weight) AS weight, house_value FROM survey_data WHERE house_owned = TRUE AND age BETWEEN @ageStart AND @ageEnd GROUP BY house_value";
            command.Parameters.AddWithValue("@ageStart", minAge);
            command.Parameters.AddWithValue("@ageEnd", maxAge);

            return command;
        }

        protected override SurveyData ParseSample(NpgsqlDataReader reader)
        {
            var weight = reader.GetDouble(reader.GetOrdinal("weight"));
            var houseValue = reader.GetDouble(reader.GetOrdinal("house_value"));

            return new SurveyData { Weight = weight, Data = houseValue };
        }
    }
}
