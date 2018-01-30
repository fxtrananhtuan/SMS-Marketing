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
    public class DAO_Account:DAODataProvider
    {
        public DataTable Load()
        {
            DataTable _DSTD = new DataTable();
            Connet();
            try
            {
                string sql = "select * from Account ";
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
        public string Account(string sql,int colum)
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
            return _DSTD.Rows[0][colum].ToString();
        }
        public bool SignIn(string sql)
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
            catch 
            {
                Disconnet();
                return false;
            }
            if(_DSTD.Rows.Count==1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
        }

        public bool Insert(DTO_Account _Account)
        {
            Connet();
            string sql = "insert into Account (ID,email,create_date,Password,Name,street,suburb,state,zip,phone,Active) values (@ID,@email,@create_date,@Password,@Name,@street,@suburb,@state,@zip,@phone,@Active)";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Account.ID;
                _cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = _Account.email;
                _cmd.Parameters.Add("@create_date", SqlDbType.DateTime).Value = _Account.create_date;
                _cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = _Account.Password;
                _cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Account.Name;
                _cmd.Parameters.Add("@street", SqlDbType.VarChar).Value = _Account.street;
                _cmd.Parameters.Add("@suburb", SqlDbType.VarChar).Value = _Account.city;
                _cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = _Account.state;
                _cmd.Parameters.Add("@zip", SqlDbType.Int).Value = _Account.zip;
                _cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = _Account.Phone;
                _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = _Account.Active;
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

        public bool Delete(DTO_Account _Account)
        {
            Connet();
            string sql = "delete from Account where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Account.ID;
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
        public bool Update(DTO_Account _Account)
        {
            Connet();
            string sql = "update Account set email=@email,create_date=@create_date,Password=@Password,Name=@Name,street=@street,suburb=@suburb,state=@state,zip=@zip,phone=@phone,Active=@Active where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Account.ID;
                _cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = _Account.email;
                _cmd.Parameters.Add("@create_date", SqlDbType.DateTime).Value = _Account.create_date;
                _cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = _Account.Password;
                _cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = _Account.Name;
                _cmd.Parameters.Add("@street", SqlDbType.VarChar).Value = _Account.street;
                _cmd.Parameters.Add("@suburb", SqlDbType.VarChar).Value = _Account.city;
                _cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = _Account.state;
                _cmd.Parameters.Add("@zip", SqlDbType.Int).Value = _Account.zip;
                _cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = _Account.Phone;
                _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = _Account.Active;
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
            return CreateIDCode("ASMSM","ID", "Account", 10);
        }

    }
}
