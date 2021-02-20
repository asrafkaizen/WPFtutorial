using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using simpletest.Helper_Code.Common;
using simpletest.Model.BusinessLogic.Helper_Code.Common;
using Microsoft.Win32;

namespace simpletest.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            this.txtSearch.Text = "Email";

            listuser();
        }
        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = this.txtName.Text;
                string email = this.txtEmail.Text;
                string phone = this.txtPhone.Text;
                string password = this.txtPassword.Text;
                string roleindex = this.role.SelectedIndex.ToString();
                string role = "admin";
                if (roleindex == "0")
                {
                    role = "user";
                }

                // Verification.  
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show(
                        "This field can not be empty. Please fill all fields"
                        , "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Save Info.  
                HomeBusinessLogic.SaveInfo(name, email, phone, password, role);

                // Display Message  
                MessageBox.Show("You are Successfully Registered", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                listuser();
            }
            catch (Exception ex)
            {
                Console.Write(ex);

                // Assuming data insert error is only due to duplicate email
                MessageBox.Show("The email has already been registered. Please use a different email", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnreload_click(object sender, RoutedEventArgs e)
        {
            listuser();
        }

        private void btnsearch_click(object sender, RoutedEventArgs e)
        {
            string strConn = "Data Source=DESKTOP-UJS9FKG" + "\\" + "SQLEXPRESS;Database=Assesment;User Id=acap;Password=acapacap;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            sqlConnection.Open();

            string txtemail = this.txtSearch.Text;
            string Get_Data = "SELECT id, name, email, phoneNumber, role FROM Users where Email = '" + txtemail + "'";
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = Get_Data;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            sda.Fill(ds);

            //check if email is found
            if (ds.Tables[0].Rows.Count > 0)
            {
                string name = ds.Tables[0].Rows[0]["name"].ToString();
                string email = ds.Tables[0].Rows[0]["email"].ToString();
                string phone = ds.Tables[0].Rows[0]["phoneNumber"].ToString();
                string role = ds.Tables[0].Rows[0]["role"].ToString();
                string id = ds.Tables[0].Rows[0]["id"].ToString();

                this.txtid.Text = id;
                this.txtName.Text = name;
                this.txtEmail.Text = email;
                this.txtPhone.Text = phone;
                if (role == "0")
                {
                    this.role.SelectedIndex = 0;
                }
                else
                {
                    this.role.SelectedIndex = 1;
                }
                this.btnUpdate.Visibility = Visibility.Visible;

            }
            else
            {
                //email is not found
                MessageBox.Show("Email not found", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public void RemovePlaceholder(object sender, EventArgs e)
        {
            if (this.txtSearch.Text == "Email")
            {
                this.txtSearch.Text = "";
            }
        }
        
        public void AddPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtSearch.Text))
                this.txtSearch.Text = "Email";
        }

        private void listuser()
        {
            try
            {
                string strConn = "Data Source=DESKTOP-UJS9FKG" + "\\" + "SQLEXPRESS;Database=Assesment;User Id=acap;Password=acapacap;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                sqlConnection.Open();

                string Get_Data = "SELECT id, name, email, phoneNumber, role FROM Users";
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = Get_Data;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                sda.Fill(ds);
                userlist.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch //(SqlException ex)
            {
                MessageBox.Show("db error. edit s.code to enable console write");
                //Console.WriteLine(ex.ToString());
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = this.txtid.Text;
                string name = this.txtName.Text;
                string email = this.txtEmail.Text;
                string phone = this.txtPhone.Text;
                string password = this.txtPassword.Text;
                string roleindex = this.role.SelectedIndex.ToString();
                string role = "admin";
                if (roleindex == "0")
                {
                    role = "user";
                }

                // Verification.  
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show(
                        "This field can not be empty. Please fill all fields"
                        , "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Save Info.  
                HomeBusinessLogic.UpdateInfo(id, name, email, phone, password, role);

                // Display Message  
                MessageBox.Show("Update succesful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                listuser();
            }
            catch (Exception ex)
            {
                Console.Write(ex);

                // Assuming data insert error is only due to duplicate email
                MessageBox.Show("Update error. Check logs for detail", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

   


        ////////add new functions above this line
    }
}
