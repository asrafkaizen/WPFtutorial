using simpletest.Helper_Code.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace simpletest.Model.BusinessLogic.Helper_Code.Common
{
    class HomeBusinessLogic
    {
        public static void SaveInfo(string name, string email, string phone, string password, string role)
        {
            
            try
            {
                password = GetStringSha256Hash(password);

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
                password = GetStringSha256Hash(password);
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

        public static void UpdateInfo(string id, string name, string email, string phone, string role)
        {
            try
            {
                string query = "update USERS set name='" + name + "', email='" + email + "', phoneNumber='"
                    + phone + "', role='" + role + "'where id='" + id + "'";

                // Execute.  
                DAL.executeQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string getcon()
        {
            string strConn = ConfigurationManager.AppSettings["constring2"];
            return strConn;
        }

        internal static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}