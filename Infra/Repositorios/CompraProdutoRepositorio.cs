using APIXepaFood.Controllers;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infra.Repositorios
{
    public class CompraProdutoRepositorio : ICompraProdutoRepositorio
    {
        private readonly IConfiguration _configuration;
        public CompraProdutoRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public int ComprarProduto(int idUsuario, DateTime dataCompra, decimal valorTotalCompra, int idStatusCompra)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            int idCompra;

            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"INSERT INTO Compra (IdUsuario, DataCompra, ValorTotal, IdStatusCompra)
                            OUTPUT INSERTED.IdCompra
                            VALUES (@IdUsuario, @DataCompra, @ValorTotal, @IdStatusCompra);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@DataCompra", dataCompra);
                    command.Parameters.AddWithValue("@ValorTotal", valorTotalCompra);
                    command.Parameters.AddWithValue("@IdStatusCompra", idStatusCompra);

                    // Executa a consulta e captura o valor de IdCompra gerado
                    idCompra = (int)command.ExecuteScalar();
                }
            }
            return idCompra;
        }


        public void RegistrarItemCompra(int idCompra, int idProduto, int quantidade, decimal precoUnitario)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"INSERT INTO ItemCompra (IdCompra, IdProduto, Quantidade, PrecoUnitario)
                            VALUES (@IdCompra, @IdProduto, @Quantidade, @PrecoUnitario);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdCompra", idCompra);
                    command.Parameters.AddWithValue("@IdProduto", idProduto);
                    command.Parameters.AddWithValue("@Quantidade", quantidade);
                    command.Parameters.AddWithValue("@PrecoUnitario", precoUnitario);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarEstoque(int idProduto, int quantidadeComprada)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var sql = @"UPDATE Estoque 
                    SET Quantidade = Quantidade - @QuantidadeComprada
                    WHERE IdProduto = @IdProduto;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdProduto", idProduto);
                    command.Parameters.AddWithValue("@QuantidadeComprada", quantidadeComprada);

                    command.ExecuteNonQuery();
                }
            }
        }
        public int RetornarQtdProdutoEstoque(int idProduto)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT Quantidade FROM Estoque WHERE IdProduto = @IdProduto;";

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
                                return reader.GetInt32(reader.GetOrdinal("Quantidade"));
                            }
                            return 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter quantidade de estoque por IdProduto.", ex);
                    }
                }
            }
        }
    }
}