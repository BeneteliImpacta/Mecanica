using MecanicaBeneteli.Business.Interfaces.Repository;
using MecanicaBeneteli.Business.Models;
using MecanicaBeneteli.Data.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Data.Repository
{
    public class ManutencaoRepository : BaseRepository, IManutencaoRepository
    {
        public ManutencaoRepository(ConnectionStringsDTO connectionStrings) : base(connectionStrings) { }
        public void BeginTran()
        {
            BeginTransaction(_connectionStrings.Mecanica);
        }

        public void CommitTran()
        {
            Commit();
        }

        public void RollbackTran()
        {
            Rollback();
        }

        public async Task<Peca> ConsultarPecaParaAlterarQuantidade(int id)
        {
            var peca = new Peca();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Id", Value = id },
            };

            var query = @$"SELECT [Id]
                                  ,[Nome]
                                  ,[Marca]
                                  ,[Modelo]
                                  ,[Ano]
                                  ,[Preco]
                                  ,[QuantidadeEstoque]
                              FROM [Mecanica].[dbo].[Pecas]
                              WHERE Id = @Id";


            DataTable dataTable = await ExecutarSelect(_connectionStrings.Mecanica, query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                peca = new Peca
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nome = row["Nome"]?.ToString(),
                    Marca = row["Marca"]?.ToString(),
                    Modelo = row["Modelo"]?.ToString(),
                    Ano = Convert.ToInt32(row["Ano"]),
                    Preco = Convert.ToDecimal(row["Preco"]),
                    Quantidade = Convert.ToInt32(row["QuantidadeEstoque"])

                };

            }

            return peca;
        }

        public async Task<bool> AlteraQuantidadePeca(int quantidade, int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Id", Value = id },
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@QuantidadeEstoque", Value = quantidade },
            };

            string query = @$"UPDATE [Pecas]
                              SET 
                              QuantidadeEstoque = @QuantidadeEstoque
                              where id = @Id";

            int rowsAffect = await ExecutarUpdate(_connectionStrings.Mecanica, query, parameters);

            return (rowsAffect == 1);
        }

        public async Task<bool> InsereManutencao(Manutencao manutencao)
        {

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ModeloVeiculo", Value = manutencao.CarroUsado },
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Placa", Value = manutencao.Placa},
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@ValorManutencao", Value = manutencao.Valor },
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@PecaUsadaId", Value = manutencao.Peca.Id },

            };

            string query = @$"INSERT INTO Manutencao (ModeloVeiculo, Placa, ValorManutencao, PecaUsadaId) 
						   VALUES(@ModeloVeiculo, @Placa, @ValorManutencao, @PecaUsadaId)";

            int rowsAffect = await ExecutarInsert(_connectionStrings.Mecanica, query, parameters);

            return (rowsAffect == 1);
        }

    }
}
