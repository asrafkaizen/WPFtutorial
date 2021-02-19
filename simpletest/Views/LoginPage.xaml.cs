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

namespace simpletest.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = this.txtEmail.Text;
                string password = this.txtPassword.Text;

                // Verification.  
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("This field can not be empty. Please fill all fields", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string strConn = "Data Source=DESKTOP-UJS9FKG" + "\\" + "SQLEXPRESS;Database=Assesment;User Id=acap;Password=acapacap;";
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
                    MessageBox.Show("Login unsuccessful. Please enter correct email and password", "Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex);

                // Display Message  
                MessageBox.Show("Something goes wrong, Please try again later.", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
