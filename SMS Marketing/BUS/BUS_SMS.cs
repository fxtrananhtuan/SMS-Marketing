using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
using System.Data;
using System.IO.Ports;

namespace SMS_Marketing
{
    public class BUS_SMS
    {
        DAO_SMS _SMS = new DAO_SMS();

        //Open Port
        public SerialPort OpenPort(string p_strPortName, int p_uBaudRate, int p_uDataBits, int p_uReadTimeout, int p_uWriteTimeout)
        {
            return _SMS.OpenPort(p_strPortName, p_uBaudRate, p_uDataBits, p_uReadTimeout, p_uWriteTimeout);
        }

        public void ClosePort(SerialPort port)
        {
            _SMS.ClosePort(port);
        }


        #region Count SMS
        public int CountSMSmessages(SerialPort port)
        {
            return _SMS.CountSMSmessages(port);
        }

        #endregion

        #region Read SMS
        public DTO_SMS_Collection ReadSMS(SerialPort port, string p_strCommand)
        {
            return _SMS.ReadSMS(port, p_strCommand);
        }

        #endregion

        #region Send SMS

        public bool sendMsg(SerialPort port, string PhoneNo, string Message)
        {
            return _SMS.sendMsg(port, PhoneNo, Message);
        }
        #endregion

        #region Send Delete
        public bool DeleteMsg(SerialPort port, string p_strCommand)
        {
            return _SMS.DeleteMsg(port, p_strCommand);
        }
        #endregion

        public bool InsetSMS_toDatabase(DTO_SMS sms, string table)
        {
            return _SMS.InsertSMS_ToDatabase(sms, table);
        }

        public bool DeleteSMS_toDatabase(DTO_SMS sms, string table)
        {
            return _SMS.DeleteSMS_ToDatabase(sms, table);
        }

        public bool UpdateSMS_toDatabase(DTO_SMS sms, string table)
        {
            return _SMS.UpdateSMS_ToDatabase(sms, table);
        }

        public DataTable LoadSMS(string table)
        {
            return _SMS.LoadSMS_ToDatabase(table);
        }

        public DTO_SMS DoAnalyse(DTO_SMS sms)
        {
            return _SMS.DoAnalyse(sms);
        }
        public string ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            return _SMS.ExecCommand(port,command,responseTimeout,errorMessage).Result;
        }
    }
}
