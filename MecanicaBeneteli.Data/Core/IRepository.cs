using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Data.Core
{
    public interface IRepository
    {
        void BeginTransaction(string connectionString);

        void Commit();

        void Rollback();

        Task<DataTable> ExecutarSelect(string connectionString, string query, List<SqlParameter> parameters = null);

        Task<int> ExecutarInsert(string connectionString, string query, List<SqlParameter> parameters);

        Task<int> ExecutarUpdate(string connectionString, string query, List<SqlParameter> parameters);

        Task<int> ExecutarDelete(string connectionString, string query, List<SqlParameter> parameters);

        Task<DataTable> ExecutarSelectTransacao(string query, List<SqlParameter> parameters = null);

        Task<int> ExecutarInsertTransacao(string query, List<SqlParameter> parameters);

        Task<int> ExecutarUpdateTransacao(string query, List<SqlParameter> parameters);

        Task<int> ExecutarDeleteTransacao(string query, List<SqlParameter> parameters);
    }
}
