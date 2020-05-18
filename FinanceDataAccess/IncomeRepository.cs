using FinanceLib;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceDataAccess
{
    public class IncomeRepository : BaseRepository
    {
        public IncomeRepository(IOptions<FinanceDbOptions> dbOptions) : base(dbOptions)
        {
        }

        protected override NpgsqlCommand CreateCommand(NpgsqlConnection conn, int minAge, int maxAge)
        {
            var command = conn.CreateCommand();

            command.CommandText = "SELECT SUM(weight) AS weight, income FROM survey_data WHERE age BETWEEN @ageStart AND @ageEnd GROUP BY income";
            command.Parameters.AddWithValue("@ageStart", minAge);
            command.Parameters.AddWithValue("@ageEnd", maxAge);

            return command;
        }

        protected override SurveyData ParseSample(NpgsqlDataReader reader)
        {
            var weight = reader.GetDouble(reader.GetOrdinal("weight"));
            var income = reader.GetDouble(reader.GetOrdinal("income"));

            return new SurveyData { Weight = weight, Data = income };
        }
    }
}
