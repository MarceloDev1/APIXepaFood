using Domain.Entidades;
using Domain.Interfaces;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Domain.Requests;

namespace Infra.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly IConfiguration _configuration;
        public ProdutoRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int CriarProduto(ProdutoRequest produto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"INSERT INTO Produtos (NomeProduto, Preco, Descricao)
                 VALUES (@NomeProduto, @Preco, @Descricao);
                 SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);

                    var idGerado = command.ExecuteScalar();

                    return Convert.ToInt32(idGerado); // Converte para int e retorna
                }
            }
        }
        public Produto ObterProdutoPorId(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT p.*, e.Quantidade FROM Produtos p " +
                    "LEFT JOIN Estoque e ON p.IdProduto = e.IdProduto " +
                    "WHERE p.IdProduto = @IdProduto;";
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
                                return new Produto
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    NomeProduto = reader.GetString(reader.GetOrdinal("NomeProduto")),
                                    Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                    Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
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
                        throw new Exception("Erro ao obter usuário por id", ex);
                    }
                }
            }
        }

        public List<Produto> ObterProdutoIdLoja(int idLoja)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT p.*, e.Quantidade FROM Produtos p " +
                          "LEFT JOIN Estoque e ON p.IdProduto = e.IdProduto " +
                          "WHERE e.IdLoja = @IdLoja;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdLoja", idLoja);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var produtos = new List<Produto>();

                            while (reader.Read())
                            {
                                var produto = new Produto
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    NomeProduto = reader.GetString(reader.GetOrdinal("NomeProduto")),
                                    Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                    Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                    Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
                                };

                                produtos.Add(produto);
                            }
                            return produtos;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter usuário por id", ex);
                    }
                }
            }
        }

        public Produto ObterProdutoPorNome(string nome)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Produtos WHERE NomeProduto = @NomeProduto;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NomeProduto", nome);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Produto
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    NomeProduto = reader.GetString(reader.GetOrdinal("NomeProduto")),
                                    Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                    Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
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

        public List<Produto> ObterTodosProdutos()
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Produtos;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var produtos = new List<Produto>();

                            while (reader.Read()) // Leitura de múltiplos registros
                            {
                                var produto = new Produto
                                {
                                    IdProduto = reader.GetInt32(reader.GetOrdinal("IdProduto")),
                                    NomeProduto = reader.GetString(reader.GetOrdinal("NomeProduto")),
                                    Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                                    Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                };

                                produtos.Add(produto);
                            }

                            return produtos;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter todos os produtos", ex);
                    }
                }
            }
        }

        public void AtualizarProdutoPorId(Produto novoProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"UPDATE Produtos
                        SET NomeProduto = @NomeProduto, Preco = @Preco, Descricao = @Descricao
                        WHERE IdProduto = @IdProduto;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@NomeProduto", novoProduto.NomeProduto));
                        command.Parameters.Add(new SqlParameter("@Preco", novoProduto.Preco));
                        command.Parameters.Add(new SqlParameter("@Descricao", novoProduto.Descricao));
                        command.Parameters.Add(new SqlParameter("@IdProduto", novoProduto.IdProduto));

                        // Executa o comando SQL para atualizar o produto
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar o produto", ex);
                }
            }
        }

        public void DeletarProdutoPorId(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"DELETE FROM Produtos WHERE IdProduto = @IdProduto;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@IdProduto", idProduto));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao deletar o produto", ex);
                }
            }
        }        
    }
}