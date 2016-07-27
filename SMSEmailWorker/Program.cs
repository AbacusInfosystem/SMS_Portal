using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SMSEmailWorker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new SMSEmailWorker() 
            //};
            //ServiceBase.Run(ServicesToRun);

            try
            {
                SMSEmailWorker sv = new SMSEmailWorker();

                //sv.Send_Mail();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
