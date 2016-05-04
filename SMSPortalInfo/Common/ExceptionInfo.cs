using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPortalRepo.Common
{
    public class ExceptionInfo
    {
        public string FileName { get; set; }
        public DateTime? UploadedDate { get; set; }
        public int RowNo { get; set; }
        public string Columns { get; set; }
        public string ErrorMessage { get; set; }
        public string TableName { get; set; }
    }
    public enum ExceptionEnum 
    {
        Invalid_Master_Name,
        Invalid_Table_Name,
        Invalid_Frame_Name,
        Invalid_Field_Name,
        Invalid_Value
    }
}
