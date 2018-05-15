namespace Api.Services.DocumentDb
{
    public class GameResult
    {
        public short game_year { get; set; }

        public byte game_number { get; set; }

        public byte game_month { get; set; }

        public byte game_day { get; set; }

        public string home_team { get; set; }
        public string visiting_team { get; set; }

        public byte home_team_score { get; set; }

        public byte visiting_team_score { get; set; }
    }
}