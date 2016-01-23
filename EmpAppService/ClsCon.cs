using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EmpAppService
{
    public class ClsCon
    {
        public static string ConnStr()
        {
            return ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        }
    }
}