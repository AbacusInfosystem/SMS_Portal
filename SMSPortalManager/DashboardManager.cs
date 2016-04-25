using SMSPortalHelper.Logging;
using SMSPortalInfo;
using SMSPortalRepo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSPortalInfo;
using SMSPortalInfo.Common;

namespace SMSPortalManager
{
    public class DashboardManager
    {
        public DashboardRepo _dashboardRepo;

        public DashboardManager()
        {
            _dashboardRepo = new DashboardRepo();
        }
    }
}
