using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalInfo.Common
{
	public enum MessageType
	{
		Information = 1,
		Error = 2,
		Success = 3,
		Warning = 4,
	}

	public enum RefType
	{

	}

    public enum BrandCategory
    {
        Elite=1,        
        Volumn_Based=2,
        Beyond_Borders=3
    }


    public enum GenderType
    {
        Male = 1,
        Female = 2,
    }

    public enum AppFunction
    {
        // Dashboard
        Token,
    }

    public enum TransactionType
    {
        Cheque = 1,
        NEFT = 2,
        Credit_Debit = 3
    }

    public enum OrderStatus
    {
        Order_Received = 1,
        Order_Confirmed = 2,
        Order_Dispatched = 3,
        Order_Delivered = 4
    }
}
