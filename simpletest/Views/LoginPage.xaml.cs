using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using simpletest.Model.BusinessLogic.Helper_Code.Common;
using System.Text.RegularExpressions;

namespace simpletest.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public bool formvalid = true;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = this.txtEmail.Text;
                string password = this.txtPassword.Password;

                // Verification.  
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("This field can not be empty. Please fill all fields", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (formvalid == false)
                {
                    MessageBox.Show("Please use valid email format", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                password = HomeBusinessLogic.GetStringSha256Hash(password);

                string strConn = HomeBusinessLogic.getcon();
                SqlConnection sqlConnection = new SqlConnection(strConn);
                sqlConnection.Open();

                string Get_Data = "SELECT id, name, email, phoneNumber FROM Users where email = '"
                    + email + "' and password = '"
                    + password + "' and role = 'admin'";
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = Get_Data;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("Login successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Uri uri = new Uri("/Views/HomePage.xaml", UriKind.Relative);
                    this.NavigationService.Navigate(uri);
                }
                else
                {
                    MessageBox.Show("Login unsuccessful. Please admin email and password", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex);

                // Display Message  
                MessageBox.Show("Something went wrong, Please try again later.", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            
            if ((!String.IsNullOrEmpty(txtEmail.Text)) && (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$")))
            {
                MessageBox.Show("Please enter a valid email format", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                formvalid = false;
            }
            else
            {
                formvalid = true;
            }
        }
    }
}
