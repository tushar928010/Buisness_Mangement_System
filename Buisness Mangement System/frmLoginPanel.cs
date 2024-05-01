using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Buisness_Mangement_System
{
    public partial class frmLoginPanel : Form
    {
        public frmLoginPanel()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtpassword.Text = "";
            txtUsername.Focus();
        }

        private void btnCreate_new_acccount_Click_1(object sender, EventArgs e)
        {
            frmRegistation registation = new frmRegistation();
            registation.Show();
            this.Hide();
        }

        private void chkShow_Password_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkShow_Password.Checked)
            {
                txtpassword.PasswordChar = '\0';

            }
            else
            {
                txtpassword.PasswordChar = '•';

            }

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {

       
         

            conn.Open();

            string userName = txtUsername.Text.ToString().Trim();
            string password = txtpassword.Text.ToString().Trim();
            string login = "select  * from tbl_user where Username =  '" + userName + "' and PPassword  = '" + password + "'";


            cmd = new SqlCommand(login, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true)
            {
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                MessageBox.Show(" Access Greanted ");
            }
            else
            {
                MessageBox.Show("Invaild Username Or Password Please try Again", "Login Failed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtpassword.Text = "";
                txtUsername.Focus();
               
            }
            conn.Close();
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtpassword.Text = "";
            txtUsername.Focus();
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmLoginPanel_Load(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    

