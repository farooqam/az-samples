﻿namespace Api.DomainModel
{
    public class GameResult
    {
        public byte GameNumber { get; set; }

        public byte GameMonth { get; set; }

        public byte GameDay { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public byte HomeTeamScore { get; set; }

        public byte AwayTeamScore { get; set; }

        public byte RunDifferential => (byte)(HomeTeamScore - AwayTeamScore);
    }
}