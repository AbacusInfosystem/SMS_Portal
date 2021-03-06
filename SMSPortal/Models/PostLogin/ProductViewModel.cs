﻿using SMSPortal.Common;
using SMSPortalInfo;
using SMSPortalInfo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSPortalRepo;

namespace SMSPortal.Models.PostLogin
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Friendly_Message = new List<FriendlyMessage>();
            Pager = new PaginationInfo();
            Pager.IsPagingRequired = true;
            Pager.PageSize = 5;
            Product = new ProductInfo();
            Products = new List<ProductInfo>();
            ImagesList = new List<ProductImageInfo>();
            ProductImage = new ProductImageInfo();
            Filter = new ProductFilter();
            Cookies = new CookiesInfo();
            tax = new TaxInfo();
            dealer = new DealerInfo();
            state = new StateInfo();
            order = new OrdersInfo();
            Brand = new BrandInfo();
            ThirdPartyVendors = new List<VendorInfo>();
            User = new UserInfo();
        }
        public List<FriendlyMessage> Friendly_Message { get; set; }
        public PaginationInfo Pager { get; set; }
        public ProductInfo Product { get; set; }
        public List<ProductInfo> Products { get; set; }
        public ProductFilter Filter { get; set; }
        public List<BrandInfo> Brands { get; set; }
        public List<CategoryInfo> Categories { get; set; }
        public List<SubCategoryInfo> SubCategories { get; set; }
        public List<ProductImageInfo> ImagesList { get; set; }
        public HttpPostedFileBase UploadImage { get; set; }
        public HttpPostedFileBase UploadProductExcel { get; set; }
        public ProductImageInfo ProductImage { get; set; }
        public CookiesInfo Cookies { get; set; }
        public string ProductIds { get; set; }
        public TaxInfo tax { get; set; }
        public DealerInfo dealer { get; set; }
        public StateInfo state { get; set; }
        public OrdersInfo order { get; set; }
        public string ProductQuantities { get; set; }
        public BrandInfo Brand { get; set; }
        public List<VendorInfo> ThirdPartyVendors { get; set; }
        public UserInfo User { get; set; }
    }
    public class ProductFilter
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public bool Is_Active { get; set; }
    }
}