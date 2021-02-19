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

            try {
                string strConn = "Data Source=DESKTOP-UJS9FKG" + "\\" + "SQLEXPRESS;Database=Assesment;User Id=acap;Password=acapacap;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                sqlConnection.Open();

                string Get_Data = "SELECT id, name, email, phoneNumber FROM Users";
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
        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = this.txtName.Text;
                string email = this.txtEmail.Text;
                string phone = this.txtPhone.Text;
                string password = this.txtPassword.Text;

                // Verification.  
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("This field can not be empty. Please fill all fields", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Save Info.  
                HomeBusinessLogic.SaveInfo(name, email, phone, password);

                // Display Message  
                MessageBox.Show("You are Successfully! Registered", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.Write(ex);

                // Display Message  
                MessageBox.Show("Something goes wrong, Please try again later.", "Fail", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnreload_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strConn = "Data Source=DESKTOP-UJS9FKG" + "\\" + "SQLEXPRESS;Database=Assesment;User Id=acap;Password=acapacap;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                sqlConnection.Open();

                string Get_Data = "SELECT id, name, email, phoneNumber FROM Users";
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

        private void btnsearch_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strConn = "Data Source=DESKTOP-UJS9FKG" + "\\" + "SQLEXPRESS;Database=Assesment;User Id=acap;Password=acapacap;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                sqlConnection.Open();

                string email = this.txtSearch.Text;
                string Get_Data = "SELECT id, name, email, phoneNumber FROM Users WHERE email like '%" + email + "%'";
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


    }
}
