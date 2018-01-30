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
    public partial class sendsms : Form
    {
        Thread thrListener = null;
        public sendsms()
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

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                Listport.Add(s);
            }
            cbxCOM.DataSource = Listport;

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
                    while (_message != "OK")
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

                }
            }

        }

    
        private void Services_FormClosing(object sender, FormClosingEventArgs e)
        {
            thrListener.Abort();
            Application.Exit();
        }

        BUS_Schedule _schedule = new BUS_Schedule();
        private DataTable checkSchedule()
        {
            string sql = "select ID,groupID from Schedule where Schedule.Date =  CONVERT(date,SYSDATETIME()) and DATEPART(hh,Schedule.Time)=DATEPART(hh,GETDATE()) and DATEPART(MINUTE,Schedule.Time) Between DATEPART(MINUTE,GETDATE()) and DATEPART(MINUTE,GETDATE())+1 and Schedule.Active=1";

            DataTable dt = new DataTable();
            dt = _schedule.Load(sql);
            return dt;
        }
        private void sql_all_customer(string ID)
        {
            string sql = "select cus.Phone, sc.SMS_info from Customer cus, Account ac, Schedule sc where cus.AccountID=ac.ID and cus.active=1 and ac.id= sc.AccountID and sc.ID='" + ID + "'";
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
        private void sql_all_customer(string ID, string group_id)
        {
            string sql = "select cus.Phone, sc.SMS_info from Customer cus, Account ac, Schedule sc where cus.AccountID=ac.ID and cus.active=1 and ac.id= sc.AccountID and sc.ID='" + ID + "' and sc.GroupID ='" + group_id + "'";
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            LoadPort();
            if (Listport.Count > 0)
            {
                thrListener.Start();
                _BUSSMS.sendMsg(port, "0468607682", "Testing");

            }
            else
            {
                statusBar1.Text = "Can not find any device";
            }
        }

        private void sendsms_FormClosing(object sender, FormClosingEventArgs e)
        {
            thrListener.Abort();
            Application.Exit();
        }
    }
}
