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
    public partial class frmProducts : Form
    {
        string conn = "Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False";
        bool sidebarExpand;

        private const string connectionStrings = "Data Source=.;Initial Catalog=Business;Integrated Security=True";

        public frmProducts()
        {
            InitializeComponent();
            PopulateComboBox();
            PopulateCategoryComboBox();


        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True;Encrypt=False");
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter sda;
        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            //con.Open();
            sda = new SqlDataAdapter("select * from  tbl_Products", con);
            sda.Fill(ds);
            dgvProdusctsEntry.DataSource = ds.Tables[0];
            //con.Close();
        }

        private bool IsUsernameExists(string Product_Name)
        {
            // SQL query to check if username exists
            string query = "SELECT COUNT(*) FROM tbl_products WHERE Product_Name= @Product_Name";

            using (SqlConnection connection = new SqlConnection(conn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@Product_Name", Product_Name);

                    // Open connection
                    connection.Open();

                    // Execute query
                    int count = (int)command.ExecuteScalar();

                    // Check if count is greater than 0 (username exists)
                    return count > 0;
                }
            }
        }

        private bool ValidateInputs()
        {
            // Check textboxes
            TextBox[] textBoxes = { txtProduct_Name };
            foreach (TextBox textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    return false;

                }
            }

            // Check datepicker
            //if (dtpProductEntry.Value.Date == DateTime.Today)
            //{
            //    return false;
            //}

            // Check numericalupdown
            if (NupWholeSalePrice.Value == 0 || NupSellingPrice.Value == 0 || NupQty.Value == 0)
            {
                return false;
            }

            // Check ComboBox
            //if (CBoxCategory.SelectedIndex == 1 || CBoxCategoryID.SelectedIndex == 1)
            //{
            //    return false;
            //}

            return true;
        }

        private void btnSAve_Click(object sender, EventArgs e)
        {

            string Product_Name = txtProduct_Name.Text.Trim();

            // Check if username already exists
            if (IsUsernameExists(Product_Name))
            {
                MessageBox.Show(" Product Name already exists. Please choose a different one.");
                return;
            }
            ///*************************************************************************************////
            ///


            if (!ValidateInputs())
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if validation fails
            }
            else
            {

                try
                {
                    SqlParameter p1 = new SqlParameter("@Date", SqlDbType.Date);
                    p1.Value = Convert.ToDateTime(dtpProductEntry.Value.ToShortDateString());

                    SqlParameter p2 = new SqlParameter("@Category", SqlDbType.VarChar);
                    p2.Value = CBoxCategory.Text.ToUpper().Trim();

                    SqlParameter p3 = new SqlParameter("@Category_id", SqlDbType.Int);
                    p3.Value = Convert.ToInt32(CBoxCategoryID.Text.ToUpper().Trim());

                    SqlParameter p4 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                    p4.Value = txtProduct_Name.Text.ToUpper().Trim();

                    SqlParameter p5 = new SqlParameter("@Qty", SqlDbType.Int);
                    p5.Value = Convert.ToInt32(NupQty.Text.ToUpper().Trim());

                    SqlParameter p6 = new SqlParameter("@AvailableQty", SqlDbType.Int);
                    p6.Value = Convert.ToInt32(NupAvailableQty.Text.ToUpper().Trim());

                    SqlParameter p7 = new SqlParameter("@Brand_Name", SqlDbType.VarChar);
                    p7.Value = txtBrand.Text.ToUpper().Trim();

                    SqlParameter p8 = new SqlParameter("@Wholesale_cost", SqlDbType.Int);
                    p8.Value = Convert.ToInt32(NupWholeSalePrice.Text.ToUpper().Trim());

                    SqlParameter p9 = new SqlParameter("@Selling_Price", SqlDbType.Int);
                    p9.Value = Convert.ToInt32(NupSellingPrice.Text.ToUpper().Trim());



                    // step 5
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                    cmd.Parameters.Add(p7);
                    cmd.Parameters.Add(p8);
                    cmd.Parameters.Add(p9);


                    //step 6 
                    cmd.Connection = con;

                    // step 7 
                    cmd.CommandText = "insert into tbl_Products(Date,Category,Category_id,Product_Name,Qty,AvailableQty,Brand_Name,WholeSale_cost,Selling_Price)" +
                        " Values (@Date,@Category,@Category_id,@Product_Name,@Qty,@AvailableQty,@Brand_Name,@WholeSale_cost,@Selling_Price)";

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



            //////////////////////////**************************************************************************////////////////////////////


        }

        private void MenuButton_Click(object sender, EventArgs e)
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

        private void Sidebartimer_Tick(object sender, EventArgs e)
        {
            Sidebartimer.Start();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void btnEntryOfStock_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock frmEntryOfStock = new frmEntryOfStock();
            frmEntryOfStock.Show();
        }

        private void btnNew_transaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing = new frmNewTransaction();
            billing.Show();
        }

        private void BtnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier supplier = new frmSupplier();
            supplier.Show();
        }

        private void btnAddCategoru_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategory = new frmaddCategory();
            frmaddCategory.Show();
        }

        private void BtnEntryOFProducts(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }

        public int ID;

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the Record", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);


                if (dr == DialogResult.Yes)
                {

                    //step 6 
                    cmd.Connection = con;

                    // step 7 
                    cmd.CommandText = "delete from tbl_Products where Product_id = " + ID;

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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter p1 = new SqlParameter("@Date", SqlDbType.Date);
                p1.Value = Convert.ToDateTime(dtpProductEntry.Value.ToShortDateString());

                SqlParameter p2 = new SqlParameter("@Category", SqlDbType.VarChar);
                p2.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p3 = new SqlParameter("@Category_id", SqlDbType.Int);
                p3.Value = Convert.ToInt32(CBoxCategoryID.Text.ToUpper().Trim());

                SqlParameter p4 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p4.Value = txtProduct_Name.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Qty", SqlDbType.Int);
                p5.Value = Convert.ToInt32(NupQty.Text.ToUpper().Trim());

                SqlParameter p6 = new SqlParameter("@AvailableQty", SqlDbType.Int);
                p6.Value = Convert.ToInt32(NupAvailableQty.Text.ToUpper().Trim());

                SqlParameter p7 = new SqlParameter("@Brand_Name", SqlDbType.VarChar);
                p7.Value = txtBrand.Text.ToUpper().Trim();

                SqlParameter p8 = new SqlParameter("@Wholesale_cost", SqlDbType.Int);
                p8.Value = Convert.ToInt32(NupWholeSalePrice.Text.ToUpper().Trim());

                SqlParameter p9 = new SqlParameter("@Selling_Price", SqlDbType.Int);
                p9.Value = Convert.ToInt32(NupSellingPrice.Text.ToUpper().Trim());



                // step 5
                cmd.Parameters.Clear();
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);

                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "update tbl_Products set Date = @Date ,Category = @Category,Category_id = @Category_id,Product_Name = @Product_Name,Qty = @Qty,AvailableQty = @AvailableQty,Brand_Name = @Brand_Name,WholeSale_cost = @WholeSale_cost,Selling_Price = @Selling_Price where Product_id =  " + ID;

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


        private void DgvProdusctsEntry_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            ID = Convert.ToInt32(dgvProdusctsEntry.Rows[e.RowIndex].Cells[0].Value.ToString());

            dtpProductEntry.Value = Convert.ToDateTime(dgvProdusctsEntry.Rows[e.RowIndex].Cells[1].Value.ToString());

            CBoxCategory.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[2].Value.ToString();

            CBoxCategoryID.SelectedValue = Convert.ToInt32(dgvProdusctsEntry.Rows[e.RowIndex].Cells[3].Value.ToString());


            txtProduct_Name.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[4].Value.ToString();

            NupQty.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[5].Value.ToString();

            NupAvailableQty.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[6].Value.ToString();

            txtBrand.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[7].Value.ToString();

            NupWholeSalePrice.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[8].Value.ToString();

            NupSellingPrice.Text = dgvProdusctsEntry.Rows[e.RowIndex].Cells[9].Value.ToString();



        }


        private void FrmProducts_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void PopulateCategoryComboBox()
        {
            // Clear existing items in the ComboBox
            CBoxCategoryID.Items.Clear();

            try
            {
                // Connect to the database
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    // SQL query to select category IDs
                    string query = "SELECT Category_id FROM tbl_Category";

                    // Create a SqlCommand object
                    SqlCommand command = new SqlCommand(query, connection);

                    // Open the connection
                    connection.Open();

                    // Execute the query and retrieve the data
                    SqlDataReader reader = command.ExecuteReader();

                    // Loop through the result set and add category IDs to the ComboBox
                    while (reader.Read())
                    {
                        CBoxCategoryID.Items.Add(reader["Category_id"]);
                    }

                    // Close the reader
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void PopulateComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True"))
                {
                    // SQL query to retrieve data from the table

                    string query2 = "SELECT Category_Name FROM tbl_Category";


                    // Create a SqlDataAdapter to execute the query and fill the DataTable

                    SqlDataAdapter adapter2 = new SqlDataAdapter(query2, connection);


                    DataTable dataTable2 = new DataTable();


                    // Open the connection and fill the DataTable
                    connection.Open();

                    adapter2.Fill(dataTable2);


                    // Close the connection
                    connection.Close();

                    // Bind the DataTable to the ComboBox


                    CBoxCategory.DataSource = dataTable2;
                    CBoxCategory.DisplayMember = "Category_Name";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Pboxusersetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProduct_Name.Clear();
            txtBrand.Clear();
            NupQty.Value = 0;
            NupAvailableQty.Value = 0;


            NupWholeSalePrice.Value = 0;
            NupSellingPrice.Value = 0;
        }
        private string connectionString = "Data Source=.;Initial Catalog=Business;Integrated Security=True";
        private void CBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (CBoxCategory.SelectedItem != null && CBoxCategory.SelectedItem.GetType() == typeof(DataRowView))
            {

                // Get the selected product from the ComboBox
                string selectedProduct = ((DataRowView)CBoxCategory.SelectedItem)["Category_Name"].ToString();

                // Query the database to get the price based on the selected product
                string query = "SELECT Category_id FROM tbl_Category WHERE Category_Name = @Category_Name";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    // Add parameter @ProductName
                    command.Parameters.AddWithValue("@Category_Name", selectedProduct);



                    try
                    {
                        connection.Open();

                        // Execute the query
                        object result = command.ExecuteScalar();



                        // Check if a price was found
                        if (result != null)
                        {

                            // Display the price in the TextBox
                            CBoxCategoryID.Text = result.ToString();

                        }
                        else
                        {
                            // Product not found in the database
                            CBoxCategoryID.Text = "0"; // or any default value you prefer

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void CBoxCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


   