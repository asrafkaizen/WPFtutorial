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
using Microsoft.Office.Interop.Excel;

namespace simpletest.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page
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
                if (role == "user")
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


        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                string txtFilePath = openfile.FileName;
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(txtFilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                int rowCnt = 0;
                int colCnt = 0;
                string th = ""; //table header
                int thname = 0;
                int themail = 0;
                int thphone = 0;
                int thpassword = 99;
                int throle = 0;

                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    th = (string)(excelRange.Cells[1, colCnt] as Range).Value2;
                    if (th.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                    {
                        thname = colCnt;
                    }
                    else if (th.Equals("email", StringComparison.InvariantCultureIgnoreCase))
                    {
                        themail = colCnt;
                    }
                    else if (th.Equals("phoneNumber", StringComparison.InvariantCultureIgnoreCase))
                    {
                        thphone = colCnt;
                    }
                    else if (th.Equals("password", StringComparison.InvariantCultureIgnoreCase))
                    {
                        thpassword = colCnt;
                    }
                    else if (th.Equals("role", StringComparison.InvariantCultureIgnoreCase))
                    {
                        throle = colCnt;
                    }
                }

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    //for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++){
                    //    strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Range).Value2;
                    //}   ---------old code where excel header is too well organized

                    string cellname = Convert.ToString((excelRange.Cells[rowCnt, thname] as Range).Value2);
                    string cellemail = Convert.ToString((excelRange.Cells[rowCnt, themail] as Range).Value2);
                    string cellphone = Convert.ToString((excelRange.Cells[rowCnt, thphone] as Range).Value2);
                    string cellpassword = Convert.ToString((excelRange.Cells[rowCnt, thpassword] as Range).Value2);
                    string cellrole = Convert.ToString((excelRange.Cells[rowCnt, throle] as Range).Value2);
                    try
                    {
                        HomeBusinessLogic.SaveInfo(cellname, cellemail, cellphone, cellpassword, cellrole);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        // Assuming data insert error is only due to duplicate email
                        MessageBox.Show("Data row:" + rowCnt + ", email: " + cellemail + " is skipped. Email already exists", "Fail", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Import excel: Browsefile cancelled", "Fail", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            listuser();
            MessageBox.Show("Data import successful", "Succesful", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        ////////add new functions above this line
    }
}
