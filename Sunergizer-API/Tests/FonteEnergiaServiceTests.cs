using Moq;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Sunergizer_API.Tests
{
    public class FonteEnergiaServiceTests
    {
        private readonly Mock<SunergizerDBContext> _mockContext;
        private readonly Mock<DbSet<FonteEnergia>> _mockSet;
        private readonly FonteEnergiaService _fonteEnergiaService;

        public FonteEnergiaServiceTests()
        {
            _mockContext = new Mock<SunergizerDBContext>(new DbContextOptions<SunergizerDBContext>());
            _mockSet = new Mock<DbSet<FonteEnergia>>();

            _mockContext.Setup(x => x.FontesEnergia).Returns(_mockSet.Object);

            _fonteEnergiaService = new FonteEnergiaService(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllFontesEnergiaAsync_ReturnsAllFontes()
        {
            var data = new List<FonteEnergia>
            {
                new FonteEnergia { Id = 1, Tipo = "Solar", Descricao = "conjunto de panéis solares "},
                new FonteEnergia { Id = 2, Tipo = "Eólica", Descricao = "conjunto de turbinas eólicas" }
            }.AsQueryable();

            _mockSet.As<IQueryable<FonteEnergia>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<FonteEnergia>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<FonteEnergia>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<FonteEnergia>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var result = await _fonteEnergiaService.GetAllFontesEnergiaAsync();

            Assert.Equal(2, result.Count());
            Assert.Contains(result, f => f.Tipo == "Solar");
        }

        [Fact]
        public async Task GetFonteEnergiaByIdAsync_ReturnsFonteEnergia_WhenFound()
        {
            var data = new FonteEnergia { Id = 1, Tipo = "Solar", Descricao = "conjunto de panéis solares " };

            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(data);

            var result = await _fonteEnergiaService.GetFonteEnergiaByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Solar", result.Tipo);
        }

        [Fact]
        public async Task AddFonteEnergiaAsync_AddsNewFonte()
        {
            var fonteRequest = new FonteEnergiaRequest
            {
                Tipo = "Hidrelétrica",
                Descricao = "usina hidrelétrica"
            };

            await _fonteEnergiaService.AddFonteEnergiaAsync(fonteRequest);

            _mockSet.Verify(m => m.AddAsync(It.IsAny<FonteEnergia>(), default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteFonteEnergiaAsync_RemovesFonte_WhenExists()
        {
            var data = new FonteEnergia { Id = 1 };

            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(data);
            _mockSet.Setup(m => m.Remove(It.IsAny<FonteEnergia>()));

            var result = await _fonteEnergiaService.DeleteFonteEnergiaAsync(1);

            Assert.True(result);
            _mockSet.Verify(m => m.Remove(data), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
