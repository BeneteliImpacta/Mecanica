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
    public class EstoqueRepository : BaseRepository, IEstoqueRepository
    {
        public EstoqueRepository(ConnectionStringsDTO connectionStrings) : base(connectionStrings) { }

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

        public async Task<bool> InserePecas(Peca peca)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Nome", Value = peca.Nome },
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Marca", Value = peca.Marca},
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Modelo", Value = peca.Modelo },
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Ano", Value = peca.Ano },
                new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Preco", Value = peca.Preco },
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@QuantidadeEstoque", Value = peca.Quantidade },

            };

            string query = @$"INSERT INTO Pecas (Nome, Marca, Modelo, Ano, Preco, QuantidadeEstoque) 
						   VALUES(@Nome, @Marca, @Modelo, @Ano, @Preco, @QuantidadeEstoque)";

            int rowsAffect = await ExecutarInsert(_connectionStrings.Mecanica, query, parameters);

            return (rowsAffect == 1);
        }

        public async Task<ICollection<Peca>> ConsultarPecas()
        {
            var listaPeca = new List<Peca>();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                
            };

            var query = @$"SELECT [Id]
                                  ,[Nome]
                                  ,[Marca]
                                  ,[Modelo]
                                  ,[Ano]
                                  ,[Preco]
                                  ,[QuantidadeEstoque]
                              FROM [Mecanica].[dbo].[Pecas] ";


            DataTable dataTable = await ExecutarSelect(_connectionStrings.Mecanica, query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {

                var peca = new Peca
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nome = row["Nome"]?.ToString(),
                    Marca = row["Marca"]?.ToString(),
                    Modelo = row["Modelo"]?.ToString(),
                    Ano = Convert.ToInt32(row["Ano"]),
                    Preco = Convert.ToDecimal(row["Preco"]),
                    Quantidade = Convert.ToInt32(row["QuantidadeEstoque"])

                };

                listaPeca.Add(peca);
            }

            return listaPeca;
        }

        public async Task<Peca> ConsultarPecaParaAlterar(int id)
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

        public async Task<bool> AlteraPeca(Peca peca)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Id", Value = peca.Id },
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Nome", Value = peca.Nome },
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Marca", Value = peca.Marca},
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Modelo", Value = peca.Modelo },
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@Ano", Value = peca.Ano },
                new SqlParameter() { DbType = DbType.Decimal, Direction = ParameterDirection.Input, ParameterName = "@Preco", Value = peca.Preco },
                new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@QuantidadeEstoque", Value = peca.Quantidade },

            };

            string query = @$"UPDATE [Pecas]
                              SET 
                              Nome = @Nome,
                              Marca = @Marca,
                              Modelo = @Modelo,
                              Ano = @Ano,
                              Preco = @Preco,
                              QuantidadeEstoque = @QuantidadeEstoque
                              where id = @Id";

            int rowsAffect = await ExecutarUpdate(_connectionStrings.Mecanica, query, parameters);

            return (rowsAffect == 1);
        }
    }
}
