namespace gampix.api.Domain
{
    public class Bet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public int Stake { get; set; }
        public decimal WinAmount { get; set; } = 0;
        public BetStatus Status { get; set; } = BetStatus.Pending;
        public string? Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User? User { get; set; }
    }

    public enum BetStatus
    {
        Pending,
        Won,
        Lost,
        Cancelled
    }
}
