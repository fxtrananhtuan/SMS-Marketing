using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data;

namespace DAO
{
    public class DAO_Customer:DAODataProvider
    {
        public DataTable Load()
        {
            DataTable _DSTD = new DataTable();
            Connet();
            try
            {
                string sql = "select * from Customer where active=1";
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
        public bool Insert(DTO_Customer _Cus)
        {
            Connet();
            string sql = "insert into Customer ( Phone, Name, AccountID, Active, GroupID) values (@Phone, @Name, @AccountID, @Active, @GroupID)";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Cus.Phone;
                _cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Cus.Name;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _Cus.AccountID;
                _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = _Cus.Active;
                _cmd.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = _Cus.GroupID;
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

        public bool Delete(DTO_Customer _Cus)
        {
            Connet();
            string sql = "delete from Customer where Phone=@Phone";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Cus.Phone;
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
        public bool Update(DTO_Customer _Cus)
        {
            Connet();
            string sql = "update Customer set Name=@Name,AccountID=@AccountID,Active=@Active,GroupID=@GroupID  Where Phone=@Phone";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Cus.Phone;
                _cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Cus.Name;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _Cus.AccountID;
                _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = _Cus.Active;
                _cmd.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = _Cus.GroupID;
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

        public bool Update_state(DTO_Customer _Cus)
        {
            Connet();
            string sql = "update Customer set Active=@Active  Where Phone=@Phone";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Cus.Phone;
                _cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Cus.Name;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _Cus.AccountID;
                _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = _Cus.Active;
                _cmd.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = _Cus.GroupID;
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
            return CreateIDCode("CUS", "ID", "Customer", 10);
        }

    }

}
