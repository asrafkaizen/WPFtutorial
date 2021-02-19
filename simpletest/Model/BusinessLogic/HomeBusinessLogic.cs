using simpletest.Helper_Code.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace simpletest.Model.BusinessLogic.Helper_Code.Common
{
    class HomeBusinessLogic
    {
        public static void SaveInfo(string fullname)
        {
            try
            {
                // Query.  
                string query = "INSERT INTO [Register] ([fullname])" +
                                " Values ('" + fullname + "')";

                // Execute.  
                DAL.executeQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveInfo(string name, string email, string phone, string password, string role)
        {
            try
            {
                // Query.  
                string query = "INSERT INTO [Users] ([name], [email], [phoneNumber], [password], [role])" +
                                " Values ('" + name + "', '" + email + "','" + phone + "','" + password + "','" + role + "')";

                // Execute.  
                DAL.executeQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateInfo(string id, string name, string email, string phone, string password, string role)
        {
            try
            {
                // Query.  
                string query = "update USERS set name='" + name + "', email='" + email + "', phoneNumber='" 
                    + phone + "', password='" + password + "', role='" + role + "'where id='" + id + "'";

                // Execute.  
                DAL.executeQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}