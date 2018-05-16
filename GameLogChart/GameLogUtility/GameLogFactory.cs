using System.Collections.Generic;
using System.Dynamic;
using Net.Code.Csv;

namespace Retrosheet.Utilities.GameLogUtility
{
    public class GameLogFactory
    {
        private readonly string[] _properties;

        public GameLogFactory(string[] properties)
        {
            _properties = properties;
        }

        public ExpandoObject CreateGameLogFromCsv(string csv)
        {
            var gameLog = new ExpandoObject() as IDictionary<string, object>;

            using (var csvReader = ReadCsv.FromString(csv))
            {
                while (csvReader.Read())
                {
                    for (var i = 0; i < _properties.Length; i++)
                    {
                        gameLog.Add(_properties[i], csvReader[i]);
                    }

                    var gameDate = (string) gameLog["game_date"];
                    var gameYear = gameDate.Substring(0, 4);
                    var gameMonth = gameDate.Substring(4, 2);
                    var gameDay = gameDate.Substring(6, 2);

                    gameLog.Add("id",
                        $"{gameYear}-{gameMonth}-{gameDay}-{gameLog["game_number"]}-{gameLog["home_team"]}");
                    gameLog.Add("game_month", byte.Parse(gameMonth));
                    gameLog.Add("game_day", byte.Parse(gameDay));
                    gameLog.Add("game_year", short.Parse(gameYear));
                }
            }

            return (ExpandoObject) gameLog;
        }
    }
}