﻿using System;
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
        Elite = 1,
        Volumn_Based = 2,
        Beyond_Borders = 3
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

        //Brand
        Brand_Create,
        Brand_Search,
        Brand_Edit
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
    public enum PurchaseOrderStatus
    { 
        Ordered =1,
        Patially_Received=2,
        Received=3
    }

    public enum Roles
    {
        Admin = 1,
        Brand = 2,
        Dealer = 3,
        Vendor = 4
    }

    public enum RolesIds : int
    {
        Admin = 1,
        Brand = 2,
        Dealer = 3,
        Vendor = 4,
        ThirdPartyVendor=5
    }
}
