using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTO;

namespace SMS_Marketing
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DesktopEndpoint" in both code and config file together.
    public class DesktopEndpoint : IDesktopEndpoint
    {
        /// <summary>
        /// /////////////////////////////////// Account ///////////////////////////////
        /// </summary>
        BUS_Account _Account = new BUS_Account();
        
        public bool Insert(DTO_Account Account)
        {
            return _Account.Insert(Account);
        }

        public bool Signin(string sql)
        {
            return _Account.SignIn(sql);
        }

        public string RandomID()
        {
            return _Account.RandomID();
        }

        public bool Update(DTO_Account Account)
        {
            return _Account.Update(Account);
        }

        public string GetID(string sql)
        {
            return _Account.Account(sql,0);
        }
        public string GetName(string sql)
        {
            return _Account.Account(sql,4);
        }

        /// <summary>
        /// ///////////////////////Customers /////////////////////////////////////////
        /// </summary>
        BUS_Customers _customers = new BUS_Customers();
        public ICollection<DTO_Customer> GetCustomer(string sql)
        {
            List<DTO_Customer> _list = new List<DTO_Customer>();
            _list = cGlobalFunction.ConvertToList<DTO_Customer>(_customers.Load(sql));

            ICollection<DTO_Customer> _collection = _list;
            return _collection;
        }

        public bool Insert(DTO_Customer _Cus)
        {
            return _customers.Insert(_Cus);
        }

        public bool Delete(DTO_Customer _Cus)
        {
            return _customers.Delete(_Cus);
        }

        public bool Update(DTO_Customer _Cus)
        {
            return _customers.Update(_Cus);
        }

        public string RandomID_cus()
        {
            return _customers.RandomID();
        }

        

        /////////////////////////////////Send SMS /////////////////////////////////
        BUS_SMS _BUS_SMS = new BUS_SMS();

        public bool InsetSMS_toDatabase(DTO_SMS sms)
        {
            return _BUS_SMS.InsetSMS_toDatabase(sms, "SendSMS");
        }
        /////////////////////////////////Schedule /////////////////////////////////

        BUS_Schedule _schedule = new BUS_Schedule();
        public ICollection<DTO_Schedule> GetSchedule()
        {
            List<DTO_Schedule> _list = new List<DTO_Schedule>();
            _list = cGlobalFunction.ConvertToList<DTO_Schedule>(_schedule.Load());

            ICollection<DTO_Schedule> _collection = _list;
            return _collection;
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

        public string RandomID_schedule()
        {
            return _schedule.RandomID();
        }

        /////////////////////////////////Group /////////////////////////////////

        BUS_Group _Group = new BUS_Group();
        public ICollection<DTO_Group> GetGroup(string sql)
        {
            List<DTO_Group> _list = new List<DTO_Group>();
            _list = cGlobalFunction.ConvertToList<DTO_Group>(_Group.Load(sql));

            ICollection<DTO_Group> _collection = _list;
            return _collection;
        }

        public bool Insert(DTO_Group _group)
        {
            return _Group.Insert(_group);
        }

        public bool Delete(DTO_Group _group)
        {
            return _Group.Delete(_group);
        }

        public bool Update(DTO_Group _group)
        {
            return _Group.Update(_group);
        }

        public string RandomID_group()
        {
            return _Group.RandomID();
        }

        /////////////////////////////////SMS Template /////////////////////////////////
        BUS_SMS_Temp _BUStemp = new BUS_SMS_Temp();
        public ICollection<DTO_SMS_Template> Gettemp(string sql)
        {
            List<DTO_SMS_Template> _list = new List<DTO_SMS_Template>();
            _list = cGlobalFunction.ConvertToList<DTO_SMS_Template>(_BUStemp.Load(sql));

            ICollection<DTO_SMS_Template> _collection = _list;
            return _collection;
        }

        public bool Insert(DTO_SMS_Template _temp)
        {
            return _BUStemp.Insert(_temp);
        }

        public bool Delete(DTO_SMS_Template _temp)
        {
            return _BUStemp.Delete(_temp);
        }

        public bool Update(DTO_SMS_Template _temp)
        {
            return _BUStemp.Update(_temp);
        }

        public string RandomID_temp()
        {
           return _BUStemp.RandomID();
        }

        public string RandomID_SMS()
        {
            throw new NotImplementedException();
        }
    }
}
