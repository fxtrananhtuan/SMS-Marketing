using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTO;
using System.Data;

namespace SMS_Marketing
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDesktopEndpoint" in both code and config file together.
    [ServiceContract]
    public interface IDesktopEndpoint
    {
        /// <summary>
        /// ////////////////////////////////////////////Account ////////////////////////////////////
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        [OperationContract (Name = "Insert")]
        bool Insert(DTO_Account Account);

        [OperationContract(Name = "Update")]
        bool Update(DTO_Account Account);
        [OperationContract(Name = "RandomID")]
        string RandomID();

        [OperationContract(Name = "CheckSignin")]
        bool Signin(string sql);

        [OperationContract(Name = "GetID")]
        string GetID(string sql);

        [OperationContract(Name = "GetName")]
        string GetName(string sql);

        /// <summary>
        /// //////////////////////////// Customers //////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "load_Customer")]
        ICollection<DTO_Customer> GetCustomer(string sql);

        [OperationContract(Name = "Insert_Customer")]
        bool Insert(DTO_Customer _Cus);
        [OperationContract(Name = "Delete_Customer")]
        bool Delete(DTO_Customer _Cus);
        [OperationContract(Name = "Update_Customer")]
        bool Update(DTO_Customer _Cus);
        [OperationContract(Name = "RandomID_Customer")]
        string RandomID_cus();


        /// //////////////////////////// Customers //////////////////////////////////////
        [OperationContract(Name = "SMSID")]
        string RandomID_SMS();
        [OperationContract(Name = "SendSMS")]
        bool InsetSMS_toDatabase(DTO_SMS sms);

        //////////////////////////////////////////Schedule ///////////////////////////////////////

        [OperationContract(Name = "Load_Schedule")]
        ICollection<DTO_Schedule> GetSchedule();
        [OperationContract(Name = "Insert_Schedule")]
        bool Insert(DTO_Schedule _sch);
        [OperationContract(Name = "Delete_Schedule")]
        bool Delete(DTO_Schedule _sch);
        [OperationContract(Name = "Update_Schedule")]
        bool Update(DTO_Schedule _sch);
        [OperationContract(Name = "RandomID_Schedule")]
        string RandomID_schedule();

        //////////////////////////////////////////Group ///////////////////////////////////////

        [OperationContract(Name = "Load_Group")]
        ICollection<DTO_Group> GetGroup(string sql);
        [OperationContract(Name = "Insert_Group")]
        bool Insert(DTO_Group _group);
        [OperationContract(Name = "Delete_Group")]
        bool Delete(DTO_Group _group);
        [OperationContract(Name = "Update_Group")]
        bool Update(DTO_Group _group);
        [OperationContract(Name = "RandomID_Group")]
        string RandomID_group();


        /////////////////////////////////////////SMS Template ///////////////////////////////////////

        [OperationContract(Name = "Load_Template")]
        ICollection<DTO_SMS_Template> Gettemp(string sql);
        [OperationContract(Name = "Insert_Template")]
        bool Insert(DTO_SMS_Template _temp);
        [OperationContract(Name = "Delete_Template")]
        bool Delete(DTO_SMS_Template _temp);
        [OperationContract(Name = "Update_Template")]
        bool Update(DTO_SMS_Template _temp);
        [OperationContract(Name = "RandomID_Template")]
        string RandomID_temp();


    }

}
