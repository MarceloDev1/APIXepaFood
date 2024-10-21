using Domain.Entidades;
using Domain.Interfaces;
using Domain.Servicos;
using Moq;

namespace Test
{
    public class UsuarioServicoTests
    {
        private readonly Mock<IUsuarioRepositorio> _mockUsuarioRepositorio;
        private readonly UsuarioServico _usuarioServico;

        public UsuarioServicoTests()
        {
            // Inicializa o mock do repositório
            _mockUsuarioRepositorio = new Mock<IUsuarioRepositorio>();

            // Instancia o serviço usando o mock
            _usuarioServico = new UsuarioServico(_mockUsuarioRepositorio.Object);
        }

        [Fact]
        public void CriarUsuario_DeveChamarRepositorio()
        {
            // Arrange
            var usuario = new Usuario { IdUsuario = 1, Nome = "Teste", Email = "teste@teste.com" };

            // Act
            _usuarioServico.CriarUsuario(usuario);

            // Assert
            _mockUsuarioRepositorio.Verify(repo => repo.CriarUsuario(usuario), Times.Once);
        }

        [Fact]
        public void ObterUsuarioPorEmail_DeveRetornarUsuario()
        {
            // Arrange
            var email = "teste@teste.com";
            var usuarioEsperado = new Usuario { IdUsuario = 1, Nome = "Teste", Email = email };

            _mockUsuarioRepositorio.Setup(repo => repo.ObterUsuarioPorEmail(email)).Returns(usuarioEsperado);

            // Act
            var usuarioObtido = _usuarioServico.ObterUsuarioPorEmail(email);

            // Assert
            Assert.Equal(usuarioEsperado, usuarioObtido);
        }

        [Fact]
        public void ObterTodosUsuarios_DeveRetornarListaDeUsuarios()
        {
            // Arrange
            var listaUsuariosEsperada = new List<Usuario>
            {
                new Usuario { IdUsuario = 1, Nome = "Teste 1", Email = "teste1@teste.com" },
                new Usuario { IdUsuario = 2, Nome = "Teste 2", Email = "teste2@teste.com" }
            };

            _mockUsuarioRepositorio.Setup(repo => repo.ObterTodosUsuarios()).Returns(listaUsuariosEsperada);

            // Act
            var listaUsuariosObtida = _usuarioServico.ObterTodosUsuarios();

            // Assert
            Assert.Equal(listaUsuariosEsperada, listaUsuariosObtida);
        }

        [Fact]
        public void ObterUsuarioPorId_DeveRetornarUsuario()
        {
            // Arrange
            var idUsuario = 1;
            var usuarioEsperado = new Usuario { IdUsuario = idUsuario, Nome = "Teste", Email = "teste@teste.com" };

            _mockUsuarioRepositorio.Setup(repo => repo.ObterUsuarioPorId(idUsuario)).Returns(usuarioEsperado);

            // Act
            var usuarioObtido = _usuarioServico.ObterUsuarioPorId(idUsuario);

            // Assert
            Assert.Equal(usuarioEsperado, usuarioObtido);
        }

        [Fact]
        public void AtualizarUsuarioPorId_DeveChamarRepositorio()
        {
            // Arrange
            var usuarioAtualizado = new Usuario { IdUsuario = 1, Nome = "Teste Atualizado", Email = "teste@atualizado.com" };

            // Act
            _usuarioServico.AtualizarUsuarioPorId(usuarioAtualizado);

            // Assert
            _mockUsuarioRepositorio.Verify(repo => repo.AtualizarUsuarioPorId(usuarioAtualizado), Times.Once);
        }

        [Fact]
        public void DeletarUsuarioPorId_DeveChamarRepositorio()
        {
            // Arrange
            var idUsuario = 1;

            // Act
            _usuarioServico.DeletarUsuarioPorId(idUsuario);

            // Assert
            _mockUsuarioRepositorio.Verify(repo => repo.DeletarUsuarioPorId(idUsuario), Times.Once);
        }

        [Fact]
        public void ObterUsuarioPorEmailSenha_DeveRetornarUsuario()
        {
            // Arrange
            var email = "teste@teste.com";
            var senha = "senha123";
            var usuarioEsperado = new Usuario { IdUsuario = 1, Nome = "Teste", Email = email, Senha = senha };

            _mockUsuarioRepositorio.Setup(repo => repo.ObterUsuarioPorEmailSenha(email, senha)).Returns(usuarioEsperado);

            // Act
            var usuarioObtido = _usuarioServico.ObterUsuarioPorEmailSenha(email, senha);

            // Assert
            Assert.Equal(usuarioEsperado, usuarioObtido);
        }
    }
}
