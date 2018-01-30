using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
using System.Data;

namespace SMS_Marketing
{
    public class BUS_SMS_Temp
    {
        DAO_SMS_Template _temp = new DAO_SMS_Template();
        public DataTable Load()
        {
            return _temp.Load();
        }
        public DataTable Load(string sql)
        {
            return _temp.Load(sql);
        }
        public bool Insert(DTO_SMS_Template _tem)
        {
            return _temp.Insert(_tem);
        }
        public bool Delete(DTO_SMS_Template _tem)
        {
            return _temp.Delete(_tem);
        }
        public bool Update(DTO_SMS_Template _tem)
        {
            return _temp.Update(_tem);
        }
        public string RandomID()
        {
            return _temp.RandomID();
        }
    }
}
