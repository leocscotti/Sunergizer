using Moq;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Sunergizer_API.Tests
{
    public class ConsumoServiceTests
    {
        private readonly Mock<SunergizerDBContext> _mockContext;
        private readonly Mock<DbSet<Consumo>> _mockSet;
        private readonly ConsumoService _consumoService;

        public ConsumoServiceTests()
        {
            _mockContext = new Mock<SunergizerDBContext>(new DbContextOptions<SunergizerDBContext>());
            _mockSet = new Mock<DbSet<Consumo>>();

            // Setup DbSet mock
            _mockContext.Setup(x => x.Consumos).Returns(_mockSet.Object);

            // Create the service
            _consumoService = new ConsumoService(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllConsumosAsync_ReturnsAllConsumos()
        {
            // Arrange
            var data = new List<Consumo>
            {
                new Consumo { Id = 1, IdUsuario = 1, IdFonte = 1, KwhConsumidos = 100 },
                new Consumo { Id = 2, IdUsuario = 2, IdFonte = 2, KwhConsumidos = 200 }
            }.AsQueryable();

            _mockSet.As<IQueryable<Consumo>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Consumo>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Consumo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Consumo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Act
            var result = await _consumoService.GetAllConsumosAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Id == 1 && c.KwhConsumidos == 100);
            Assert.Contains(result, c => c.Id == 2 && c.KwhConsumidos == 200);
        }

        [Fact]
        public async Task GetConsumoByIdAsync_ReturnsConsumo_WhenFound()
        {
            // Arrange
            var data = new Consumo { Id = 1, IdUsuario = 1, IdFonte = 1, KwhConsumidos = 100 };

            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(data);

            // Act
            var result = await _consumoService.GetConsumoByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(100, result.KwhConsumidos);
        }

        [Fact]
        public async Task AddConsumoAsync_AddsNewConsumo()
        {
            // Arrange
            var consumoRequest = new ConsumoRequest
            {
                IdUsuario = 1,
                IdFonte = 1,
                KwhConsumidos = 150
            };

            // Act
            await _consumoService.AddConsumoAsync(consumoRequest);

            // Assert
            _mockSet.Verify(m => m.AddAsync(It.IsAny<Consumo>(), default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteConsumoAsync_RemovesConsumo_WhenExists()
        {
            // Arrange
            var data = new Consumo { Id = 1 };

            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(data);
            _mockSet.Setup(m => m.Remove(It.IsAny<Consumo>()));

            // Act
            var result = await _consumoService.DeleteConsumoAsync(1);

            // Assert
            Assert.True(result);
            _mockSet.Verify(m => m.Remove(data), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteConsumoAsync_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            _mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((Consumo)null);

            // Act
            var result = await _consumoService.DeleteConsumoAsync(99);

            // Assert
            Assert.False(result);
        }
    }
}
