using gampix.api.Domain;

namespace gampix.api.Services
{
    public class BetService : IBetService
    {
        private static List<Bet> _bets = new();
        private static int _nextId = 1;

        public async Task<List<Bet>> GetAllBetsAsync()
        {
            await Task.Delay(0);
            return _bets;
        }

        public async Task<Bet?> GetBetByIdAsync(int id)
        {
            await Task.Delay(0);
            return _bets.FirstOrDefault(b => b.Id == id);
        }

        public async Task<List<Bet>> GetBetsByUserIdAsync(int userId)
        {
            await Task.Delay(0);
            return _bets.Where(b => b.UserId == userId).ToList();
        }

        public async Task<List<Bet>> GetBetsByGameNameAsync(string gameName)
        {
            await Task.Delay(0);
            return _bets.Where(b => b.GameName.Equals(gameName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<Bet> CreateBetAsync(CreateBetRequest request)
        {
            await Task.Delay(0);

            if (request.Stake <= 0)
                throw new ArgumentException("Stake must be greater than 0");

            var bet = new Bet
            {
                Id = _nextId++,
                UserId = request.UserId,
                GameName = request.Game,
                Stake = request.Stake,
                WinAmount = request.WinAmount,
                Status = BetStatus.Pending
            };

            _bets.Add(bet);
            return bet;
        }

        public async Task<Bet> UpdateBetStatusAsync(int id, BetStatus status, string? result)
        {
            await Task.Delay(0);
            var bet = _bets.FirstOrDefault(b => b.Id == id);
            if (bet == null)
                throw new KeyNotFoundException($"Bet with id {id} not found");

            bet.Status = status;
            bet.Result = result;

            if (status == BetStatus.Won)
                bet.WinAmount = bet.Stake * 2;
            else if (status == BetStatus.Lost)
                bet.WinAmount = 0;

            return bet;
        }

        public async Task<decimal> GetRTPAsync()
        {
            await Task.Delay(0);

            var totalStake = _bets.Sum(b => b.Stake);
            if (totalStake == 0)
                return 0;

            var totalWin = _bets.Sum(b => b.WinAmount);
            return (totalWin / totalStake) * 100;
        }
    }
}
