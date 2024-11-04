using Domain.Entidades;
using Domain.Interfaces;
using Domain.Requests;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class LojaRepositorio : ILojaRepositorio
    {
        private readonly IConfiguration _configuration;
        public LojaRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CriarLoja(LojaRequest loja)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                connection.Open();

                var query = @"INSERT INTO Lojas (NomeLoja, Localizacao, IdUsuario)
                        VALUES (@NomeLoja, @Localizacao, @IdUsuario);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NomeLoja", loja.NomeLoja);
                    command.Parameters.AddWithValue("@CEP", loja.CEP);
                    command.Parameters.AddWithValue("@Logradouro", loja.Logradouro);
                    command.Parameters.AddWithValue("@Bairro", loja.Bairro);
                    command.Parameters.AddWithValue("@UF", loja.UF);
                    command.Parameters.AddWithValue("@Cidade", loja.Cidade);
                    command.Parameters.AddWithValue("@IdUsuario", loja.IdUsuario);

                    command.ExecuteNonQuery();

                }
            }
        }
        public List<Loja> ObterTodasLojas()
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Lojas;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var lojas = new List<Loja>();

                            while (reader.Read()) 
                            {
                                var loja = new Loja
                                {
                                    IdLoja = reader.GetInt32(reader.GetOrdinal("IdLoja")),
                                    NomeLoja = reader.GetString(reader.GetOrdinal("NomeLoja")),
                                    Localizacao = reader.GetString(reader.GetOrdinal("Localizacao")),
                                };

                                lojas.Add(loja);
                            }

                            return lojas;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter todas as lojas.", ex);
                    }
                }
            }
        }
        public Loja ObterLojaPorId(int idLoja)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Lojas WHERE IdLoja = @IdLoja;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdLoja", idLoja);
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Loja
                                {
                                    IdLoja = reader.GetInt32(reader.GetOrdinal("IdLoja")),
                                    NomeLoja = reader.GetString(reader.GetOrdinal("NomeLoja")),
                                    Localizacao = reader.GetString(reader.GetOrdinal("Localizacao")),
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario"))
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
                        throw new Exception("Erro ao obter a loja", ex);
                    }
                }
            }
        }
        public List<Loja> ObterLojaPorIdUsuario(int idUsuario)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                var sql = "SELECT * FROM Lojas WHERE IdUsuario = @IdUsuario;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var lojas = new List<Loja>();

                            while (reader.Read())
                            {
                                var loja = new Loja
                                {
                                    IdLoja = reader.GetInt32(reader.GetOrdinal("IdLoja")),
                                    NomeLoja = reader.GetString(reader.GetOrdinal("NomeLoja")),
                                    Localizacao = reader.GetString(reader.GetOrdinal("Localizacao")),
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario"))
                                };

                                lojas.Add(loja);
                            }

                            return lojas;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao obter a loja", ex);
                    }
                }
            }
        }
        public void AtualizarLoja(Loja novaLoja)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"UPDATE Lojas
                        SET NomeLoja = @NomeLoja, Localizacao = @Localizacao
                        WHERE IdLoja = @IdLoja;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@NomeLoja", novaLoja.NomeLoja));
                        command.Parameters.Add(new SqlParameter("@Localizacao", novaLoja.Localizacao));
                        command.Parameters.Add(new SqlParameter("@IdLoja", novaLoja.IdLoja));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao atualizar a loja!", ex);
                }
            }
        }
        public void DeletarLoja(int idLoja)
        {
            var stringConexao = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection connection = new SqlConnection(stringConexao))
            {
                try
                {
                    connection.Open();

                    var sql = @"DELETE FROM Lojas WHERE IdLoja = @IdLoja;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@IdLoja", idLoja));

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao deletar a loja", ex);
                }
            }
        }        
    }
}