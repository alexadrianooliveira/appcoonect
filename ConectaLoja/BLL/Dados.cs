using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ConectaLoja.BLL
{
    public abstract class Dados
    {
        protected string strquery = "";
        private static string StringDeConexao
        {
            get
            {
                //string filePath = Application.StartupPath + "\\Data\\appconnect.db";
                //return filePath;
                return "Data Source=C:\\Users\\Alex\\source\\repos\\ConectaLoja\\ConectaLoja\\Data\\appconnect.db;";
            }
        }

        public static IDbConnection GetConection()
        {
            IDbConnection oConn;
            oConn = new SQLiteConnection(StringDeConexao);
            return (oConn);
        }

        private static IDbDataAdapter GetDataAdapter()
        {
            IDbDataAdapter da;
            da = new SQLiteDataAdapter();
            return (da);
        }

        public static IDbDataParameter GetParameter(string tipo, object valor)
        {
            IDbDataParameter pa;
            pa = new SQLiteParameter(tipo, valor);
            return (pa);
        }

        public static DataSet ExecutaSQLDataSet(string sql)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbDataAdapter da = GetDataAdapter();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public static DataSet ExecutaSQLDataSet(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbDataAdapter da = GetDataAdapter();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                foreach (IDbDataParameter p in param)
                {
                    da.SelectCommand.Parameters.Add(p);
                }
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public static DataTable ExecutaSQLDataTable(string sql)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbDataAdapter da = GetDataAdapter();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                cn.Close();
                cn.Dispose();
                return ds.Tables[0];
            }
        }

        public static DataTable ExecutaSQLDataTable(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbDataAdapter da = GetDataAdapter();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                foreach (IDbDataParameter p in param)
                {
                    da.SelectCommand.Parameters.Add(p);
                }
                DataSet ds = new DataSet();
                da.Fill(ds);
                cn.Close();
                return ds.Tables[0];
            }
        }

        public static void ExecutaSQLNonQuery(string sql)
        {
            using (IDbConnection cn = GetConection())
            {
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public static void ExecutaSQLNonQuery(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                foreach (IDbDataParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public static object ExecutaSQLScalar(string sql)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                object result;
                result = cmd.ExecuteScalar();
                cn.Close();
                return result;
            }
        }

        public static object ExecutaSQLScalarProcedure(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                object result;
                foreach (IDbDataParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
                cn.Open();
                result = cmd.ExecuteScalar();
                cn.Close();
                return result;
            }
        }

        public static object ExecutaSQLScalar(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                object result;
                foreach (IDbDataParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
                cn.Open();
                result = cmd.ExecuteScalar();
                cn.Close();
                return result;
            }
        }

        public static IDataReader ExecutaSQLReaderQuery(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                IDataReader dr;
                foreach (IDbDataParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
                cn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
        }

        public static void ExecutaStoredProcedureNonQuery(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (IDbDataParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public static DataTable ExecutaSQLDataTableProcedure(string sql, params IDbDataParameter[] param)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbDataAdapter da = GetDataAdapter();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                foreach (IDbDataParameter p in param)
                {
                    da.SelectCommand.Parameters.Add(p);
                }
                DataSet ds = new DataSet();
                da.Fill(ds);
                cn.Close();
                return ds.Tables[0];
            }
        }

        public static void ExecutaStoredProcedureNonQuery(string sql)
        {
            using (IDbConnection cn = GetConection())
            {
                cn.Open();
                IDbCommand cmd = cn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}
