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
    public partial class frmSupplier : Form
    {

        bool sidebarExpand;
        public frmSupplier()
        {
            InitializeComponent();
        }


        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False");
        SqlCommand cmd = new SqlCommand();



        SqlDataAdapter sda;
        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            //con.Open();
            sda = new SqlDataAdapter("select * from  tbl_Supplier", conn);
            sda.Fill(ds);
            dgvGridViewSuppiler.DataSource = ds.Tables[0];
            //con.Close();
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();   
        }

        private void Btnsave2_Click(object sender, EventArgs e)
        {

            try
            {

                SqlParameter p1 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
                p1.Value = txtSuppilerName.Text.ToUpper().Trim();

                SqlParameter p2 = new SqlParameter("@Address", SqlDbType.VarChar);
                p2.Value = txtAddress.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Phone_no", SqlDbType.Int);
                p3.Value = Convert.ToInt32(txtPhoneNo.Text.ToUpper().Trim());

                SqlParameter p4 = new SqlParameter("@Fax", SqlDbType.Int);
                p4.Value = Convert.ToInt32(txtFaxx.Text.ToUpper().Trim());

                SqlParameter p5 = new SqlParameter("@Email_Id", SqlDbType.VarChar);
                p5.Value = txtEmailId.Text.ToUpper().Trim();



                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);


                //step 6 
                cmd.Connection = conn;

                // step 7 
                cmd.CommandText = "insert into tbl_Supplier(Supplier_Name,Address,Phone_no,Fax,Email_Id) Values (@Supplier_Name,@Address,@Phone_no,@Fax,@Email_Id)";

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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                SqlParameter p1 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
                p1.Value = txtSuppilerName.Text.ToUpper().Trim();

                SqlParameter p2 = new SqlParameter("@Address", SqlDbType.VarChar);
                p2.Value = txtAddress.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Phone_no", SqlDbType.Int);
                p3.Value = Convert.ToInt32(txtPhoneNo.Text.ToUpper().Trim());

                SqlParameter p4 = new SqlParameter("@Fax", SqlDbType.Int);
                p4.Value = Convert.ToInt32(txtFaxx.Text.ToUpper().Trim());

                SqlParameter p5 = new SqlParameter("@Email_Id", SqlDbType.VarChar);
                p5.Value = txtEmailId.Text.ToUpper().Trim();



                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);


                //step 6 
                cmd.Connection = conn;

                // step 7 
                cmd.CommandText = cmd.CommandText = "update tbl_Supplier set Supplier_Name = @Supplier_Name,Address = @Address,Phone_no = @Phone_no,Fax = @Fax,Email_Id = @Email_Id where id =  " + ID; ;

                //step 8

                conn.Open();

                //step 9
                cmd.ExecuteNonQuery();


                conn.Close();
                MessageBox.Show("Update Sucessfully....");
                LoadGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the Record", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);


                if (dr == DialogResult.Yes)
                {

                    //step 6 
                    cmd.Connection = conn;

                    // step 7 
                    cmd.CommandText = "delete from tbl_Supplier where id = " + ID;

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
        public int ID;
        private void DgvGridViewSuppiler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dgvGridViewSuppiler.Rows[e.RowIndex].Cells[0].Value.ToString());

            txtSuppilerName.Text = dgvGridViewSuppiler.Rows[e.RowIndex].Cells[1].Value.ToString();


            txtAddress.Text = dgvGridViewSuppiler.Rows[e.RowIndex].Cells[2].Value.ToString();

            txtPhoneNo.Text = dgvGridViewSuppiler.Rows[e.RowIndex].Cells[3].Value.ToString();

            txtFaxx.Text = dgvGridViewSuppiler.Rows[e.RowIndex].Cells[4].Value.ToString();

            txtEmailId.Text = dgvGridViewSuppiler.Rows[e.RowIndex].Cells[5].Value.ToString();

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();

        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        
        }

        private void BtnAddStock_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock frmEntryOfStock = new frmEntryOfStock();
            frmEntryOfStock.Show();
            
        }

        private void BtnNewTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing = new frmNewTransaction();
            billing.Show();
        }

        private void BtnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier frmSupplier = new frmSupplier();
            frmSupplier.Show();
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategory = new frmaddCategory();
            frmaddCategory.Show();
        }

        private void BtnAddproduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();

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

        private void BtnAddSupplier_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

        }

        private void Pboxusersetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
        }
    }
}
