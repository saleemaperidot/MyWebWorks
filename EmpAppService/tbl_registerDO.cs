using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpAppService
{
    public class tbl_registerDO
    {
        public int pk_int_regId { get; set; }
        public string vchar_email { get; set; }
        public string vchar_designation { get; set; }
        public string vchar_name { get; set; }
        public string vchar_dob { get; set; }
        public string vchar_gender { get; set; }
        public string vchar_address { get; set; }
        public string vchar_country { get; set; }
        public string vchar_state { get; set; }
        public string vchar_district { get; set; }
        public string int_pin { get; set; }
        public string vchar_mobile { get; set; }
        public string vchar_jdate { get; set; }
        public string bool_status { get; set; }
        public string password { get; set; }


        public tbl_registerDO()
        {
        }
    }
}