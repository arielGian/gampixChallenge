using gampix.api.Domain;

namespace gampix.api.Services
{
    public class GameService : IGameService
    {
        // TODO: Implement with database context when ready
        private static List<Game> _games = new();
        private static int _nextId = 1;

        public async Task<List<Game>> GetAllGamesAsync()
        {
            await Task.Delay(0);
            return _games;
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            await Task.Delay(0);
            return _games.FirstOrDefault(g => g.Id == id);
        }

        public async Task<Game> CreateGameAsync(CreateGameRequest request)
        {
            await Task.Delay(0);
            var game = new Game
            {
                Id = _nextId++,
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                OddsWin = request.OddsWin
            };
            _games.Add(game);
            return game;
        }

        public async Task<Game> UpdateGameAsync(int id, UpdateGameRequest request)
        {
            await Task.Delay(0);
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game == null)
                throw new KeyNotFoundException($"Game with id {id} not found");

            if (!string.IsNullOrEmpty(request.Name))
                game.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Description))
                game.Description = request.Description;
            if (request.EndDate.HasValue)
                game.EndDate = request.EndDate.Value;
            if (request.OddsWin.HasValue)
                game.OddsWin = request.OddsWin.Value;
            if (request.IsActive.HasValue)
                game.IsActive = request.IsActive.Value;

            return game;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            await Task.Delay(0);
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game == null)
                return false;

            _games.Remove(game);
            return true;
        }
    }
}
