using gampix.api.Domain;

namespace gampix.api.Services
{
    public interface IBetService
    {
        Task<List<Bet>> GetAllBetsAsync();
        Task<Bet?> GetBetByIdAsync(int id);
        Task<List<Bet>> GetBetsByUserIdAsync(int userId);
        Task<List<Bet>> GetBetsByGameNameAsync(string gameName);
        Task<Bet> CreateBetAsync(CreateBetRequest request);
        Task<Bet> UpdateBetStatusAsync(int id, BetStatus status, string? result);
        Task<decimal> GetRTPAsync();
        Task<StatsResponse> GetStatsAsync();
    }

    public class CreateBetRequest
    {
        public int UserId { get; set; }
        public string Game { get; set; } = string.Empty;
        public int Stake { get; set; }
        public decimal WinAmount { get; set; }
    }
    

    public class StatsResponse
    {
        public List<GameRtp> Games { get; set; } = new();
        public List<UserStats> Users { get; set; } = new();
    }
    public class GameRtp { public string Game { get; set; } = ""; public decimal Rtp { get; set; } }
    public class UserStats { public int UserId { get; set; } public decimal TotalStake { get; set; } public decimal TotalWin { get; set; } }
}
