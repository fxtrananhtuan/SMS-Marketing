using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_Group:DAODataProvider
    {
        public DataTable Load()
        {
            DataTable _DSTD = new DataTable();
            Connet();
            try
            {
                string sql = "select * from [Group]";
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
        public bool Insert(DTO_Group _Group)
        {
            Connet();
            string sql = "insert into Group ( ID, Group_Name, AccountID, Phone) values (@ID, @Group_Name, @AccountID, @Phone)";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Group.ID;
                _cmd.Parameters.Add("@Group_Name", SqlDbType.VarChar).Value = _Group.Group_Name;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _Group.AccountID;
                _cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Group.Phone;
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

        public bool Delete(DTO_Group _Group)
        {
            Connet();
            string sql = "delete from Group where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Group.ID;
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
        public bool Update(DTO_Group _Group)
        {
            Connet();
            string sql = "update Group set Group_Name=@Group_Name,AccountID=@AccountID,Phone=@APhone Where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Group.ID;
                _cmd.Parameters.Add("@Group_Name", SqlDbType.VarChar).Value = _Group.Group_Name;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _Group.AccountID;
                _cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = _Group.Phone;
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
            return CreateIDCode("GRO", "ID", "Group", 10);
        }
    }
}
