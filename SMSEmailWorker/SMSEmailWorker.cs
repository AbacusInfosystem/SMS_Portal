using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using SMSPortalHelper.Logging;
using SMSPortalInfo;
using System.Data.SqlClient;
using SMSPortalInfo.Common;
using SMSPortalHelper;
using System.Net.Mail;
using SMSPortalManager;

namespace SMSEmailWorker
{
    partial class SMSEmailWorker : ServiceBase
    {

        private Timer schedulertimer;

         SQLHelper _sqlRepo = null;

         SMSWorkerManager _smsManager;

        public SMSEmailWorker()
        {
            _sqlRepo = new SQLHelper();

            _smsManager = new SMSWorkerManager();

            InitializeComponent();

            schedulertimer = new Timer();

            schedulertimer.Enabled = true;

            int ServiceTimerInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["ServiceTimerInMinutes"]);

            long ServiceTimerInMiliSeconds = ServiceTimerInMinutes * 60 * 1000;

            schedulertimer.Interval = Convert.ToDouble(ServiceTimerInMiliSeconds);

            schedulertimer.Elapsed += this.Ready;
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        public void Ready(object sender, ElapsedEventArgs e)
        {
            Logger.Debug("Inside Ready");

            schedulertimer.Enabled = false;

            Send_Emails();

            schedulertimer.Enabled = true;

            Logger.Debug("Ready Ends");
        }

        public void Send_Emails()
        {
            List<Email_Data> email_Data_List = new List<Email_Data>();

            try
            {
                Logger.Debug("Inside send escalation notification");

                // For send Sales Order Emails

                Logger.Debug("Start to Get Email Data for Sales Order Emails");
                email_Data_List = _smsManager.Get_Email_Data("Sales Order");
                Logger.Debug("End to Get Email Data for Sales Order Emails");

                Logger.Debug("Start to Sent Email for Sales Order");
                if(email_Data_List!=null && email_Data_List.Count()>0)
                {
                    foreach (var item in email_Data_List)
                    {
                        Sent_Email(item.Email_Subject,item.Email_Body,item.Email_Id);

                        _smsManager.Update_Email_Status(item.Request_Id, item.Request_Type);
                    }                    
                }
                Logger.Debug("End to Sent Email for Sales Order");

                // end

                // For send Dealer Invoice Emails

                Logger.Debug("Start to Get Email Data for Dealer Invoice Emails");
                email_Data_List = _smsManager.Get_Email_Data("Dealer Invoice");
                Logger.Debug("Start to Get Email Data for Dealer Invoice Emails");

                Logger.Debug("Start to Sent Email for Dealer Invoice");
                if (email_Data_List != null && email_Data_List.Count() > 0)
                {
                    foreach (var item in email_Data_List)
                    {
                        Sent_Email(item.Email_Subject, item.Email_Body, item.Email_Id);

                        _smsManager.Update_Email_Status(item.Request_Id, item.Request_Type);
                    }
                }
                Logger.Debug("Start to Sent Email for Dealer Invoice");

                // end

                // For send Brand Invoice Emails

                Logger.Debug("Start to Get Email Data for Brand Invoice Emails");
                email_Data_List = _smsManager.Get_Email_Data("Brand Invoice");
                Logger.Debug("Start to Get Email Data for Brand Invoice Emails");

                Logger.Debug("Start to Sent Email for Brand Invoice");
                if (email_Data_List != null && email_Data_List.Count() > 0)
                {
                    foreach (var item in email_Data_List)
                    {
                        Sent_Email(item.Email_Subject, item.Email_Body, item.Email_Id);

                        _smsManager.Update_Email_Status(item.Request_Id, item.Request_Type);
                    }
                }
                Logger.Debug("Start to Sent Email for Brand Invoice");


                // end

                // For send Vendor Invoice Emails

                Logger.Debug("Start to Get Email Data for Vendor Invoice Emails");
                email_Data_List = _smsManager.Get_Email_Data("Vendor Invoice");
                Logger.Debug("Start to Get Email Data for Vendor Invoice Emails");

                Logger.Debug("Start to Sent Email for Vendor Invoice");
                if (email_Data_List != null && email_Data_List.Count() > 0)
                {
                    foreach (var item in email_Data_List)
                    {
                        Sent_Email(item.Email_Subject, item.Email_Body, item.Email_Id);

                        _smsManager.Update_Email_Status(item.Request_Id, item.Request_Type);
                    }
                }
                Logger.Debug("Start to Sent Email for Vendor Invoice");


                // end



            }
            catch (Exception ex)
            {
                Logger.Error("" + ex.InnerException);
            }

        }

        public void Sent_Email(string subject,string body,string email)
        {
            Logger.Debug("Start to send email");

            MailAddress fromMail = new MailAddress(ConfigurationManager.AppSettings["fromMailAddress"].ToString(), ConfigurationManager.AppSettings["fromMailName"].ToString());

            MailMessage message = new MailMessage();

            message.From = fromMail;

            message.Subject = subject;

            message.IsBodyHtml = true;

            message.Body = body;

            MailAddress To = new MailAddress(email);

            message.To.Add(To);

            SmtpClient client = new SmtpClient();

            client.Send(message);

            Logger.Debug("End to sent email");
        }

    }
}
