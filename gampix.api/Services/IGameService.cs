using gampix.api.Domain;

namespace gampix.api.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllGamesAsync();
        Task<Game?> GetGameByIdAsync(int id);
        Task<Game> CreateGameAsync(CreateGameRequest request);
        Task<Game> UpdateGameAsync(int id, UpdateGameRequest request);
        Task<bool> DeleteGameAsync(int id);
    }

    public class CreateGameRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal OddsWin { get; set; }
    }

    public class UpdateGameRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? OddsWin { get; set; }
        public bool? IsActive { get; set; }
    }
}
