namespace TennisPlayers.Models
{
    public class players
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string shortname { get; set; }
        public string sex { get; set; }
        public country country { get; set; }
        public string picture { get; set; }
        public playerData data { get; set; }
    }
}
