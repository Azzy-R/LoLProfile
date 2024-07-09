namespace LoLProfile.Models
{
    public class Match
    {
        public string MatchId { get; set; }
        public string KDA { get; set; }
        public bool Win { get; set; }
        public long GameDuration { get; set; }
        public Dictionary<string, string> Players { get; set; } = [];


    }
}
