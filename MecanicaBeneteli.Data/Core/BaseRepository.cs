using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Data.Core
{
    public abstract class BaseRepository : IRepository
    {
        protected readonly ConnectionStringsDTO _connectionStrings;
        private SqlConnection _conexaoParaTransacoes;
        private SqlTransaction _transacao;

        public BaseRepository(ConnectionStringsDTO connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public void BeginTransaction(string connectionString)
        {
            if (_conexaoParaTransacoes != null)
                _conexaoParaTransacoes.Dispose();

            _conexaoParaTransacoes = RetornarConexao(connectionString);
            _conexaoParaTransacoes.Open();
            _transacao = _conexaoParaTransacoes.BeginTransaction();
        }

        public void Commit()
        {
            if (_conexaoParaTransacoes.State != ConnectionState.Closed)
            {
                _transacao?.Commit();
                _transacao = null;
                _conexaoParaTransacoes?.Dispose();
            }
        }

        public void Rollback()
        {
            if (_conexaoParaTransacoes.State != ConnectionState.Closed)
            {
                _transacao?.Rollback();
                _transacao = null;
                _conexaoParaTransacoes?.Dispose();
            }
        }



        public async Task<DataTable> ExecutarSelect(string connectionString, string query, List<SqlParameter> parameters = null)
        {
            using var conexao = RetornarConexao(connectionString);

            using var command = conexao.CreateCommand();
            command.CommandText = query;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            conexao.Open();

            using var dataReader = await command.ExecuteReaderAsync();
            DataTable resultDataTable = new DataTable();
            resultDataTable.Load(dataReader);

            conexao.Close();

            return resultDataTable;
        }

        public async Task<int> ExecutarInsert(string connectionString, string query, List<SqlParameter> parameters)
        {
            using var conexao = RetornarConexao(connectionString);

            using var command = conexao.CreateCommand();
            command.CommandText = query;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            conexao.Open();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            conexao.Close();

            return rowsAffected;
        }

        public async Task<int> ExecutarUpdate(string connectionString, string query, List<SqlParameter> parameters)
        {
            using var conexao = RetornarConexao(connectionString);

            using var command = conexao.CreateCommand();
            command.CommandText = query;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            conexao.Open();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            conexao.Close();

            return rowsAffected;
        }

        public async Task<int> ExecutarDelete(string connectionString, string query, List<SqlParameter> parameters)
        {
            using var conexao = RetornarConexao(connectionString);

            using var command = conexao.CreateCommand();
            command.CommandText = query;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            conexao.Open();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            conexao.Close();

            return rowsAffected;
        }

        public async Task<DataTable> ExecutarSelectTransacao(string query, List<SqlParameter> parameters = null)
        {
            if (_transacao == null)
                throw new Exception("Necessário iniciar transação no domain service para utilização desse método");

            using var command = _conexaoParaTransacoes.CreateCommand();
            command.CommandText = query;
            command.Transaction = _transacao;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            using var dataReader = await command.ExecuteReaderAsync();
            DataTable resultDataTable = new DataTable();
            resultDataTable.Load(dataReader);

            return resultDataTable;
        }

        public async Task<int> ExecutarInsertTransacao(string query, List<SqlParameter> parameters)
        {
            if (_transacao == null)
                throw new Exception("Necessário iniciar transação no domain service para utilização desse método");

            using var command = _conexaoParaTransacoes.CreateCommand();
            command.CommandText = query;
            command.Transaction = _transacao;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected;
        }

        public async Task<int> ExecutarUpdateTransacao(string query, List<SqlParameter> parameters)
        {
            if (_transacao == null)
                throw new Exception("Necessário iniciar transação no domain service para utilização desse método");

            using var command = _conexaoParaTransacoes.CreateCommand();
            command.CommandText = query;
            command.Transaction = _transacao;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected;
        }

        public async Task<int> ExecutarDeleteTransacao(string query, List<SqlParameter> parameters)
        {
            if (_transacao == null)
                throw new Exception("Necessário iniciar transação no domain service para utilização desse método");

            using var command = _conexaoParaTransacoes.CreateCommand();
            command.CommandText = query;
            command.Transaction = _transacao;

            if (parameters != null && parameters.Count > 0)
                command.Parameters.AddRange(parameters.ToArray());

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected;
        }



        private SqlConnection RetornarConexao(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
