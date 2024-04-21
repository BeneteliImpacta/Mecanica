using MecanicaBeneteli.Business.Interfaces.Repository;
using MecanicaBeneteli.Business.Models;
using MecanicaBeneteli.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Data.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepository(ConnectionStringsDTO connectionStrings) : base(connectionStrings) { }

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

        public async Task<bool> InsereUsuario(Usuario usuario)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Nome", Value = usuario.Nome },
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Cpf", Value = usuario.CPF},
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Usuario", Value = usuario.IdUsuario },
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Senha", Value = usuario.Senha },
                
            };

            string query = @$" INSERT INTO Usuarios (Nome,Cpf,DataCriacao,Usuario,Senha)
						   VALUES(@Nome, @Cpf, GETDATE(), @Usuario, @Senha)";

            int rowsAffect = await ExecutarInsert(_connectionStrings.Mecanica, query, parameters);

            return (rowsAffect == 1);
        }

        public async Task<Usuario> ConsultarSenha(string idUsuario)
        {
            Usuario usuario = null;

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@idUsuario", Value = idUsuario }
            };

            var query = @$"SELECT [Id]
                                  ,[Nome]
                                  ,[Senha]
                              FROM [Mecanica].[dbo].[Usuarios]
                              where Usuario = @idUsuario
                            ";


            DataTable dataTable = await ExecutarSelect(_connectionStrings.Mecanica, query, parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];

                usuario = new Usuario
                {
                    Nome = row["Nome"]?.ToString(),
                    Senha = row["Senha"]?.ToString()

                };

            }

            return usuario;
        }
    }
}
