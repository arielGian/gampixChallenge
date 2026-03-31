using System.Reflection;
using gampix.api.Domain;
using gampix.api.Services;
using Xunit;

namespace gampix.api.Tests.Services
{
    public class BetServiceTests : IDisposable
    {
        private readonly BetService _service;

        public BetServiceTests()
        {
            ResetStaticState();
            _service = new BetService();
        }

        public void Dispose() => ResetStaticState();

        private static void ResetStaticState()
        {
            var type = typeof(BetService);
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            type.GetField("_bets", flags)!.SetValue(null, new List<Bet>());
            type.GetField("_nextId", flags)!.SetValue(null, 1);
        }

        // --- Validación de Stake ---

        [Fact]
        public async Task CreateBetAsync_StakeCero_LanzaArgumentException()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 0, WinAmount = 0 };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateBetAsync(request));
        }

        [Fact]
        public async Task CreateBetAsync_StakeNegativo_LanzaArgumentException()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = -50, WinAmount = 0 };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateBetAsync(request));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task CreateBetAsync_StakeInvalido_LanzaArgumentException(int stake)
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = stake, WinAmount = 0 };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateBetAsync(request));
        }

        // --- Propiedades del bet creado ---

        [Fact]
        public async Task CreateBetAsync_RequestValido_RetornaApuestaConUserId()
        {
            var request = new CreateBetRequest { UserId = 42, Game = "Partido A", Stake = 100, WinAmount = 150 };

            var bet = await _service.CreateBetAsync(request);

            Assert.Equal(42, bet.UserId);
        }

        [Fact]
        public async Task CreateBetAsync_RequestValido_RetornaApuestaConNombreJuego()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Barcelona vs Real Madrid", Stake = 100, WinAmount = 150 };

            var bet = await _service.CreateBetAsync(request);

            Assert.Equal("Barcelona vs Real Madrid", bet.GameName);
        }

        [Fact]
        public async Task CreateBetAsync_RequestValido_RetornaApuestaConStake()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 200, WinAmount = 0 };

            var bet = await _service.CreateBetAsync(request);

            Assert.Equal(200, bet.Stake);
        }

        [Fact]
        public async Task CreateBetAsync_RequestValido_RetornaApuestaConWinAmount()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 100, WinAmount = 175.50m };

            var bet = await _service.CreateBetAsync(request);

            Assert.Equal(175.50m, bet.WinAmount);
        }

        [Fact]
        public async Task CreateBetAsync_RequestValido_EstadoEsPending()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 100, WinAmount = 0 };

            var bet = await _service.CreateBetAsync(request);

            Assert.Equal(BetStatus.Pending, bet.Status);
        }

        [Fact]
        public async Task CreateBetAsync_RequestValido_CreatedAtAsignado()
        {
            var antes = DateTime.UtcNow.AddSeconds(-1);
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 100, WinAmount = 0 };

            var bet = await _service.CreateBetAsync(request);

            Assert.True(bet.CreatedAt >= antes && bet.CreatedAt <= DateTime.UtcNow.AddSeconds(1));
        }

        // --- ID auto-incremental ---

        [Fact]
        public async Task CreateBetAsync_PrimeraApuesta_IdEsUno()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 100, WinAmount = 0 };

            var bet = await _service.CreateBetAsync(request);

            Assert.Equal(1, bet.Id);
        }

        [Fact]
        public async Task CreateBetAsync_VariasApuestas_IdsIncrementales()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 100, WinAmount = 0 };

            var bet1 = await _service.CreateBetAsync(request);
            var bet2 = await _service.CreateBetAsync(request);
            var bet3 = await _service.CreateBetAsync(request);

            Assert.Equal(1, bet1.Id);
            Assert.Equal(2, bet2.Id);
            Assert.Equal(3, bet3.Id);
        }

        // --- Persistencia en memoria ---

        [Fact]
        public async Task CreateBetAsync_RequestValido_ApuestaGuardadaEnMemoria()
        {
            var request = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 100, WinAmount = 0 };

            var bet = await _service.CreateBetAsync(request);
            var all = await _service.GetAllBetsAsync();

            Assert.Contains(all, b => b.Id == bet.Id);
        }

        [Fact]
        public async Task CreateBetAsync_VariasApuestas_TodasGuardadasEnMemoria()
        {
            var r1 = new CreateBetRequest { UserId = 1, Game = "Partido A", Stake = 100, WinAmount = 0 };
            var r2 = new CreateBetRequest { UserId = 2, Game = "Partido B", Stake = 200, WinAmount = 0 };

            await _service.CreateBetAsync(r1);
            await _service.CreateBetAsync(r2);
            var all = await _service.GetAllBetsAsync();

            Assert.Equal(2, all.Count);
        }
    }
}
