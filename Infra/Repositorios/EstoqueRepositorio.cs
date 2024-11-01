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
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
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

        public void DeletarEstoquePorIdProduto(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
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

        public List<InformacoesLojaProduto> ObterEstoque(string nomeProduto, string localizacao)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT p.NomeProduto, p.Preco, p.Descricao DescricaoProduto, l.NomeLoja, " +
                    "l.Localizacao LocalizacaoLoja, e.Quantidade " +
                    "FROM Produtos p " +
                    "LEFT JOIN Estoque e ON p.IdProduto = e.IdProduto " +
                    "LEFT JOIN Lojas l ON e.IdLoja = l.IdLoja " +
                    "WHERE 1=1 " +
                    "AND e.Quantidade > 0 ";

                if (!string.IsNullOrEmpty(nomeProduto))
                {
                    sql += " AND p.NomeProduto = @NomeProduto";
                }

                if (!string.IsNullOrEmpty(localizacao))
                {
                    sql += " AND l.Localizacao = @Localizacao";
                }

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (!string.IsNullOrEmpty(nomeProduto))
                    {
                        command.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                    }

                    if (!string.IsNullOrEmpty(localizacao))
                    {
                        command.Parameters.AddWithValue("@Localizacao", localizacao);
                    }

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var listaInformacoes = new List<InformacoesLojaProduto>();
                            while (reader.Read())
                            {
                                var item = new InformacoesLojaProduto
                                {
                                    Quantidade = reader.IsDBNull(reader.GetOrdinal("Quantidade")) ? 0 : reader.GetInt32(reader.GetOrdinal("Quantidade")),
                                    NomeProduto = reader.IsDBNull(reader.GetOrdinal("NomeProduto")) ? string.Empty : reader.GetString(reader.GetOrdinal("NomeProduto")),
                                    DescricaoProduto = reader.IsDBNull(reader.GetOrdinal("DescricaoProduto")) ? string.Empty : reader.GetString(reader.GetOrdinal("DescricaoProduto")),
                                    Preco = reader.IsDBNull(reader.GetOrdinal("Preco")) ? 0m : reader.GetDecimal(reader.GetOrdinal("Preco")),
                                    NomeLoja = reader.IsDBNull(reader.GetOrdinal("NomeLoja")) ? string.Empty : reader.GetString(reader.GetOrdinal("NomeLoja")),
                                    LocalizacaoLoja = reader.IsDBNull(reader.GetOrdinal("LocalizacaoLoja")) ? string.Empty : reader.GetString(reader.GetOrdinal("LocalizacaoLoja"))
                                };
                                listaInformacoes.Add(item);
                            }
                            return listaInformacoes;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter quantidade de estoque por idLoja.", ex);
                    }
                }
            }
        }
    }
}