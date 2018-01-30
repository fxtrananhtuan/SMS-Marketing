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
    public class DAO_Schedule:DAODataProvider
    {
        public DataTable Load()
        {
            DataTable _DSTD = new DataTable();
            Connet();
            try
            {
                string sql = "select * from Schedule";
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
        public bool Insert(DTO_Schedule _Cus)
        {
            Connet();
            string sql = "insert into Schedule ( ID, Date, Time, SMS_info, AccountID, Active) values (@ID, @Date, @Time, @SMS_info, @AccountID, @Active)";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Cus.ID;
                _cmd.Parameters.Add("@Date", SqlDbType.Date).Value = _Cus.Date;
                _cmd.Parameters.Add("@Time", SqlDbType.DateTime).Value = _Cus.Time;
                _cmd.Parameters.Add("@SMS_info", SqlDbType.VarChar).Value = _Cus.SMS_info;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _Cus.AccountID;
                int i = 0;
                if(_Cus.Active==true)
                {
                    _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = 1;
                }
                else
                {
                    _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = 0;
                }
               
                
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

        public bool Delete(DTO_Schedule _Cus)
        {
            Connet();
            string sql = "delete from Schedule where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Cus.ID;
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
        public bool Update(DTO_Schedule _Cus)
        {
            Connet();
            string sql = "update Schedule set Date=@Date, Time=@Time,SMS_info =@SMS_info,AccountID= @AccountID, Active=@Active  Where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Cus.ID;
                _cmd.Parameters.Add("@Date", SqlDbType.Date).Value = _Cus.Date;
                _cmd.Parameters.Add("@Time", SqlDbType.DateTime).Value = _Cus.Time;
                _cmd.Parameters.Add("@AccountID", SqlDbType.VarChar).Value = _Cus.AccountID;
                _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = _Cus.Active;
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
        public bool Update_state(DTO_Schedule _Cus)
        {
            Connet();
            string sql = "update Schedule set Active=@Active  Where ID=@ID";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _Cus.ID;
                _cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = _Cus.Active;
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
            return CreateIDCode("SCH", "ID", "Schedule", 10);
        }
    }
}
