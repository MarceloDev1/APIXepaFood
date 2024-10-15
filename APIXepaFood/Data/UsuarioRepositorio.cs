using APIXepaFood.Interfaces;
using APIXepaFood.Models;
using Dapper;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace APIXepaFood.Data
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioRepositorio(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void CriarUsuario(Usuario usuario)
        {
            var sql = @"INSERT INTO Clientes (Nome, Email, Senha, Localizacao, Telefone)
                        VALUES (@Nome, @Email, @Senha, @Localizacao, @Telefone);";

            _dbConnection.Execute(sql, new
            {
                usuario.Nome,
                usuario.Email,
                usuario.Senha,
                usuario.Localizacao,
                usuario.Telefone
            });
        }

        public Usuario ObterUsuarioPorEmail(string email)
        {
            var sql = "SELECT * FROM Clientes WHERE Email = @Email;";
            try
            {
                using (var connection = _dbConnection)
                {
                    connection.Open();
                    return connection.Query<Usuario>(sql, new { Email = email }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("Erro ao obter usuário por email", ex);
            }
        }

        public List<Usuario> ObterTodosUsuarios()
        {
            var sql = "SELECT * FROM Clientes;";
            return _dbConnection.Query<Usuario>(sql).ToList();
        }

        public Usuario ObterUsuarioPorId(int idUsuario)
        {
            var sql = "SELECT * FROM Clientes WHERE IdCliente = @IdCliente;";
            try
            {
                using (var connection = _dbConnection)
                {
                    connection.Open();
                    return connection.Query<Usuario>(sql, new { IdCliente = idUsuario }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter usuário por Id", ex);
            }
        }

        public void AtualizarUsuarioPorId(Usuario usuario)
        {
            var sql = @"UPDATE Clientes
                SET Nome = @Nome, Email = @Email, Senha = @Senha, Localizacao = @Localizacao, Telefone = @Telefone
                WHERE Id = @Id;";

            _dbConnection.Execute(sql, new
            {
                usuario.Nome,
                usuario.Email,
                usuario.Senha,
                usuario.Localizacao,
                usuario.Telefone,
                usuario.IdUsuario
            });
        }

        public void DeletarUsuarioPorId(int idUsuario)
        {
            var sql = @"DELETE FROM Clientes WHERE IdCliente = @IdCliente;";

            _dbConnection.Execute(sql, new { IdUsuario = idUsuario });
        }

    }
}