using Moq;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Sunergizer_API.Tests
{
    public class ComunidadeServiceTests
    {
        private readonly Mock<SunergizerDBContext> _mockContext;
        private readonly Mock<DbSet<Comunidade>> _mockSet;
        private readonly ComunidadeService _comunidadeService;

        public ComunidadeServiceTests()
        {
            _mockContext = new Mock<SunergizerDBContext>(new DbContextOptions<SunergizerDBContext>());
            _mockSet = new Mock<DbSet<Comunidade>>();

            _mockContext.Setup(x => x.Comunidades).Returns(_mockSet.Object);

            _comunidadeService = new ComunidadeService(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllComunidadesAsync_ReturnsAllComunidades()
        {
            var data = new List<Comunidade>
            {
                new Comunidade { Id = 1, Nome = "Comunidade A", Cidade = "Cidade A", Uf = "SP" },
                new Comunidade { Id = 2, Nome = "Comunidade B", Cidade = "Cidade B", Uf = "RJ" }
            }.AsQueryable();

            _mockSet.As<IQueryable<Comunidade>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Comunidade>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Comunidade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Comunidade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var result = await _comunidadeService.GetAllComunidadesAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Nome == "Comunidade A");
        }

        [Fact]
        public async Task GetComunidadeByIdAsync_ReturnsComunidade_WhenFound()
        {
            var data = new Comunidade { Id = 1, Nome = "Comunidade A", Cidade = "Cidade A", Uf = "SP" };

            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(data);

            var result = await _comunidadeService.GetComunidadeByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Comunidade A", result.Nome);
        }

        [Fact]
        public async Task AddComunidadeAsync_AddsNewComunidade()
        {
            var comunidadeRequest = new ComunidadeRequest
            {
                Nome = "Comunidade C",
                Cidade = "Cidade C",
                Uf = "MG"
            };

            await _comunidadeService.AddComunidadeAsync(comunidadeRequest);

            _mockSet.Verify(m => m.AddAsync(It.IsAny<Comunidade>(), default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteComunidadeAsync_RemovesComunidade_WhenExists()
        {
            var data = new Comunidade { Id = 1 };

            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(data);
            _mockSet.Setup(m => m.Remove(It.IsAny<Comunidade>()));

            var result = await _comunidadeService.DeleteComunidadeAsync(1);

            Assert.True(result);
            _mockSet.Verify(m => m.Remove(data), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
