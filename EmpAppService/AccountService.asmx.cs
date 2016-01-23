using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;


namespace EmpAppService
{
    /// <summary>
    /// Summary description for AccountService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AccountService : System.Web.Services.WebService
    {
        ErrorLog log = new ErrorLog();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World its me";
        }
        [WebMethod]
        public string ValidateUserLogin(string username, string password)
        {
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ClsCon.ConnStr()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Proc_ValidateUserLogin", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dtb = new DataTable();
                    dtb.Load(dr);
                    connection.Close();
                    string output = DataTableToJSONWithJSONNet(dtb);
                    return output;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string saveRegisterData(string email,string desig,string name,string dob,string gender,string address,string country,string state,string district,string pin ,string ph,string jdate,string password)
        {
            string JSONString = string.Empty;
            SqlConnection connection = null;
            try
            {  

                using (connection = new SqlConnection(ClsCon.ConnStr()))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("Proc_save_register", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pk_int_regId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@vchar_email", SqlDbType.VarChar).Value =email;
                    cmd.Parameters.Add("@vchar_designation", SqlDbType.VarChar).Value = desig;
                    cmd.Parameters.Add("@vchar_name", SqlDbType.VarChar).Value =name;
                    cmd.Parameters.Add("@vchar_dob", SqlDbType.VarChar).Value = dob;
                    cmd.Parameters.Add("@vchar_gender", SqlDbType.VarChar).Value =gender;
                    cmd.Parameters.Add("@vchar_address", SqlDbType.VarChar).Value = address;
                    cmd.Parameters.Add("@vchar_country", SqlDbType.VarChar).Value = country;
                    cmd.Parameters.Add("@vchar_state", SqlDbType.VarChar).Value = state;
                    cmd.Parameters.Add("@vchar_district", SqlDbType.VarChar).Value =district ;
                    cmd.Parameters.Add("@int_pin", SqlDbType.VarChar).Value = pin;
                    cmd.Parameters.Add("@vchar_mobile", SqlDbType.Text).Value =ph;
                    cmd.Parameters.Add("@vchar_jdate", SqlDbType.VarChar).Value = jdate;
                    cmd.Parameters.Add("@vchar_password", SqlDbType.VarChar).Value = password;            
                    cmd.ExecuteNonQuery();
                    int userId = Convert.ToInt32(cmd.Parameters["@pk_int_regId"].Value);
                    connection.Close();                   
                    JSONString = JsonConvert.SerializeObject(userId);                   


                }
              
               
            }
               
            catch (Exception ex)
            {
                log.WriteErrorLog(ex.ToString());
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return JSONString;
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}
