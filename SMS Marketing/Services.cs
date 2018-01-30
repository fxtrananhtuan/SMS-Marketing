using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DAO;
using System.IO.Ports;
using System.Threading;

namespace SMS_Marketing
{
    public partial class Services : Form
    {
        Thread thrListener = null;
        public Services()
        {
            InitializeComponent();
            thrListener = new Thread(StartListening);
            cGlobalvariable._port = new SerialPort();
        }
        public BUS_SMS _BUSSMS = new BUS_SMS();
        SerialPort port = new SerialPort();
        public DTO_SMS _sms = new DTO_SMS();
        public DTO_SMS_Collection _smsCollection = new DTO_SMS_Collection();
        
        List<string> Listport = new List<string>();

        private void btnOK_Click(object sender, EventArgs e)
        {
            dgvWaitingSMS.AutoGenerateColumns = false;
            dgvRequest.AutoGenerateColumns = false;
            dgvSendSMS.AutoGenerateColumns = false;
            LoadPort();
            if (Listport.Count > 0)
            {
                 thrListener.Start();

            }
            else
            {
                statusBar1.Text = "Can not find any device";
            }
        }
        private void LoadPort()
        {
            port = cGlobalvariable._port;
            try
            {

                //Open communication port 
                foreach (string p in Listport)
                {
                    if (p == cbxCOM.Text)
                    {
                        port = _BUSSMS.OpenPort(p, 9600, 8, 300, 300);
                        if (port == null)
                        {
                            this.statusBar1.Text = "Invalid port settings";
                            btnOK.Enabled = true;
                           

                        }
                        else
                        {
                            this.statusBar1.Text = "Successfully connect the device";

                            lblConnectionStatus.Text = "Connected ";
                            btnOK.Enabled = false;
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                cGlobalFunction.ErrorLog(ex.Message);
            }
        }

        private void Services_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                Listport.Add(s);
            }
            cbxCOM.DataSource = Listport;
        }
        public delegate void ChangesStatusBar(StatusBar st, string text);
        private void ChgLabelText(StatusBar st, string text)
        {
            st.Text = text;
        }
        private void StartListening()
        {
            while (true)//Keep listening
            {

                Insert_to_waitingSMS();
                //Get new SMS
                Invoke(new ChangesStatusBar(ChgLabelText), statusBar1, "Getting SMS");
                ReadSMS();
                

                //Refresh Request Data Grid View
                Invoke(new RefreshReceivedDgv(RfshReceivedDgv), dgvRequest);

                //Process recevied SMS
                if (_BUSSMS.LoadSMS("ReceivedSMS").Rows.Count > 0 || _BUSSMS.LoadSMS("WaittingSMS").Rows.Count > 0)
                {
                    Invoke(new ChangesStatusBar(ChgLabelText), statusBar1, "Processing recevied SMS");
                    ProcessReceivedSMS();
                }


                //Send waiting SMS
                if (_BUSSMS.LoadSMS("SendSMS").Rows.Count > 0)
                {
                    Invoke(new ChangesStatusBar(ChgLabelText), statusBar1, "Processing Send SMS");
                    ProcessSendSMS();
                }


                //Listen again
                Invoke(new ChangesStatusBar(ChgLabelText), statusBar1, "Listening");
                Thread.Sleep(5000);
            }
        }
        private void ReadSMS()
        {
            try
            {
                //count SMS 
                int uCountSMS = _BUSSMS.CountSMSmessages(port);
                if (uCountSMS > 0)
                {

                    #region Command
                    string strCommand = "AT+CMGL=\"ALL\"";

                    #endregion

                    // If SMS exist then read SMS
                    #region Read SMS
                    //.............................................. Read all SMS ....................................................
                    _smsCollection = _BUSSMS.ReadSMS(port, strCommand);
                    foreach (DTO_SMS msg in _smsCollection)
                    {
                        _BUSSMS.InsetSMS_toDatabase(msg, "ReceivedSMS");
                    }
                    strCommand = "AT+CMGD=1,3";
                    _BUSSMS.DeleteMsg(port, strCommand);
                    dgvRequest.DataSource = _BUSSMS.LoadSMS("ReceivedSMS");
                    #endregion

                }
                else
                {
                    //MessageBox.Show("There is no message in SIM");
                    this.statusBar1.Text = "There is no message in SIM";

                }
            }
            catch (Exception ex)
            {
                cGlobalFunction.ErrorLog(ex.Message);
            }
            Invoke(new RefreshReceivedDgv(RfshReceivedDgv), dgvRequest);
        }

        public delegate void RefreshReceivedDgv(DataGridView dgvRequest);
        private void RfshReceivedDgv(DataGridView dgvRequest)
        {
            dgvRequest.DataSource = null;
            dgvRequest.DataSource = _BUSSMS.LoadSMS("ReceivedSMS");
        }
        public delegate void RefreshWaitingDgv(DataGridView dgvWaiting);
        private void RfshWaitingDgv(DataGridView dgvWaiting)
        {
            dgvWaiting.DataSource = null;
            dgvWaiting.DataSource = _BUSSMS.LoadSMS("WaittingSMS");
        }

        public delegate void RefreshSentDgv(DataGridView dgvSent);
        private void RfshSentDgv(DataGridView dgvSent)
        {
            dgvSent.DataSource = null;
            dgvSent.DataSource = _BUSSMS.LoadSMS("SendSMS");
        }

        private void ProcessSendSMS()
        {
            DataTable temptb_waiting = _BUSSMS.LoadSMS("SendSMS");
            DTO_SMS TEMP = new DTO_SMS();
            Invoke(new RefreshReceivedDgv(RfshSentDgv), dgvSendSMS);
            Thread.Sleep(2000);
            if (temptb_waiting.Rows.Count > 0)
            {
                for (int i = 0; i < temptb_waiting.Rows.Count; i++)
                {


                    TEMP.Index = temptb_waiting.Rows[i]["ID"].ToString();
                    TEMP.Message = temptb_waiting.Rows[i]["SMSmessage"].ToString();
                    TEMP.Sender = temptb_waiting.Rows[i]["SMSsender"].ToString();
                    TEMP.Sent = temptb_waiting.Rows[i]["_sent"].ToString();
                    TEMP.Status = temptb_waiting.Rows[i]["SMSstatus"].ToString();
                    TEMP.Alphabet = temptb_waiting.Rows[i]["SMSalphabet"].ToString();
                    //send sms
                    //
                    string _message = "";
                    while (_message!="OK")
                    {
                        string _ms = _BUSSMS.ExecCommand(port, "AT+CSMP=17,167,0,16", 300, "NOT");
                        Thread.Sleep(200);
                        if (_ms == "AT+CSMP=17,167,0,16\r\r\nOK\r\n")
                        {
                            _message = "OK";
                           
                            
                        }
                    }
                   
                    _BUSSMS.sendMsg(port, TEMP.Sender, TEMP.Message);

                    _BUSSMS.DeleteSMS_toDatabase(TEMP, "SendSMS");
                    Invoke(new RefreshReceivedDgv(RfshSentDgv), dgvSendSMS);

                }
            }


            this.Invoke(new RefreshReceivedDgv(RfshWaitingDgv), dgvWaitingSMS);
        }

        private void ProcessReceivedSMS()
        {
            DataTable temptb = _BUSSMS.LoadSMS("ReceivedSMS");
            DataTable temptb_waiting = _BUSSMS.LoadSMS("WaittingSMS");
            DTO_SMS TEMP = new DTO_SMS();
            if (temptb_waiting.Rows.Count > 0)
            {
                for (int i = 0; i < temptb_waiting.Rows.Count; i++)
                {


                    TEMP.Index = temptb_waiting.Rows[i]["ID"].ToString();
                    TEMP.Message = temptb_waiting.Rows[i]["SMSmessage"].ToString();
                    TEMP.Sender = temptb_waiting.Rows[i]["SMSsender"].ToString();
                    TEMP.Sent = temptb_waiting.Rows[i]["_sent"].ToString();
                    TEMP.Status = temptb_waiting.Rows[i]["SMSstatus"].ToString();
                    TEMP.Alphabet = temptb_waiting.Rows[i]["SMSalphabet"].ToString();



                    _BUSSMS.DoAnalyse(TEMP);



                    _BUSSMS.InsetSMS_toDatabase(TEMP, "SendSMS");
                    Invoke(new RefreshReceivedDgv(RfshSentDgv), dgvSendSMS);
                    _BUSSMS.DeleteSMS_toDatabase(TEMP, "WaittingSMS");
                    Invoke(new RefreshReceivedDgv(RfshWaitingDgv), dgvWaitingSMS);
                }
            }
            if (temptb.Rows.Count > 0)
            {
                Thread.Sleep(1000);
                this.Invoke(new RefreshReceivedDgv(RfshReceivedDgv), dgvRequest);
                for (int i = 0; i < temptb.Rows.Count; i++)
                {


                    TEMP.Index = temptb.Rows[i]["ID"].ToString();
                    TEMP.Message = temptb.Rows[i]["SMSmessage"].ToString();
                    TEMP.Sender = temptb.Rows[i]["SMSsender"].ToString();
                    TEMP.Sent = temptb.Rows[i]["_sent"].ToString();
                    TEMP.Status = temptb.Rows[i]["SMSstatus"].ToString();
                    TEMP.Alphabet = temptb.Rows[i]["SMSalphabet"].ToString();
                    _BUSSMS.InsetSMS_toDatabase(TEMP, "WaittingSMS");
                    Invoke(new RefreshReceivedDgv(RfshWaitingDgv), dgvWaitingSMS);
                    _BUSSMS.DeleteSMS_toDatabase(TEMP, "ReceivedSMS");
                    this.Invoke(new RefreshReceivedDgv(RfshReceivedDgv), dgvRequest);
                }
            }

        }

        private void Services_FormClosing(object sender, FormClosingEventArgs e)
        {
            thrListener.Abort();
            Application.Exit();
        }

        BUS_Schedule _schedule = new BUS_Schedule();
        private DataTable checkSchedule ()
        {
            string sql = "select ID,groupID from Schedule where Schedule.Date =  CONVERT(date,SYSDATETIME()) and DATEPART(hh,Schedule.Time)=DATEPART(hh,GETDATE()) and DATEPART(MINUTE,Schedule.Time) Between DATEPART(MINUTE,GETDATE()) and DATEPART(MINUTE,GETDATE())+1 and Schedule.Active=1";

            DataTable dt = new DataTable();
            dt = _schedule.Load(sql);
            return dt;
        }
        private void Insert_to_waitingSMS ()
        {
            DataTable tb = new DataTable();
            tb = checkSchedule();
            if (tb.Rows.Count > 0)
            {
                DTO_Schedule _sch = new DTO_Schedule();
                _sch.Active = false;
                _sch.ID = tb.Rows[0][0].ToString();
                _schedule.Update_state(_sch);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    string ID = tb.Rows[i][0].ToString();
                    string GroupID = tb.Rows[i][1].ToString();
                    if(GroupID=="")
                    {
                        sql_all_customer(ID);
                    }
                    else
                    {
                        sql_all_customer(ID, GroupID);
                    }

                }
            }
        }
        private void sql_all_customer(string ID)
        {
            string sql = "select cus.Phone, sc.SMS_info from Customer cus, Account ac, Schedule sc where cus.AccountID=ac.ID and cus.active=1 and ac.id= sc.AccountID and sc.ID='" + ID+"'";
            DataTable tb = _schedule.Load(sql);
            
            if(tb.Rows.Count>0)
            {
                for (int i=0;i<tb.Rows.Count;i++)
                {
                    DTO_SMS _sms = new DTO_SMS();
                    _sms.Sender = tb.Rows[i][0].ToString();
                    _sms.Message= tb.Rows[i][1].ToString();
                    _BUSSMS.InsetSMS_toDatabase(_sms, "SendSMS");
                    
                }
            }
        }
        private void sql_all_customer(string ID,string group_id)
        {
            string sql = "select cus.Phone, sc.SMS_info from Customer cus, Account ac, Schedule sc where cus.AccountID=ac.ID and cus.active=1 and ac.id= sc.AccountID and sc.ID='" + ID + "' and sc.GroupID ='"+group_id+"'";
            DataTable tb = _schedule.Load(sql);
            if (tb.Rows.Count > 0)
            {
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    DTO_SMS _sms = new DTO_SMS();
                    
                    _sms.Sender = tb.Rows[i][0].ToString();
                    _sms.Message = tb.Rows[i][1].ToString();
                    _BUSSMS.InsetSMS_toDatabase(_sms, "SendSMS");

                }
            }
        }

       
    }
}
