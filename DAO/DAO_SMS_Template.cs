using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class DAO_SMS_Template:DAODataProvider
    {
        public DataTable Load()
        {
            DataTable _DSTD = new DataTable();
            Connet();
            try
            {
                string sql = "select * from SMS_Template";
                _cmd = new SqlCommand(sql, _conn);
                _da = new SqlDataAdapter(_cmd);
                _da.Fill(_DSTD);
                _cmd.Dispose();
                Disconnet();
            }
            catch (Exception ex)
            {
                Disconnet();
                throw ex;
            }
            return _DSTD;
        }
        public DataTable Load(string sql)
        {
            DataTable _DSTD = new DataTable();
            Connet();
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _da = new SqlDataAdapter(_cmd);
                _da.Fill(_DSTD);
                _cmd.Dispose();
                Disconnet();
            }
            catch (Exception ex)
            {
                Disconnet();
                throw ex;
            }
            return _DSTD;
        }
        public bool Insert(DTO_SMS_Template _temp)
        {
            Connet();
            string sql = "insert into SMS_Template( SMS_Template, ID, Account) values (@SMS_Template, @ID, @Account)";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@SMS_Template", SqlDbType.NVarChar).Value = _temp.SMS_Template;
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _temp.ID;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _temp.Account;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                Disconnet();
            }
            catch (Exception ex)
            {
                Disconnet();
                return false;
                throw ex;
            }
            return true;
        }

        public bool Delete(DTO_SMS_Template _temp)
        {
            Connet();
            string sql = "delete from SMS_TEMPLATE  where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _temp.ID;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                Disconnet();
            }
            catch (Exception ex)
            {
                Disconnet();
                return false;
                throw ex;
            }
            return true;
        }
        public bool Update(DTO_SMS_Template _temp)
        {
            Connet();
            string sql = "update Customer set SMS_Template=@SMS_Template,AccountID=@AccountID  Where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@SMS_Template", SqlDbType.NVarChar).Value = _temp.SMS_Template;
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _temp.ID;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _temp.Account;
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                Disconnet();
            }
            catch (Exception ex)
            {
                Disconnet();
                return false;
                throw ex;
            }
            return true;
        }

        public string RandomID()
        {
            Connet();
            return CreateIDCode("SMSTP", "ID", "SMS_TEMPLATE", 10);
        }
    }
}
