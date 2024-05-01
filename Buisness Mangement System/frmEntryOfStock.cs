using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Buisness_Mangement_System
{
    public partial class frmEntryOfStock : Form
    {
        string conn = "Data Source=.;Initial Catalog=Business;Integrated Security=True";

        bool sidebarExpand;

        public frmEntryOfStock()
        {
            InitializeComponent();

            //PopulateComboBox();

        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();


        SqlDataAdapter sda;
        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            //con.Open();
            sda = new SqlDataAdapter("select * from  Stock", con);
            sda.Fill(ds);
            Gridview.DataSource = ds.Tables[0];
            //con.Close();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            LoadGrid();
            LoadData();
            LoadCategories();


            DatefrmPickee.Value = DateTime.Today.AddDays(-7);
            DatetoPicker.Value = DateTime.Today;

        }
        private void LoadCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Category_id, Category_Name FROM tbl_Category";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable categoryTable = new DataTable();

                adapter.Fill(categoryTable);

                CBoxCategory.DataSource = categoryTable;
                CBoxCategory.DisplayMember = "Category_Name";
                CBoxCategory.ValueMember = "Category_id";

                CBoxCategory.SelectedIndex = -1; // clear selection
            }
        }


        private void LoadProducts(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Product_id, Product_Name FROM tbl_products WHERE Category_id = @Category_id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Category_id", categoryId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable productTable = new DataTable();

                adapter.Fill(productTable);

                CBoxProductName.DataSource = productTable;
                CBoxProductName.DisplayMember = "Product_Name";
                CBoxProductName.ValueMember = "Product_id";

                CBoxProductName.SelectedIndex = -1;
            }
        }



        public void SelectedRowTotal()
        {
            double sum = 0;

            for (int i = 0; i < Gridview.Rows.Count; i++)
            {
                if (Convert.ToBoolean(Gridview.Rows[i].Cells[0].Value) == true)
                {
                    sum += double.Parse(Gridview.Rows[i].Cells[5].Value.ToString());
                }

                txtTotal.Text = sum.ToString();
            }
        }

        public void SelectedRowTotal2()
        {
            double sum = 0;

            for (int i = 0; i < Gridview.Rows.Count; i++)
            {
                if (Convert.ToBoolean(Gridview.Rows[i].Cells[0].Value) == true)
                {
                    sum += double.Parse(Gridview.Rows[i].Cells[6].Value.ToString());
                }

                txtSalesProfit.Text = sum.ToString();
            }
        }




        public int ID;
        private void Gridview_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(Gridview.Rows[e.RowIndex].Cells[0].Value.ToString());


            CBoxCategory.Text = Gridview.Rows[e.RowIndex].Cells[1].Value.ToString();

            CBoxProductName.Text = Gridview.Rows[e.RowIndex].Cells[2].Value.ToString();


            txtProductQty.Text = Gridview.Rows[e.RowIndex].Cells[3].Value.ToString();

            txtWholeSaleprice.Text = Gridview.Rows[e.RowIndex].Cells[4].Value.ToString();

            txtSellingPrice.Text = Gridview.Rows[e.RowIndex].Cells[5].Value.ToString();

            CboxSupplier.Text = Gridview.Rows[e.RowIndex].Cells[6].Value.ToString();

            DatePicker.Value = Convert.ToDateTime(Gridview.Rows[e.RowIndex].Cells[7].Value.ToString());
        }





        private bool IsUsernameExists(string Product)
        {
            // SQL query to check if username exists
            string query = "SELECT COUNT(*) FROM Stock WHERE Product_Name = @Product_Name";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Product_Name", Product);

                    // Open connection
                    connection.Open();

                    // Execute query
                    int count = (int)command.ExecuteScalar();

                    // Check if count is greater than 0 (username exists)
                    return count > 0;
                }
            }
        }


        private void btnAddStock_Click_1(object sender, EventArgs e)
        {
            // Retrieve username and password from textboxes
            // Retrieve username and password from textboxes
            string Product = CBoxProductName.Text.Trim();


            // Validate both fields are filled
            if (string.IsNullOrWhiteSpace(Product))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Check if username already exists
            if (IsUsernameExists(Product))
            {
                MessageBox.Show("Product already exists. Please choose a different one.");
                return;
            }



            try
            {

                SqlParameter p1 = new SqlParameter("@Category", SqlDbType.VarChar);
                p1.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p2 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p2.Value = CBoxProductName.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Qty", SqlDbType.Int);
                p3.Value = txtProductQty.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("@WholeSale_Price", SqlDbType.Int);
                p4.Value = txtWholeSaleprice.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Selling_Price", SqlDbType.Int);
                p5.Value = txtSellingPrice.Text.ToUpper().Trim();

                SqlParameter p6 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
                p6.Value = CboxSupplier.Text.ToUpper().Trim();

                SqlParameter p7 = new SqlParameter("@Date", SqlDbType.Date);
                p7.Value = Convert.ToDateTime(DatePicker.Value.ToShortDateString());


                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);


                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "insert into Stock (Category,Product_Name,Qty,WholeSale_Price,Selling_Price,Supplier_Name,Date) Values (@Category,@Product_Name,@Qty,@WholeSale_Price,@Selling_Price,@Supplier_Name,@Date)";

                //step 8

                con.Open();

                //step 9
                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Add successfully....");
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoadGrid();
            }

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the Record", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);


                if (dr == DialogResult.Yes)
                {

                    //step 6 
                    cmd.Connection = con;

                    // step 7 
                    cmd.CommandText = "delete from Stock where Product_id = " + ID;

                    //step 8

                    con.Open();

                    //step 9
                    cmd.ExecuteNonQuery();

                    //step 1

                    con.Close();
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
        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("Select * From Stock", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Gridview.DataSource = dt;
            Gridview.AllowUserToAddRows = false;
            DataGridViewCheckBoxColumn checkboxcol = new DataGridViewCheckBoxColumn();
            checkboxcol.Width = 40;
            checkboxcol.Name = "check1";
            checkboxcol.HeaderText = "CheckToAdd";
            Gridview.Columns.Insert(0, checkboxcol);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                SqlParameter p1 = new SqlParameter("@Category", SqlDbType.VarChar);
                p1.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p2 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p2.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Qty", SqlDbType.Int);
                p3.Value = txtProductQty.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("@WholeSale_Price", SqlDbType.Int);
                p4.Value = txtWholeSaleprice.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Selling_Price", SqlDbType.Int);
                p5.Value = txtSellingPrice.Text.ToUpper().Trim();

                SqlParameter p6 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
                p6.Value = CboxSupplier.Text.ToUpper().Trim();

                SqlParameter p7 = new SqlParameter("@Date", SqlDbType.Date);
                p7.Value = Convert.ToDateTime(DatePicker.Value.ToShortDateString());


                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);

                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "update Stock set Category = @Category,Qty = @Qty,WholeSale_Price = @WholeSale_Price,Selling_Price = @Selling_Price,Supplier_Name = @Supplier_Name,Date = @Date where Product_id =  " + ID;

                //step 8

                con.Open();

                //step 9
                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Update successfully....");
                LoadGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoadGrid();
            }

        }

        private const string connectionString = "Data Source=.;Initial Catalog=Business;Integrated Security=True";

        private void BtnLoadRecord_Click_1(object sender, EventArgs e)
        {
            DateTime startDate = DatefrmPickee.Value;
            DateTime endDate = DatetoPicker.Value;


            string query = "Select * from Stock WHERE Date BETWEEN  @StartDate AND @EndDate ";

            using (SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True"))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                con.Open();

                MessageBox.Show("Date fetched based on the seleted date range");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                dgvStockHistory.DataSource = dataTable;
            }
        }

        private void TxtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string Name = txtSearch.Text.ToString().Trim();
            DataSet ds = new DataSet();
            string query = " select * from Stock where Category like '" + Name + "%'";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(ds);
            Gridview.DataSource = ds.Tables[0];
        }

        private void Gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            btnSum.PerformClick();

            btnSalesProfit.PerformClick();

            if (Gridview != null && e.RowIndex >= 1 && e.RowIndex < Gridview.Rows.Count)
            {
                // Ensure the cell is not null
                DataGridViewCell cell = Gridview.Rows[e.RowIndex].Cells[1];
                if (cell != null && cell.Value != null)
                {
                    ID = Convert.ToInt32(cell.Value.ToString());
                }
            }
            //ID = Convert.ToInt32(Gridview.Rows[e.RowIndex].Cells[0].Value.ToString());

            CBoxCategory.Text = Gridview.Rows[e.RowIndex].Cells[2].Value.ToString();

            CBoxProductName.Text = Gridview.Rows[e.RowIndex].Cells[3].Value.ToString();

            txtProductQty.Text = Gridview.Rows[e.RowIndex].Cells[4].Value.ToString();

            txtWholeSaleprice.Text = Gridview.Rows[e.RowIndex].Cells[5].Value.ToString();

            txtSellingPrice.Text = Gridview.Rows[e.RowIndex].Cells[6].Value.ToString();

            CboxSupplier.Text = Gridview.Rows[e.RowIndex].Cells[7].Value.ToString();

            DatePicker.Value = Convert.ToDateTime(Gridview.Rows[e.RowIndex].Cells[8].Value.ToString());
        }

        private void TxtSearch_Leave_1(object sender, EventArgs e)
        {

            if (txtSearch.Text == "")
            {
                txtSearch.Text = "------------------------------------------------------Serach Here---------------------------------------------------------------------";
            }

        }

        private void TxtSearch_Enter_1(object sender, EventArgs e)
        {
            if (txtSearch.Text == "------------------------------------------------------Serach Here---------------------------------------------------------------------")
            {
                txtSearch.Text = "";
            }

        }

        private void BtnCLose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PboxHome_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void PboxAddStock_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock addStock = new frmEntryOfStock();
            addStock.Show();
        }

        private void PBoxuseraccout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
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
        //private void PopulateComboBox()
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True"))
        //        {
        //            // SQL query to retrieve data from the table
        //            string query = "SELECT Supplier_Name FROM tbl_Supplier";
        //            string query2 = "SELECT Category_Name FROM tbl_Category";
        //            string query3 = "SELECT Product_Name FROM tbl_Products";


        //            // Create a SqlDataAdapter to execute the query and fill the DataTable
        //            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
        //            SqlDataAdapter adapter2 = new SqlDataAdapter(query2, connection);
        //            SqlDataAdapter adapter3 = new SqlDataAdapter(query3, connection);


        //            DataTable dataTable1 = new DataTable();
        //            DataTable dataTable2 = new DataTable();
        //            DataTable dataTable3 = new DataTable();

        //            // Open the connection and fill the DataTable
        //            connection.Open();
        //            adapter.Fill(dataTable1);
        //            adapter2.Fill(dataTable2);
        //            adapter3.Fill(dataTable3);

        //            // Close the connection
        //            connection.Close();

        //            // Bind the DataTable to the ComboBox
        //            CboxSupplier.DataSource = dataTable1;
        //            CboxSupplier.DisplayMember = "Supplier_Name"; // Display the 'NameColumn' in the ComboBox

        //            CBoxCategory.DataSource = dataTable2;
        //            CBoxCategory.DisplayMember = "Category_Name";

        //            CBoxProductName.DataSource = dataTable3;
        //            CBoxProductName.DisplayMember = "Product_Name";

        //        }
        //    }
        //catch (Exception ex)
        //{
        //    MessageBox.Show("Error: " + ex.Message);
        //}
        //}

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategoriescs = new frmaddCategory();
            frmaddCategoriescs.ShowDialog();
        }

        private void pboxupplier_Click(object sender, EventArgs e)
        {
            frmSupplier frmSupplier = new frmSupplier();
            frmSupplier.ShowDialog();
        }

        private void PBoxaddcategory_Click(object sender, EventArgs e)
        {
            frmaddCategory frmaddCategory = new frmaddCategory();
            frmaddCategory.ShowDialog();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock form1 = new frmEntryOfStock();
            form1.ShowDialog();
        }

        private void PboxNewTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing = new frmNewTransaction();
            billing.ShowDialog();
        }

        private void btnNewTransaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing2 = new frmNewTransaction();
            billing2.ShowDialog();
        }

        private void btnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier frmSupplier1 = new frmSupplier();
            frmSupplier1.ShowDialog();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategoriescs4 = new frmaddCategory();
            frmaddCategoriescs4.ShowDialog();
        }


        private void PboxAddProducts_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier supplier = new frmSupplier();
            supplier.ShowDialog();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }



        private void BtnSum_Click(object sender, EventArgs e)
        {
            SelectedRowTotal();
        }

        private void btnSalesProfit_Click(object sender, EventArgs e)
        {
            SelectedRowTotal2();
        }


        private void CBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CBoxCategory.SelectedIndex >= 0)
            {

                DataRowView selectedRow = (DataRowView)CBoxCategory.SelectedItem;
                int categoryId = Convert.ToInt32(selectedRow["Category_id"]);
                LoadProducts(categoryId);
            }
            else
            {
                CBoxProductName.DataSource = null;
            }

        }






        private void CBoxProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSellingPrice.Clear();
            txtProductQty.Clear();

            if (CBoxProductName.SelectedItem != null && CBoxProductName.SelectedItem.GetType() == typeof(DataRowView))
            {
              
                // Get the selected product from the ComboBox
                string selectedProduct = ((DataRowView)CBoxProductName.SelectedItem)["Product_Name"].ToString();

                // Query the database to get the price based on the selected product
                string query = "SELECT Selling_Price FROM tbl_products WHERE Product_Name = @Product_Name";
                string query2 = "SELECT Qty FROM tbl_products WHERE Product_Name = @Product_Name";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlCommand command2 = new SqlCommand(query2, connection);

                    // Add parameter @ProductName
                    command.Parameters.AddWithValue("@Product_Name", selectedProduct);
                    command2.Parameters.AddWithValue("@Product_Name", selectedProduct);


                    try
                    {
                        connection.Open();

                        // Execute the query
                        object result = command.ExecuteScalar();
                        object result2 = command2.ExecuteScalar();


                        // Check if a price was found
                        if (result != null && result2 != null)
                        {
                           
                            // Display the price in the TextBox
                            txtSellingPrice.Text = result.ToString();
                            txtProductQty.Text = result2.ToString();
                        }
                        else
                        {
                            // Product not found in the database
                            txtSellingPrice.Text = "0"; // or any default value you prefer
                            txtProductQty.Text = "0";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }
    }
}

    
        
   


        






























































