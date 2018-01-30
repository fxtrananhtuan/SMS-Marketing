using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAODataProvider
    {
        public string _strconn;
        public SqlCommand _cmd;
        public SqlConnection _conn;
        protected SqlDataAdapter _da;
        public bool Connet()
        {
              //_strconn =" Server = tcp:smscos80010.database.windows.net,1433; Database = COS80010; User ID = Anh@smscos80010; Password =Cos80010; Encrypt = True; TrustServerCertificate = true; Connection Timeout = 30";
            //_strconn ="Server = tcp:smscos80010.database.windows.net,1433; Database = COS80010; User ID = Anh@smscos80010; Password =Cos80010; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30";
             _strconn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SMS_Maketing;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                _conn = new SqlConnection(_strconn);
                _conn.Open();
            }
            catch (Exception ex)
            {
                _conn.Close();
                return false;
                throw ex;
            }
            return true;

        }
        public void Disconnet()
        {
            _conn.Close();
        }


        #region Execute Query Methods

        /// <summary>
        /// Execute query string, return DataSet
        /// </summary>
        /// <param name="strSelect">Query string</param>
        /// <returns>DataSet contains result</returns>
        public DataSet ExecuteQuery_DataSet(string strSelect)
        {
            DataSet dataset = new DataSet();
            _cmd = new SqlCommand();
            _cmd.Connection = this._conn;
            _da = new SqlDataAdapter(strSelect, _conn);
            try
            {
                _da.Fill(dataset);
            }
            catch (SqlException e)
            {
                throw e;
            }
            return dataset;
        }

        /// <summary>
        /// Execute query string, return DataTable
        /// </summary>
        /// <param name="strSelect">Query string</param>
        /// <returns>DataTable contains result</returns>
        public DataTable ExecuteQuery_DataTable(string strSelect)
        {
            return ExecuteQuery_DataSet(strSelect).Tables[0];
        }

        /// <summary>
        /// Execute Non-query string
        /// </summary>
        /// <param name="SqlString">Non-query string </param>
        public void ExecuteNonQuery(string SqlString)
        {
            _conn.Open();
            _cmd = new SqlCommand(SqlString, _conn);
            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        /// <summary>
        /// Execute query string, return first rows and first colum of result 
        /// </summary>
        /// <param name="SqlString">Query string</param>
        /// <returns>Object contains result</returns>
        public object ExecuteScalar(string SqlString)
        {
            _conn.Open();
            _cmd = new SqlCommand(SqlString, _conn);
            object _ret = _cmd.ExecuteScalar();
            _conn.Close();

            return _ret;
        }

        #endregion


        protected string CreateIDCode(string strID, string field, string table, int length)
        {
            try
            {
                this.Connet();
                string IDCode = strID + "0000000000";
                string query = "select max(" + field.Trim() + ") from " + table.Trim() + " where left(" + field.Trim() + "," + strID.Length + ")='" + strID.Trim() + "'";

                this._cmd = new SqlCommand(query, this._conn);
                this._cmd.CommandType = CommandType.Text;
                SqlDataReader reader = this._cmd.ExecuteReader();

                while (reader.Read())
                    if (!reader.IsDBNull(0))
                        IDCode = Convert.ToString(reader.GetValue(0));

                IDCode = IDCode.Substring(strID.Length);
                IDCode = "0000000000000000" + Convert.ToString(Convert.ToInt64(IDCode) + 1);
                IDCode = strID + IDCode.Substring(IDCode.Length - length + strID.Length);

                this.Disconnet();
                return IDCode;
            }
            catch (Exception ex)
            {
                this.Disconnet();
                throw ex;
            }
        }
    }
}
