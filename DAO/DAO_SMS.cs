using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DTO;
using System.Threading.Tasks;

namespace DAO
{
    public class DAO_SMS:DAODataProvider
    {
        #region Open and Close Ports
        //Open Port
        public SerialPort OpenPort(string p_strPortName, int p_uBaudRate, int p_uDataBits, int p_uReadTimeout, int p_uWriteTimeout)
        {
            receiveNow = new AutoResetEvent(false);
            SerialPort port = new SerialPort();

            try
            {
                port.PortName = p_strPortName;                 //COM1
                port.BaudRate = p_uBaudRate;                   //9600
                port.DataBits = p_uDataBits;                   //8
                port.StopBits = StopBits.One;                  //1
                port.Parity = Parity.None;                     //None
                port.ReadTimeout = p_uReadTimeout;             //300
                port.WriteTimeout = p_uWriteTimeout;           //300
                port.Encoding = Encoding.GetEncoding("iso-8859-1");
                port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return port;
        }

        //Close Port
        public void ClosePort(SerialPort port)
        {
            try
            {
                port.Close();
                port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                port = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        //Execute AT Command
        public string ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            return Task.Run(() =>
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                string input = ReadResponse(port, responseTimeout);
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("No success message was received.");
                return input;
            });

           


        }

        //Receive data from port
        public void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                {
                    receiveNow.Set();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            throw new ApplicationException("Response received is incomplete.");
                        else
                            throw new ApplicationException("No data received from phone.");
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }

        #region Count SMS
        public int CountSMSmessages(SerialPort port)
        {
            int CountTotalMessages = 0;
            try
            {

                #region Execute Command

                var recievedData = ExecCommand(port, "AT", 300, "No phone connected at");
                recievedData = ExecCommand(port, "AT+CMGF=1", 300, "Failed to set message format.");
                String command = "AT+CPMS?";
                recievedData = ExecCommand(port, command, 1000, "Failed to count SMS message");
                string Recive_data = recievedData.Result;
                int uReceivedDataLength = Recive_data.Length;

                #endregion

                #region If command is executed successfully
                if ((Recive_data.Length >= 45) && (Recive_data.StartsWith("AT+CPMS?")))
                {

                    #region Parsing SMS
                    string[] strSplit = Recive_data.Split(',');
                    string strMessageStorageArea1 = strSplit[0];     //SM
                    string strMessageExist1 = strSplit[1];           //Msgs exist in SM
                    #endregion

                    #region Count Total Number of SMS In SIM
                    CountTotalMessages = Convert.ToInt32(strMessageExist1);
                    #endregion

                }
                #endregion

                #region If command is not executed successfully
                else if (Recive_data.Contains("ERROR"))
                {

                    #region Error in Counting total number of SMS
                    string recievedError = Recive_data;
                    recievedError = recievedError.Trim();
                    Recive_data = "Following error occured while counting the message" + recievedError;
                    #endregion

                }
                #endregion

                return CountTotalMessages;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Read SMS

        public AutoResetEvent receiveNow;

        public DTO_SMS_Collection ReadSMS(SerialPort port, string p_strCommand)
        {

            // Set up the phone and read the messages
            DTO_SMS_Collection messages = null;
            try
            {

                #region Execute Command
                // Check connection
                ExecCommand(port, "AT", 300, "No phone connected");
                // Use message format "Text mode"
                ExecCommand(port, "AT+CMGF=1", 300, "Failed to set message format.");
                // Use character set "PCCP437"
                ExecCommand(port, "AT+CSCS=\"PCCP437\"", 300, "Failed to set character set.");
                // Select SIM storage
                ExecCommand(port, "AT+CPMS=\"SM\"", 300, "Failed to select message storage.");
                // Read the messages
                var input = ExecCommand(port, p_strCommand, 5000, "Failed to read the messages.");
                string input_s = input.Result;
                #endregion

                #region Parse messages
                messages = ParseMessages(input_s);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (messages != null)
                return messages;
            else
                return null;

        }
        public DTO_SMS_Collection ParseMessages(string input)
        {
            DTO_SMS_Collection messages = new DTO_SMS_Collection();
            try
            {
                Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n");
                Match m = r.Match(input);
                while (m.Success)
                {
                    DTO_SMS msg = new DTO_SMS();
                    msg.Index = m.Groups[1].Value;
                    msg.Status = m.Groups[2].Value;
                    msg.Sender = m.Groups[3].Value;
                    msg.Alphabet = m.Groups[4].Value;
                    msg.Sent = m.Groups[5].Value;
                    msg.Message = m.Groups[6].Value;
                    messages.Add(msg);

                    m = m.NextMatch();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return messages;
        }

        #endregion

        #region Send SMS

        static AutoResetEvent readNow = new AutoResetEvent(false);

        public bool sendMsg(SerialPort port, string PhoneNo, string Message)
        {
            bool isSend = false;

            try
            {

                var recievedData = ExecCommand(port, "AT", 300, "No phone connected");
                recievedData = ExecCommand(port, "AT+CMGF=1", 300, "Failed to set message format.");
                String command = "AT+CMGS=\"" + PhoneNo + "\"";
                recievedData = ExecCommand(port, command, 300, "Failed to accept phoneNo");
                command = Message + char.ConvertFromUtf32(26) + "\r";
                recievedData = ExecCommand(port, command, 3000, "Failed to send message"); //3 seconds
                string recieved_Data = recievedData.Result;
                if (recieved_Data.EndsWith("\r\nOK\r\n"))
                {
                    isSend = true;
                }
                else if (recieved_Data.Contains("ERROR"))
                {
                    isSend = false;
                }
                return isSend;
            }
            catch 
            {
                return isSend;
            }

        }
        static void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                    readNow.Set();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Delete SMS
        public bool DeleteMsg(SerialPort port, string p_strCommand)
        {
            bool isDeleted = false;
            try
            {

                #region Execute Command
                var recievedData = ExecCommand(port, "AT", 300, "No phone connected");
                recievedData = ExecCommand(port, "AT+CMGF=1", 300, "Failed to set message format.");
                String command = p_strCommand;
                recievedData = ExecCommand(port, command, 300, "Failed to delete message");
                #endregion
                string recieved_Data = recievedData.Result;
                if (recieved_Data.EndsWith("\r\nOK\r\n"))
                {
                    isDeleted = true;
                }
                if (recieved_Data.Contains("ERROR"))
                {
                    isDeleted = false;
                }
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        public bool InsertSMS_ToDatabase(DTO_SMS sms, string table)
        {
            string sql = "insert into " + table + " values ('" + RandomID(table) + "','" + sms.Status + "','" + sms.Sender + "','" + sms.Alphabet + "','" + sms.Sent + "','" + sms.Message + "')";

            try
            {
                ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                Disconnet();
                return false;
                throw ex;
            }
            return true;
        }
        public DataTable LoadSMS_ToDatabase(string table)
        {
            DataTable _DSTD = new DataTable();
            Connet();
            try
            {
                string sql = "select * from " + table;
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

        public bool DeleteSMS_ToDatabase(DTO_SMS sms, string table)
        {
            Connet();
            string sql = "delete from " + table + " where ID=@_index";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@_index", SqlDbType.Char).Value = sms.Index;
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
        public bool UpdateSMS_ToDatabase(DTO_SMS sms, string table)
        {
            Connet();
            string sql = "update " + table + " set _index=@_index,_status=@_status,_sender=@_sender,_alphabet=@_alphabet,_sent=@_sent, _message=@_message ";
            try
            {
                _cmd = new SqlCommand(sql, _conn);
                _cmd.Parameters.Add("@_index", SqlDbType.Char).Value = sms.Index;
                _cmd.Parameters.Add("@_status", SqlDbType.NVarChar).Value = sms.Status;
                _cmd.Parameters.Add("@_sender", SqlDbType.NVarChar).Value = sms.Sender;
                _cmd.Parameters.Add("@_alphabet", SqlDbType.NVarChar).Value = sms.Alphabet;
                _cmd.Parameters.Add("@_sent", SqlDbType.NVarChar).Value = sms.Sent;
                _cmd.Parameters.Add("@_message", SqlDbType.NVarChar).Value = sms.Message;
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



        /// <summary>
        /// Define which type of input Mesg
        /// </summary>
        /// <param name="Mesg">Input Mesg need to define type</param>
        /// <returns>Int value which indicate input message type</returns>
        private int TypeDefine(string Mesg)
        {
            if (Mesg.Length < 3)
                return 0;

            Mesg = Mesg.Substring(0, 3);

            if (Mesg.Equals("FWR", StringComparison.OrdinalIgnoreCase))
                return 1;
            else
                if (Mesg.Equals("CAN", StringComparison.OrdinalIgnoreCase))
                return 2;
            else
                return 0;

        }

        private string FindName(string mgs)
        {
            try
            {

                string[] words = mgs.Split(' ');
                mgs = words[1];

                return mgs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DateTime findDate(string mgs)
        {
            try
            {
                string[] words = mgs.Split(' ');
                return Convert.ToDateTime(words[2]);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DateTime findTime(string mgs)
        {
            try
            {
                string[] words = mgs.Split(' ');
                DateTime oDate = DateTime.ParseExact(words[3], "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                if (oDate.Hour > 9 && oDate.Hour < 17)
                {

                }
                return oDate;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public string RandomID(string table)
        {
            Connet();
            return CreateIDCode("SMS", "ID", table, 7);
        }


        /// <summary>
        /// Analyse input SMS, implement request and create reply SMS
        /// </summary>
        /// <param name="_DTOReceivedSMS">SMS need to analyzed </param>
        /// <returns></returns>
        public DTO_SMS DoAnalyse(DTO_SMS sms)
        {
            //Create Reply SMS
            DTO_SMS _RDSMS = new DTO_SMS();
            _RDSMS = sms;
            //Define SMS type and set reply SMS Message
            switch (TypeDefine(_RDSMS.Message))
            {
                //Invalid SMS structure
                case 0:
                    {
                        _RDSMS.Message = "Invalid SMS structure. Available is FWR: Forward and CAN: Cancel";
                        return _RDSMS;
                    }

                #region Register
                case 1:
                    {
                        if (_RDSMS.Message.Length > 5)
                        {
                            string Mesg = _RDSMS.Message.Substring(3, _RDSMS.Message.Length-3);
                            DAO_Account _account = new DAO_Account();
                            _RDSMS.Sender = "0" + _RDSMS.Sender.Substring(3, _RDSMS.Sender.Length - 3);
                            string sql = "select ID from Account where phone='"+_RDSMS.Sender+"'";
                            DataTable tb = new DataTable();
                            tb = _account.Load(sql);
                            if(tb.Rows.Count>0)
                            {
                                string _ac = tb.Rows[0][0].ToString();
                                sql = "select Phone from Customer where AccountID='"+_ac+"'";
                                DataTable Cuss = _account.Load(sql);
                                if(Cuss.Rows.Count>0)
                                {
                                    Insert_template(_ac, Mesg);
                                    for(int i=0;i<Cuss.Rows.Count;i++)
                                    {
                                        DTO_SMS _temp = new DTO_SMS();
                                        _temp.Sender = Cuss.Rows[i][0].ToString();
                                        _temp.Message = Mesg;
                                        InsertSMS_ToDatabase(_temp, "SendSMS");
                                    }
                                    _RDSMS.Message = "Your message already send to your customer";
                                    return _RDSMS;
                                }
                                else
                                {
                                    _RDSMS.Message = "You don't have any customers in your account";
                                    return _RDSMS;
                                }
                            }
                            else
                            {
                                _RDSMS.Message = "Your Account not exits";
                                return _RDSMS;
                            }

                        }
                        else
                        {
                            _RDSMS.Message = "Invalid SMS structure. Available is FWR: Forward and CAN: Cancel";
                            return _RDSMS;
                        }



                    }
                #endregion

                #region Cancel registration
                case 2:

                    {

                        DTO_Customer _cus = new DTO_Customer();
                        DAO_Customer _DAO_Cus = new DAO_Customer();

                        _cus.Phone = _RDSMS.Sender;
                        _cus.Active = false;

                        if (_DAO_Cus.Update_state(_cus))
                        {
                            _RDSMS.Message = "Cancel registration successfully";
                            return _RDSMS; ;
                        }
                        else
                        {
                            _RDSMS.Message = "This phone number is not register";
                            return _RDSMS;
                        }

                    }
                #endregion


                default:
                    _RDSMS.Message = "Invalid SMS structure. Please recheck";
                    return _RDSMS;

            }

        }

        private void Insert_template(string account, string mess)
        {
            DTO_SMS_Template tem = new DTO_SMS_Template();
            DAO_SMS_Template _temlate = new DAO_SMS_Template();
            tem.ID = _temlate.RandomID();
            tem.Account= account;
            tem.SMS_Template = mess;
            _temlate.Insert(tem);
        }
    }
}
