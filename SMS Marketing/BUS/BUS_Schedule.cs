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
    public  class BUS_Schedule
    {
        DAO_Schedule _schedule = new DAO_Schedule();
        public DataTable Load()
        {
            return _schedule.Load();
        }
        public DataTable Load(string sql)
        {
            return _schedule.Load(sql);
        }
        public bool Insert(DTO_Schedule _sch)
        {
            return _schedule.Insert(_sch);
        }
        public bool Delete(DTO_Schedule _sch)
        {
            return _schedule.Delete(_sch);
        }
        public bool Update(DTO_Schedule _sch)
        {
            return _schedule.Update(_sch);
        }
        public bool Update_state(DTO_Schedule _sch)
        {
            return _schedule.Update_state(_sch);
        }
        public string RandomID()
        {
            return _schedule.RandomID();
        }
    }
}
