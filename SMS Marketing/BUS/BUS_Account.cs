using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DTO;
using DAO;

namespace SMS_Marketing
{
    public class BUS_Account
    {
        DAO_Account _Account = new DAO_Account();
        public DataTable Load()
        {
            return _Account.Load();
        }
        public DataTable Load(string sql)
        {
            return _Account.Load(sql);
        }
        public bool SignIn(string sql)
        {
            return _Account.SignIn(sql);
        }
        public bool Insert(DTO_Account Account)
        {
            return _Account.Insert(Account);
        }
        public bool Delete(DTO_Account Account)
        {
            return _Account.Delete(Account);
        }
        public bool Update(DTO_Account Account)
        {
            return _Account.Update(Account);
        }
        public string RandomID()
        {
            return _Account.RandomID();
        }
        public string Account (string sql,int column)
        {
            return _Account.Account(sql,column);
        }
    }
}
