using Microsoft.EntityFrameworkCore;
using Moq;
using Sunergizer_API.Database;
using Sunergizer_API.DTO;
using Sunergizer_API.Models;
using Sunergizer_API.Services;
using Xunit;

namespace Sunergizer_API.Tests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<DbSet<Usuario>> _mockSet;
        private readonly Mock<SunergizerDBContext> _mockContext;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _mockSet = new Mock<DbSet<Usuario>>();
            _mockContext = new Mock<SunergizerDBContext>(new DbContextOptions<SunergizerDBContext>());

            _mockContext.Setup(m => m.Usuarios).Returns(_mockSet.Object);
            _usuarioService = new UsuarioService(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllUsuariosAsync_ShouldReturnAllUsuarios()
        {
            // Arrange
            var data = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "João", Endereco = "Rua A", Email = "joao@email.com" },
                new Usuario { Id = 2, Nome = "Maria", Endereco = "Rua B", Email = "maria@email.com" }
            }.AsQueryable();

            _mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Act
            var result = await _usuarioService.GetAllUsuariosAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, u => u.Nome == "João");
        }

        [Fact]
        public async Task GetUsuarioByIdAsync_ShouldReturnUsuario_WhenFound()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "João", Endereco = "Rua A", Email = "joao@email.com" };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.GetUsuarioByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("João", result.Nome);
        }

        [Fact]
        public async Task AddUsuarioAsync_ShouldAddNewUsuario()
        {
            // Arrange
            var usuarioRequest = new UsuarioRequest
            {
                Nome = "Pedro",
                Endereco = "Rua C",
                Email = "pedro@email.com"
            };

            // Act
            var result = await _usuarioService.AddUsuarioAsync(usuarioRequest);

            // Assert
            _mockSet.Verify(m => m.Add(It.IsAny<Usuario>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateUsuarioAsync_ShouldUpdateUsuario_WhenFound()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nome = "Antigo", Endereco = "Rua Velha", Email = "antigo@email.com" };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(usuario);

            var usuarioRequest = new UsuarioRequest
            {
                Nome = "Novo",
                Endereco = "Rua Nova",
                Email = "novo@email.com"
            };

            // Act
            await _usuarioService.UpdateUsuarioAsync(1, usuarioRequest);

            // Assert
            Assert.Equal("Novo", usuario.Nome);
            Assert.Equal("Rua Nova", usuario.Endereco);
            Assert.Equal("novo@email.com", usuario.Email);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteUsuarioAsync_ShouldDeleteUsuario_WhenFound()
        {
            // Arrange
            var usuario = new Usuario { Id = 1 };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(usuario);

            // Act
            await _usuarioService.DeleteUsuarioAsync(1);

            // Assert
            _mockSet.Verify(m => m.Remove(It.IsAny<Usuario>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteUsuarioAsync_ShouldDoNothing_WhenNotFound()
        {
            // Arrange
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync((Usuario)null);

            // Act
            await _usuarioService.DeleteUsuarioAsync(1);

            // Assert
            _mockSet.Verify(m => m.Remove(It.IsAny<Usuario>()), Times.Never);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Never);
        }
    }
}
