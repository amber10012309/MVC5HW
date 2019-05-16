using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Cust.DataTypeAttributes 
{
    public class 手機驗證Attribute : DataTypeAttribute
    {
        string pattern;
        public 手機驗證Attribute() : base(DataType.Text)
        {
            pattern = @"\d{4}-\d{6}";
            ErrorMessage = "手機格式錯誤( e.g. 09XX-XXXXXX )";
        }
        public override bool IsValid(object value)
        {

            if (value == null)
            {
                return true;
            }

            string str = (string)value;


                if (!Regex.IsMatch(str, pattern))
                {
                    return false;
                }

            return true;
        }
    }
}