using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Buisness_Mangement_System
{
    public partial class frmRegistation : Form
    {
        string con = "Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False";
        public frmRegistation()
        {
            InitializeComponent();
            
        }
        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False");
        SqlCommand cmd = new SqlCommand();


        private bool IsUsernameExists(string username)
        {
            // SQL query to check if username exists
            string query = "SELECT COUNT(*) FROM tbl_user WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Username", username);

                    // Open connection
                    connection.Open();

                    // Execute query
                    int count = (int)command.ExecuteScalar();

                    // Check if count is greater than 0 (username exists)
                    return count > 0;
                }
            }
        }



        private void btnREGISTATION_Click(object sender, EventArgs e)
        {

            // Retrieve username and password from textboxes
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validate both fields are filled
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Check if username already exists
            if (IsUsernameExists(username))
            {
                MessageBox.Show("Username already exists. Please choose a different one.");
                return;
            }


            MessageBox.Show("User registered successfully.");
        






            try
            {
                if (txtPassword.Text != txtComfimPassword.Text)
                {
                    MessageBox.Show("Password Does Not Match", "error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }

                SqlParameter p1 = new SqlParameter("@Username", SqlDbType.VarChar);
                p1.Value = txtUsername.Text.ToString().Trim();
                SqlParameter p2 = new SqlParameter("@PPassword", SqlDbType.VarChar);
                p2.Value = txtPassword.Text.ToString().Trim();

                SqlParameter p3 = new SqlParameter("@role", SqlDbType.VarChar);
                p3.Value = txtrole.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("@name", SqlDbType.VarChar);
                p4.Value = txtfullname.Text.ToUpper().Trim();



                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = "insert into tbl_user(Username, PPassword,role,name) Values(@Username,@PPassword,@role,@name)";
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Crete accouct Sucessfully");
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message,"Warring");
            }

        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmRegistation_Load(object sender, EventArgs e)
        {
            
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtComfimPassword.PasswordChar= '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtComfimPassword.PasswordChar = '•';
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtComfimPassword.Text = "";
            txtrole.Text = "";
            txtfullname.Text = " ";
            txtUsername.Focus();
        }

        private void lblBckToLoginPanel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLoginPanel loginPanel = new frmLoginPanel();
            loginPanel.ShowDialog();
         
        }
    }
}
