﻿using Domain.Entidades;
using Domain.Interfaces;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infra.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly IConfiguration _configuration;
        public ProdutoRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void CriarProduto(Produto produto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringVinicius");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"INSERT INTO Produtos (NomeProduto, Preco, Descricao)
                 VALUES (@NomeProduto, @Preco, @Descricao);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);

                    command.ExecuteNonQuery();

                }
            }
        }
        public Produto ObterProdutoPorId(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringVinicius");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Produtos WHERE IdProduto = @IdProduto;";
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
                                    Descricao = reader.GetString(reader.GetOrdinal("Descricao"))
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

        public Produto ObterProdutoPorNome(string nome)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionStringVinicius");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionStringVinicius");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionStringVinicius");
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
            var stringConexao = _configuration.GetConnectionString("ConnectionStringVinicius");
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