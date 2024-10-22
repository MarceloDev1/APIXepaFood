using Domain.Interfaces;
using Domain.Entidades;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infra.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IConfiguration _configuration;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CriarUsuario(Usuario usuario)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"INSERT INTO Usuarios (Nome, Email, Senha, Localizacao, Telefone)
                        VALUES (@Nome, @Email, @Senha, @Localizacao, @Telefone);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", usuario.Nome);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Senha", usuario.Senha);
                    command.Parameters.AddWithValue("@Localizacao", usuario.Localizacao);
                    command.Parameters.AddWithValue("@Telefone", usuario.Telefone);

                    command.ExecuteNonQuery();

                }
            }
        }
        public int CriarUsuarioRetornaIdUsuario(Usuario usuario)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"INSERT INTO Usuarios (Nome, Email, Senha, Localizacao, Telefone)
                        VALUES (@Nome, @Email, @Senha, @Localizacao, @Telefone);
                        SELECT SCOPE_IDENTITY();";  // Retorna o ID gerado";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", usuario.Nome);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Senha", usuario.Senha);
                    command.Parameters.AddWithValue("@Localizacao", usuario.Localizacao);
                    command.Parameters.AddWithValue("@Telefone", usuario.Telefone);

                    var idGerado = command.ExecuteScalar();

                    return Convert.ToInt32(idGerado); // Converte para int e retorna

                }
            }
        }


        public Usuario ObterUsuarioPorEmail(string email)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Usuarios WHERE Email = @Email;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usuario
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Senha = reader.GetString(reader.GetOrdinal("Senha")),
                                    Localizacao = reader.GetString(reader.GetOrdinal("Localizacao")),
                                    Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter usuário por email", ex);
                    }
                }
            }
        }

        public List<Usuario> ObterTodosUsuarios()
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Usuarios;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var usuarios = new List<Usuario>();

                            while (reader.Read()) // Leitura de múltiplos registros
                            {
                                var usuario = new Usuario
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Senha = reader.GetString(reader.GetOrdinal("Senha")),
                                    Localizacao = reader.GetString(reader.GetOrdinal("Localizacao")),
                                    Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                                };

                                usuarios.Add(usuario);
                            }

                            return usuarios;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter todos os usuários", ex);
                    }
                }
            }
        }

        public Usuario ObterUsuarioPorId(int idUsuario)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Usuarios WHERE IdUsuario = @IdUsuario;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usuario
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Senha = reader.GetString(reader.GetOrdinal("Senha")),
                                    Localizacao = reader.GetString(reader.GetOrdinal("Localizacao")),
                                    Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter usuário por email", ex);
                    }
                }
            }
        }

        public void AtualizarUsuarioPorId(Usuario usuario)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"UPDATE Usuarios
                        SET Nome = @Nome, Email = @Email, Senha = @Senha, Localizacao = @Localizacao, Telefone = @Telefone
                        WHERE IdUsuario = @IdUsuario;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Nome", usuario.Nome));
                        command.Parameters.Add(new SqlParameter("@Email", usuario.Email));
                        command.Parameters.Add(new SqlParameter("@Senha", usuario.Senha));
                        command.Parameters.Add(new SqlParameter("@Localizacao", usuario.Localizacao));
                        command.Parameters.Add(new SqlParameter("@Telefone", usuario.Telefone));
                        command.Parameters.Add(new SqlParameter("@IdUsuario", usuario.IdUsuario));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar o usuário", ex);
                }
            }
        }


        public void DeletarUsuarioPorId(int idUsuario)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@IdUsuario", idUsuario));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao deletar o usuário", ex);
                }
            }
        }

        public Usuario ObterUsuarioPorEmailSenha(string email, string senha)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Usuarios WHERE Email = @Email AND Senha = @Senha;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Senha", senha);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usuario
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Senha = reader.GetString(reader.GetOrdinal("Senha")),
                                    Localizacao = reader.GetString(reader.GetOrdinal("Localizacao")),
                                    Telefone = reader.GetString(reader.GetOrdinal("Telefone"))
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter usuário!", ex);
                    }
                }
            }
        }
    }
}