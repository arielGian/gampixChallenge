namespace gampix.api.Domain
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal OddsWin { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Bet> Bets { get; set; } = new();
    }
}
