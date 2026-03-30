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
    }

    public class CreateBetRequest
    {
        public int UserId { get; set; }
        public string Game { get; set; } = string.Empty;
        public int Stake { get; set; }
        public decimal WinAmount { get; set; }
    }
}
