using System.IO;
using System;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net.Mail;
using Newtonsoft.Json;

namespace Emilia.Extensions
{
    public class Module
    {
        #region Singleton config

        private static Module __module;

        private Module() {}

        public static Module Sql {
            get{
                if(__module == null)
                {
                     __module = new Module();
                }   
                 return __module;
            }
        }
        #endregion

        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable table;

        public DataTable Table {get{return table;}}
        public SqlDataAdapter Adapter {get{return this.adapter;}}

        public void SetConnection(SqlConnection connection)
        {
            this.connection = connection;

            try {
                this.connection.Open();
            }catch(SqlException e)
            {
                string error = e.Message;
            }
            finally{
                connection.Close();
            }
        }

        public void SetConnection(IDbConnection connection)
        {
            this.connection = connection as SqlConnection;

            try {
                this.connection.Open();
            }catch(SqlException e)
            {
                string error = e.Message;
            }
            finally{
                connection.Close();
            }
        }

         public void RunCommandText(string sql)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = (sql);
            command.ExecuteNonQuery();
            connection.Close();
        }

         public object RunFunctionCommandText(string sql)
        {
            object x;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = (sql);
            x= command.ExecuteScalar();
            connection.Close();
            return x;
        }

        public void Save(string table, string fieldname, string Value)
        {
            string sqlSave = "insert into " + table + " (" + fieldname + ")" + " Values(" + Value + ")";
            RunCommandText(sqlSave);
        }

        public void CommandTextUpdate(string tbl, string fields, string cond)
        {
            string sqlUpdate = "Update " + tbl + " Set " + fields + " Where " + cond + "";
            RunSql(sqlUpdate);
        }

        public void RunSql(string sql)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                adapter.Fill(dt);
                dt.Dispose();
                adapter.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
               ex.Message.ToString();
            }
        }

        public void FillDataTable(string sql)
        {
            try
            {
                command = new SqlCommand(sql, connection);
                this.adapter = new SqlDataAdapter();
                this.adapter.SelectCommand = command;


                this.table = new DataTable();
                this.adapter.Fill(this.table);
                this.adapter.Dispose();
                this.adapter = null;
                command.Dispose();
                command = null;
                connection.Close();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
    }


}