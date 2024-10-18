using Domain.Entidades;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;

namespace Infra.Repositorios
{
    public class EstoqueRepositorio : IEstoqueRepositorio
    {
        private readonly IConfiguration _configuration;
        public EstoqueRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InserirEstoque(Estoque estoque)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringMarcelo");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"INSERT INTO Estoque (IdProduto, IdLoja, Quantidade)
                        VALUES (@IdProduto, @IdLoja, @Quantidade);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdProduto", estoque.IdProduto);
                    command.Parameters.AddWithValue("@IdLoja", estoque.IdLoja);
                    command.Parameters.AddWithValue("@Quantidade", estoque.Quantidade);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Estoque> ObterProdutoPorIdProduto(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringMarcelo");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Estoque WHERE IdProduto = @IdProduto;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdProduto", idProduto);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var listaEstoque = new List<Estoque>();

                            while (reader.Read())
                            {
                                var estoque = new Estoque
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    IdLoja = reader.GetInt32(reader.GetOrdinal("IdLoja")),
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
                                };

                                listaEstoque.Add(estoque);
                            }
                            return listaEstoque;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter quantidade de estoque por idProduto.", ex);
                    }
                }
            }
        }

        public List<Estoque> ObterProdutoPorIdLoja(int idLoja)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringMarcelo");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Estoque WHERE IdLoja = @IdLoja;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdLoja", idLoja);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var listaEstoque = new List<Estoque>();

                            while (reader.Read())
                            {
                                var estoque = new Estoque
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    IdLoja = reader.GetInt32(reader.GetOrdinal("IdLoja")),
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
                                };

                                listaEstoque.Add(estoque);
                            }
                            return listaEstoque;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter quantidade de estoque por idLoja.", ex);
                    }
                }
            }
        }

        public Estoque ObterEstoquePorIdLojaEIdProduto(int idProduto, int idLoja)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringMarcelo");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Estoque WHERE IdLoja = @IdLoja AND IdProduto = @IdProduto;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdLoja", idLoja);
                    command.Parameters.AddWithValue("@IdProduto", idProduto);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Estoque
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    IdLoja = reader.GetInt32(reader.GetOrdinal("IdLoja")),
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
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
                        throw new Exception("Erro ao obter quantidade de estoque por idLoja.", ex);
                    }
                }
            }
        }
        public Estoque ObterEstoqueIdProduto(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringMarcelo");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Estoque WHERE IdProduto = @IdProduto;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdProduto", idProduto);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Estoque
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    IdLoja = reader.GetInt32(reader.GetOrdinal("IdLoja")),
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
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
                        throw new Exception("Erro ao obter estoque por idProduto", ex);
                    }
                }
            }
        }

        public void AtualizarEstoquePorId(Estoque estoque)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringMarcelo");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"UPDATE Estoque
                        SET IdProduto = @IdProduto, IdLoja = @IdLoja, Quantidade = @Quantidade
                        WHERE IdProduto = @IdProduto AND IdLoja = @IdLoja;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@IdProduto", estoque.IdProduto));
                        command.Parameters.Add(new SqlParameter("@IdLoja", estoque.IdLoja));
                        command.Parameters.Add(new SqlParameter("@Quantidade", estoque.Quantidade));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar o estoque", ex);
                }
            }
        }

        public void DeletarEstoquePorId(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringMarcelo");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"DELETE FROM Estoque WHERE IdProduto = @IdProduto;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@IdProduto", idProduto));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao deletar o estoque", ex);
                }
            }
        }        
    }
}