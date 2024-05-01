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
    public partial class frmaddCategory : Form
    {
        string con = "Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False";

        bool sidebarExpand;
        public frmaddCategory()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False");
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter sda;

        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            sda = new SqlDataAdapter(" Select * from tbl_Category", conn);
            sda.Fill(ds);
            dgvCategory.DataSource = ds.Tables[0];
        }
        private bool IsUsernameExists(string Category_Name)
        {
            // SQL query to check if username exists
            string query = "SELECT COUNT(*) FROM tbl_Category WHERE Category_Name= @Category_Name";

            using (SqlConnection connection = new SqlConnection(con))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Category_Name", Category_Name);

                    // Open connection
                    connection.Open();

                    // Execute query
                    int count = (int)command.ExecuteScalar();

                    // Check if count is greater than 0 (username exists)
                    return count > 0;
                }
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            // Retrieve username and password from textboxes
            string Category_Name = txtaddCategory.Text.Trim();


            // Check if username already exists
            if (IsUsernameExists(Category_Name))
            {
                MessageBox.Show(" Category already exists. Please choose a different one.");
                return;
            }



            try
            {

                SqlParameter p1 = new SqlParameter("@Date", SqlDbType.Date);
                p1.Value = Convert.ToDateTime(dtpdateCategory.Value.ToShortDateString());

                SqlParameter p2 = new SqlParameter("@Category_Name", SqlDbType.VarChar);
                p2.Value = txtaddCategory.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Brand", SqlDbType.VarChar);
                p3.Value = txtBrand.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
                p4.Value = CboxSupplier.Text.ToUpper().Trim();

                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);

                //step 6 
                cmd.Connection = conn;

                // step 7 
                cmd.CommandText = "insert into tbl_Category(Date,Category_Name,Brand,Supplier_Name) Values (@Date,@Category_Name,@Brand,@Supplier_Name)";

                //step 8

                conn.Open();

                //step 9
                cmd.ExecuteNonQuery();


                conn.Close();
                MessageBox.Show("Add successfully....");
                LoadGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void frmaddCategoriescs_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmEntryOfStock form1 = new frmEntryOfStock();
            form1.ShowDialog();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_Add_stock_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock frm = new frmEntryOfStock();
            frm.ShowDialog();
        }

        private void New_Transaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing = new frmNewTransaction();
            billing.Show();
        }

        private void btnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier frmSupplier = new frmSupplier();
            frmSupplier.ShowDialog();
        }

        private void btnAdd_Category_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmaddCategoriescs = new frmProducts();
            frmaddCategoriescs.ShowDialog();
        }

        private void UserSetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
        }

        private void BtnCLose_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
        public int ID;
        private void DgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString());

            dtpdateCategory.Value = Convert.ToDateTime(dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString());

            txtaddCategory.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();

            txtBrand.Text = dgvCategory.Rows[e.RowIndex].Cells[3].Value.ToString();

            CboxSupplier.Text = dgvCategory.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                SqlParameter p1 = new SqlParameter("@Date", SqlDbType.Date);
                p1.Value = Convert.ToDateTime(dtpdateCategory.Value.ToShortDateString());

                SqlParameter p2 = new SqlParameter("@Category_Name", SqlDbType.VarChar);
                p2.Value = txtaddCategory.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Brand", SqlDbType.VarChar);
                p3.Value = txtBrand.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
                p4.Value = CboxSupplier.Text.ToUpper().Trim();


                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);


                //step 6 
                cmd.Connection = conn;

                // step 7 
                cmd.CommandText = "update  tbl_Category  set  Date = @Date,Category_Name = @Category_Name,Brand = @Brand,Supplier_Name = Supplier_Name where Category_id =  " + ID;
                //step 8

                conn.Open();

                //step 9
                cmd.ExecuteNonQuery();


                conn.Close();
                MessageBox.Show("Update SucessFully....");
                LoadGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True"))
                {
                    // SQL query to retrieve data from the table
                    string query = "SELECT Supplier_Name FROM tbl_Supplier";


                    // Create a SqlDataAdapter to execute the query and fill the DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    DataTable dataTable1 = new DataTable();


                    // Open the connection and fill the DataTable
                    connection.Open();
                    adapter.Fill(dataTable1);


                    // Close the connection
                    connection.Close();

                    // Bind the DataTable to the ComboBox
                    CboxSupplier.DataSource = dataTable1;
                    CboxSupplier.DisplayMember = "Supplier_Name"; // Display the 'NameColumn' in the ComboBox


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void PboxaddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier frmSupplier = new frmSupplier();
            frmSupplier.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the Record", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);


                if (dr == DialogResult.Yes)
                {

                    //step 6 
                    cmd.Connection = conn;

                    // step 7 
                    cmd.CommandText = "delete from tbl_Category where Category_id = " + ID;

                    //step 8

                    conn.Open();

                    //step 9
                    cmd.ExecuteNonQuery();

                    //step 1

                    conn.Close();
                    MessageBox.Show("Delete record....");
                    LoadGrid();
                }
                else
                {
                    LoadGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoadGrid();
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Sidebartimer.Start();
        }

        private void Sidebartimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 150;

                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    Sidebartimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 150;
                if (sidebar.Width == sidebar.MinimumSize.Width) ;
                sidebarExpand = true;
                Sidebartimer.Stop();
            }

        }

        private void TxtaddCategory_TextChanged(object sender, EventArgs e)
        {
            txtaddCategory.Focus();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
