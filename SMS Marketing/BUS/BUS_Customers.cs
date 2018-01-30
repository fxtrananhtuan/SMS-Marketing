using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
using System.Data;

namespace SMS_Marketing
{
    public class BUS_Customers
    {
        DAO_Customer _cus = new DAO_Customer();
        public DataTable Load()
        {
            return _cus.Load();
        }
        public DataTable Load(string sql)
        {
            return _cus.Load(sql);
        }
        public bool Insert(DTO_Customer _Cus)
        {
            return _cus.Insert(_Cus);
        }
        public bool Delete(DTO_Customer _Cus)
        {
            return _cus.Delete(_Cus);
        }
        public bool Update(DTO_Customer _Cus)
        {
            return _cus.Update(_Cus);
        }
        public string RandomID()
        {
            return _cus.RandomID();
        }
    }
}
