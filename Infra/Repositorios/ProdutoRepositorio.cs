using Domain.Entidades;
using Domain.Interfaces;
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
            throw new NotImplementedException();
        }

        public Produto ObterProdutoPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public List<Produto> ObterTodosProdutos()
        {
            throw new NotImplementedException();
        }
        
        public void AtualizarProdutoPorId(Produto novoProduto)
        {
            throw new NotImplementedException();
        }

        public void DeletarProdutoPorId(int idProduto)
        {
            throw new NotImplementedException();
        }

    }
}