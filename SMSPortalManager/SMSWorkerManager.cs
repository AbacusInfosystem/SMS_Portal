using SMSPortalInfo;
using SMSPortalInfo.Common;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalManager
{
    public class SMSWorkerManager
    {
        SMSWorkerRepo _smsWorker;

        public SMSWorkerManager()
        {
            _smsWorker = new SMSWorkerRepo();
        }

        public List<Email_Data> Get_Email_Data(string request_Type)
        {
            return _smsWorker.Get_Email_Data(request_Type);
        }

        public void Update_Email_Status(int Request_Id, string Request_Type)
        {
            _smsWorker.Update_Email_Status(Request_Id, Request_Type);
        }
    }
}
