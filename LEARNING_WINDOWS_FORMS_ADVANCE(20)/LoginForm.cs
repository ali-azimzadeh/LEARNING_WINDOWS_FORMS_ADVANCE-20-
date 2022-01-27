using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private MyDataSetTableAdapters.UsersTableAdapter _usersTable = null;

        public MyDataSetTableAdapters.UsersTableAdapter UsersTable
        {
            get
            {
                if (_usersTable == null)
                {
                    _usersTable =
                        new MyDataSetTableAdapters.UsersTableAdapter();
                }

                return (_usersTable);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Application.CurrentInputLanguage =
                         InputLanguage.FromCulture(
                             System.Globalization.CultureInfo
                             .CreateSpecificCulture("en-US"));

            userNameTextBox.Focus();
        }

        private void userNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                passwordTextBox.Focus();
            }
        }

        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userNameTextBox.Text) == false)
            {
                passwordTextBox.Enabled = true;
            }
            else
            {
                passwordTextBox.Enabled = false;
            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text) == false)
            {
                loginButton.Enabled = true;
            }
            else
            {
                loginButton.Enabled = false;
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            // استفاده می کنید باید در این قسمت  app.config در برنامه های ویندوزی اگر از 
            // کنید encrypt را  connection string بخشهای مهم مانند 

            if(string.IsNullOrWhiteSpace(userNameTextBox.Text)==false 
                && string.IsNullOrWhiteSpace(passwordTextBox.Text) == false)
            {
                MyDataSet.UsersRow userRow =
                    Login(
                        userName: userNameTextBox.Text,
                        password: passwordTextBox.Text
                        );

                if(userRow != null)
                {
                    //در صورت داشتن سطوح دسترسی باید در این قسمت آنها را 
                    //برای کاربری که می خواهد وارد برنامه شود تنظیم کنید
                    
                    //**********************************************************
                    //دقت کنید که این داده ها در واقع باید از بانک اطلاعاتی
                    //خوانده شود اینجا صرفا جهت آموزش بصورت دستی ایجاد شده
                    //**********************************************************
                    MenuStatus menuStatus = new MenuStatus(1, true);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(2, false);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(3, true);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(11, true);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(12, true);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(13, false);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(14, true);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(121, true);

                    Temp.MenuList.Add(menuStatus);

                    menuStatus = new MenuStatus(122, false);

                    Temp.MenuList.Add(menuStatus);


                    //اگر شیفت کاری برای کاربران تعریف کرده اید باید اینجا
                    //کنترلهای لازم را انجام دهید و در صورتی که کاربر مجاز
                    //به ورود نباشد نباید اجازه ی ورود به برنامه را بدهید

                    //و اگر همه چیز اوکی بود آنگاه این فرم را می بندیم
                    this.Close();
                }
            }
        }

        public MyDataSet.UsersRow Login(string userName, string password)
        {
            try
            {
                MyDataSet.UsersDataTable usersRows =
                    UsersTable.GetDataByUserName(Username: userName);

                if (usersRows.Count == 0)
                {
                    // شناسه کاربری و/يا کلمه عبور را به درستی وارد نکرده ايد
                    throw (new ApplicationException("UserName or Password is incorrect"));
                }

                MyDataSet.UsersRow usersRow = usersRows[0];

                string hashOfPassword = Hash.GetSha1(password);

                if (string.Compare(usersRow.Password, hashOfPassword, false) != 0)
                {
                    // شناسه کاربری و/يا کلمه عبور را به درستی وارد نکرده ايد
                    throw (new ApplicationException("UserName or Password is incorrect"));
                }

                if (usersRow.IsActive == false)
                {
                    // دسترسی شما تا اطلاع ثانوی به اين برنامه مسدود می باشد، لطفا با مدير سيستم تماس حاصل فرماييد
                    throw (new ApplicationException("You dont permission to use this application"));
                }

                return (usersRow);
            }
            catch (ApplicationException ex)
            {
                //throw (ex);
                MessageBox.Show(ex.Message);

                InitializeForm();

                return null;
            }
            catch (System.Exception ex)
            {
                // Log(ex)
                return null;
            }
            finally
            {

            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                loginButton.Focus();
            }
        }

        private void InitializeForm()
        {
            userNameTextBox.ResetText();

            passwordTextBox.ResetText();

            loginButton.Enabled = false;
        }
    }
}
