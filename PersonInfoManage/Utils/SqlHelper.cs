using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PersonInfoManage.Utils
{
    public class SqlHelper
    {
        private readonly static string connStr = ConfigurationManager.ConnectionStrings["defaultconnection"].ConnectionString;
        public static DataTable GetTable(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlDataAdapter atper = new SqlDataAdapter(sql, conn))
                {
                    atper.SelectCommand.CommandType = type;
                    if (pars != null)
                    {
                        atper.SelectCommand.Parameters.AddRange(pars);
                    }
                    DataTable da = new DataTable();
                    atper.Fill(da);
                    return da;
                }
            }
        }
        public static int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static object ExecuteScalar(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

    }
}