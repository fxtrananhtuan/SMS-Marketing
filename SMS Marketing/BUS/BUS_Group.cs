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
    public class BUS_Group
    {
        DAO_Group _Group = new DAO_Group();
        public DataTable Load()
        {
            return _Group.Load();
        }
        public DataTable Load(string sql)
        {
            return _Group.Load(sql);
        }
        public bool Insert(DTO_Group group)
        {
            return _Group.Insert(group);
        }
        public bool Delete(DTO_Group group)
        {
            return _Group.Delete(group);
        }
        public bool Update(DTO_Group group)
        {
            return _Group.Update(group);
        }
        public string RandomID()
        {
            return _Group.RandomID();
        }
    }
}
