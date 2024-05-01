using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;



namespace Buisness_Mangement_System
{
    public partial class frmNewTransaction : Form
    {
        string conn = "Data Source=.;Initial Catalog=Business;Integrated Security=True";

        private const string connectionStrings = "Data Source=.;Initial Catalog=Business;Integrated Security=True";

        bool sidebarExpand;

        private string tableName = "tbl_products";

        // Assuming this is your ComboBox and TextBox

        public frmNewTransaction()
        {
            InitializeComponent();

            //PopulateComboBox();
        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        private string connectionString = "Data Source=.;Initial Catalog=Business;Integrated Security=True";

        SqlDataAdapter sda;

        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string query = "select * from grid";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(ds);
            DgvNewTransaction.DataSource = ds.Tables[0];

        }

        public void LoadData()
        {
            SqlCommand cmd = new SqlCommand("Select * From grid", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DgvNewTransaction.DataSource = dt;
            DgvNewTransaction.AllowUserToAddRows = false;
            DataGridViewCheckBoxColumn checkboxcol = new DataGridViewCheckBoxColumn();
            checkboxcol.Width = 40;
            checkboxcol.Name = "check1";
            checkboxcol.HeaderText = "CheckToAdd";
            DgvNewTransaction.Columns.Insert(0, checkboxcol);



        }

        private void Billing_Load(object sender, EventArgs e)
        {
            LoadGrid();
            timer1.Start();
            LoadData();
            LoadCategories();

        }


        private void LoadCategories()
        {
            CBoxCategory.DataSource = null; // Clear the DataSource property
            CBoxCategory.Items.Clear(); // Clear the Items collection

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Initialize the connection
                connection.Open();

                string query = "SELECT Category_id, Category_Name FROM tbl_Category";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int categoryId = reader.GetInt32(0);
                            string categoryName = reader.GetString(1);
                            CBoxCategory.Items.Add(new Category(categoryId, categoryName));
                        }
                    }
                }
            }
        }

  

        private void LoadProducts(int categoryId)
        {
            CBoxProductName.DataSource = null; // Clear the DataSource property

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Initialize the connection
                connection.Open();

                string query = "SELECT Product_id, Product_Name FROM tbl_products WHERE Category_id = @Category_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Category_id", categoryId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        CBoxProductName.Items.Clear(); // Clear the Items collection
                        while (reader.Read())
                        {
                            int productId = reader.GetInt32(0);
                            string productName = reader.GetString(1);
                            CBoxProductName.Items.Add(new Product(productId, productName));
                        }
                    }
                }
            }

            // Display product count
            txtproductCount.Text = CBoxProductName.Items.Count.ToString();
        }




        public class Category
        {
            public int CategoryID { get; }
            public string CategoryName { get; }

            public Category(int categoryId, string categoryName)
            {
                CategoryID = categoryId;
                CategoryName = categoryName;
            }

            public override string ToString()
            {
                return CategoryName;
            }
        }

        // Custom class to represent Product
        public class Product
        {
            public int ProductID { get; }
            public string ProductName { get; }

            public Product(int productId, string productName)
            {
                ProductID = productId;
                ProductName = productName;
            }

            public override string ToString()
            {
                return ProductName;
            }
        }
        private void LblTimer_Click(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss  tt");
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

        private void PboxDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void Pboxnew_transaction(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock form = new frmEntryOfStock();
            form.Show();
        }

        private void PboxBilling(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction bill = new frmNewTransaction();
            bill.Show();
        }

        private void Pboxusersetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
        }

        private void MenuButton_Click_1(object sender, EventArgs e)
        {
            Sidebartimer.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier supplier = new frmSupplier();
            supplier.Show();
        }

        private void PboxSupplier(object sender, EventArgs e)
        {

            frmSupplier supplier = new frmSupplier();
            supplier.Show();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategoriescs = new frmaddCategory();
            frmaddCategoriescs.Show();
        }

        private void PboxAddCategory(object sender, EventArgs e)
        {
            frmaddCategory categoriescs = new frmaddCategory();
            categoriescs.Show();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void New_Transaction_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock form1 = new frmEntryOfStock();
            form1.Show();
        }

        private void btnNewTransaction_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction billing = new frmNewTransaction();
            billing.Show();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }

        private void Pboxaddcate_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory frmaddCategory = new frmaddCategory();
            frmaddCategory.Show();
        }


        private void BtnAdd_Click_1(object sender, EventArgs e)

        {

            int purchasedQty = (int)NuPUserQty.Value;
            int totalQty = int.Parse(NupTotalQty.Text);

            if (purchasedQty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Perform subtraction
            int remainingQty = totalQty - purchasedQty;

            // Check if remaining quantity is non-negative
            if (remainingQty < 0)
            {
                MessageBox.Show("Out of stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update database and TextBox2
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = $"UPDATE tbl_products SET AvailableQty = @AvailableQty";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@AvailableQty", remainingQty);
                    command.ExecuteNonQuery();
                    connection.Close();

                    // Update TextBox2
                    NupTotalQty.Text = remainingQty.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                SqlParameter p1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                p1.Value = txtCustomer_Name.Text.Trim();

                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.Date);
                p2.Value = Convert.ToDateTime(DtpNewTransaction.Value.ToShortDateString());

                SqlParameter p3 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p3.Value = CBoxProductName.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("Category_Name", SqlDbType.VarChar);
                p4.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Price", SqlDbType.Decimal);
                p5.Value = Convert.ToDecimal(txtPrice.Text.ToString());

                SqlParameter p6 = new SqlParameter("@Product_Qty", SqlDbType.Int);
                p6.Value = Convert.ToInt32(NuPUserQty.Text.ToUpper().Trim());

                SqlParameter p7 = new SqlParameter("@AvailableQty", SqlDbType.Int);
                p7.Value = Convert.ToInt32(NupTotalQty.Text.ToUpper().Trim());

                SqlParameter p8 = new SqlParameter("@Discount", SqlDbType.Int);
                p8.Value = Convert.ToInt32(CBoxDiscout.SelectedItem.ToString());

                SqlParameter p9 = new SqlParameter("@Customer_Contactno", SqlDbType.BigInt);
                p9.Value = Convert.ToInt64(txtCustomerContactNo.Text.ToUpper().Trim());

                SqlParameter p10 = new SqlParameter("@Total", SqlDbType.Decimal);
                p10.Value = Convert.ToDecimal(txtTotalPrice.Text.ToUpper().Trim());

                SqlParameter p11 = new SqlParameter("@Payment", SqlDbType.Decimal);
                p11.Value = Convert.ToDecimal(txtPayment.Text.ToUpper().Trim());




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
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);




                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "insert into grid (Customer_Name,Date,Product_Name,Category_Name,Price,Product_Qty,AvailableQty,Discount,Customer_Contactno,Total,Payment) Values (@Customer_Name,@Date,@Product_Name,@Category_Name,@Price,@Product_Qty,@AvailableQty,@Discount,@Customer_Contactno,@Total,@Payment)";

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

        private void PboxaddProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }


        private void BtnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the Record", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);


                if (dr == DialogResult.Yes)
                {

                    //step 6 
                    cmd.Connection = con;

                    // step 7 
                    cmd.CommandText = "delete from grid where Unique_id = " + ID;

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
        public int ID;




        private void BtnUpdatee_Click_1(object sender, EventArgs e)
        {
            try
            {

                SqlParameter p1 = new SqlParameter("@Customer_Name", SqlDbType.VarChar);
                p1.Value = txtCustomer_Name.Text.Trim();

                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.Date);
                p2.Value = Convert.ToDateTime(DtpNewTransaction.Value.ToShortDateString());

                SqlParameter p3 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p3.Value = CBoxProductName.Text.ToUpper().Trim();

                SqlParameter p4 = new SqlParameter("Category_Name", SqlDbType.VarChar);
                p4.Value = CBoxCategory.Text.ToUpper().Trim();

                SqlParameter p5 = new SqlParameter("@Price", SqlDbType.Int);
                p5.Value = Convert.ToInt32(txtPrice.Text.ToString());

                SqlParameter p6 = new SqlParameter("@Product_Qty", SqlDbType.Int);
                p6.Value = Convert.ToInt32(NuPUserQty.Text.ToUpper().Trim());

                SqlParameter p7 = new SqlParameter("@AvailableQty", SqlDbType.Int);
                p7.Value = Convert.ToInt32(NupTotalQty.Text.ToUpper().Trim());

                SqlParameter p8 = new SqlParameter("@Discount", SqlDbType.Int);
                p8.Value = Convert.ToInt32(CBoxDiscout.SelectedItem.ToString());

                SqlParameter p9 = new SqlParameter("@Customer_Contactno", SqlDbType.BigInt);
                p9.Value = Convert.ToInt64(txtCustomerContactNo.Text.ToUpper().Trim());

                SqlParameter p10 = new SqlParameter("@Total", SqlDbType.Int);
                p10.Value = Convert.ToInt32(txtTotalPrice.Text.ToUpper().Trim());

                SqlParameter p11 = new SqlParameter("@Payment", SqlDbType.Float);
                p11.Value = (txtPayment.Text.ToUpper().Trim());



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
                cmd.Parameters.Add(p10);
                cmd.Parameters.Add(p11);


                //step 6 
                cmd.Connection = con;

                // step 7 
                cmd.CommandText = "update grid set Customer_Name = @Customer_Name,Date = @Date,Product_Name = @Product_Name,Price = @Price,Product_Qty = @Product_Qty,AvailableQty = @AvailableQty,Discount = @Discount,Customer_Contactno = @Customer_Contactno,Total = @Total,Payment = @Payment  where Unique_id =  " + ID;
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

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss  tt");
        }

        private void TxtSerachBox_TextChanged_1(object sender, EventArgs e)
        {
            string Name = txtSerachBox.Text.ToString().Trim();
            DataSet ds = new DataSet();
            string query = " select * from grid where Customer_Contactno like '" + Name + "%'";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(ds);
            DgvNewTransaction.DataSource = ds.Tables[0];
        }



        private void BtnCalculate_Click_1(object sender, EventArgs e)
        {




            decimal price;
            int quantity;

            // Parse the price from the TextBox
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                // Handle invalid price input
                txtTotalPrice.Text = "Invalid price";
                return;
            }

            // Parse the quantity from the TextBox
            if (!int.TryParse(NuPUserQty.Text, out quantity))
            {
                // Handle invalid quantity input
                txtTotalPrice.Text = "Invalid quantity";
                return;
            }

            // Calculate the total price
            decimal totalPrice = price * quantity;

            // Display the total price in the TextBox
            txtTotalPrice.Text = totalPrice.ToString();

            // Apply discount based on the selected discount percentage
            int selectedDiscountIndex = CBoxDiscout.SelectedIndex;
            decimal discount = 0;

       

            if (selectedDiscountIndex == 1)
            {
                discount = (totalPrice * 5 ) / 100 ; // 5% discount
            
            }
            else if (selectedDiscountIndex == 2)
            {
                discount = (totalPrice * 10) / 100; // 10% discount
            }

            // Calculate the payment after discount
            decimal payment = totalPrice - discount;

            // Display the payment in the TextBox
            txtPayment.Text = payment.ToString();
        }

        public void SelectedRowTotal()
        {
            double sum = 0;

            for (int i = 0; i < DgvNewTransaction.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DgvNewTransaction.Rows[i].Cells[0].Value) == true)
                {
                    sum += double.Parse(DgvNewTransaction.Rows[i].Cells[11].Value.ToString());
                }

                txtSumColumn.Text = sum.ToString();


            }

        }


        private void BtnAddColumn_Click_1(object sender, EventArgs e)
        {
            SelectedRowTotal();

        }


        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (DgvNewTransaction.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select rows before generating the report.");
                return;
            }




            List<DataRow> selectedRows = new List<DataRow>();
            foreach (DataGridViewRow row in DgvNewTransaction.SelectedRows)
            {
                selectedRows.Add(((DataRowView)row.DataBoundItem).Row);
            }

            //DataRow selectedRow = ((DataRowView)DgvNewTransaction.SelectedRows[0].DataBoundItem).Row;

            // Step 3: Create DataTable and Add Selected Row Data
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Unique_id");
            dataTable.Columns.Add("Customer_Name"); // Replace "Column1" with your actual column names
            dataTable.Columns.Add("Date");
            dataTable.Columns.Add("Product_Name");
            dataTable.Columns.Add("Category_Name");
            dataTable.Columns.Add("Price");
            dataTable.Columns.Add("Product_Qty");
            dataTable.Columns.Add("Discount");
            dataTable.Columns.Add("Customer_Contactno");
            dataTable.Columns.Add("Total");
            dataTable.Columns.Add("Payment");
            // Add more columns as needed

            // Add the selected row data to the DataTable


            foreach (DataRow row in selectedRows)
            {
                dataTable.Rows.Add(row["Unique_id"], row["Customer_Name"], row["Date"], row["Product_Name"],

               row["Category_Name"], row["Price"], row["Product_Qty"], row["Discount"],

               row["Customer_Contactno"], row["Total"], row["Payment"]);

                // Add more columns as needed
            }

            // Add more columns as needed

            // Step 4: Create Crystal Report Instance
            ReportDocument report = new ReportDocument();
            report.Load("GetByIdReport.rpt"); // Load your Crystal Report file

            // Step 5: Set Crystal Report Data Source
            report.SetDataSource(dataTable);

            // Step 6: Bind Crystal Report to CrystalReportViewer

            ReportViewForm reportViewForm = new ReportViewForm();
            reportViewForm.crystalReportViewer1.ReportSource = report;
            reportViewForm.Show();


        }

        private void CBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CBoxCategory.SelectedIndex >= 0)
            //{
            //    DataRowView selectedRow = (DataRowView)CBoxCategory.SelectedItem;
            //    int categoryId = Convert.ToInt32(selectedRow["Category_id"]);
            //    LoadProducts(categoryId);
            //}
            //else
            //{
            //    CBoxProductName.DataSource = null;
            //}

         

            Category selectedCategory = CBoxCategory.SelectedItem as Category;
            if (selectedCategory != null)
            {
                LoadProducts(selectedCategory.CategoryID);
            }


        }

    
      

        private void dgvNewTransactionCellContent_Click(object sender, DataGridViewCellEventArgs e)
        {
            btnColumnAdd.PerformClick();


            if (DgvNewTransaction != null && e.RowIndex >= 1 && e.RowIndex < DgvNewTransaction.Rows.Count)
            {

                DataGridViewCell cell = DgvNewTransaction.Rows[e.RowIndex].Cells[1];
                if (cell != null && cell.Value != null)
                {
                    ID = Convert.ToInt32(cell.Value.ToString());
                }



                //ID = Convert.ToInt32(DgvNewTransaction.Rows[e.RowIndex].Cells[0].Value.ToString());

                txtCustomer_Name.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[2].Value.ToString();

                DtpNewTransaction.Value = Convert.ToDateTime(DgvNewTransaction.Rows[e.RowIndex].Cells[3].Value.ToString());

                CBoxProductName.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[4].Value.ToString();

                CBoxCategory.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[5].Value.ToString();


                txtPrice.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[6].Value.ToString();

                NupTotalQty.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[7].Value.ToString();


                CBoxDiscout.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[8].Value.ToString();

                txtCustomerContactNo.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[9].Value.ToString();

                txtTotalPrice.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[10].Value.ToString();

                txtPayment.Text = DgvNewTransaction.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
        }
        private decimal GetProductPrice(int productId)
        {
            decimal productPrice = 0;
            int Qty = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Selling_Price FROM tbl_products WHERE Product_id= @Product_id";
    
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Product_id", productId);
                
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value )
                {
                    productPrice = Convert.ToDecimal(result);
                   
                }
                else
                {
                    // If the product price is not found, handle the case appropriately
                    // For example, you can throw an exception, return a default value, or take some other action
                    // For now, let's return -1 to indicate that the price is not found
                    return -1; // Adjust this based on your actual business logic
                }
            }
            return productPrice;
        }

        private int GetProductQuantity(int productId)
        {
            int productQty = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AvailableQty FROM tbl_products WHERE Product_id= @Product_id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Product_id", productId);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    productQty = Convert.ToInt32(result);
                }
            }
            return productQty;
        }



        private void CBoxProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product selectedProduct = CBoxProductName.SelectedItem as Product;
            if (selectedProduct != null)
            {
                int productId = selectedProduct.ProductID;
                // Fetch price of the selected product from the database
                decimal productPrice = GetProductPrice(productId);
                // Display the price in a TextBox or any other suitable control
                txtPrice.Text = productPrice.ToString(); // Assuming textBoxProductPrice is the TextBox to display price


            }



            Product selectedProduct2 = CBoxProductName.SelectedItem as Product;
            if (selectedProduct != null)
            {
                int productId = selectedProduct.ProductID;
                // Fetch quantity of the selected product from the database
                int productQty = GetProductQuantity(productId);
                // Display the quantity in a TextBox or any other suitable control
                NupTotalQty.Text = productQty.ToString(); // Assuming textBoxProductQty is the TextBox to display quantity
            }






        }



        //if (CBoxProductName.SelectedItem != null && CBoxProductName.SelectedItem.GetType() == typeof(DataRowView))
        //{

        //    // Get the selected product from the ComboBox
        //    string selectedProduct = ((DataRowView)CBoxProductName.SelectedItem)["Product_Name"].ToString();

        //    // Query the database to get the price based on the selected product
        //    string query = "SELECT Selling_Price FROM tbl_products WHERE Product_Name = @Product_Name";
        //    //string query2 = "SELECT Qty FROM tbl_products WHERE Product_Name = @Product_Name";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        //SqlCommand command2 = new SqlCommand(query2, connection);

        //        // Add parameter @ProductName
        //        command.Parameters.AddWithValue("@Product_Name", selectedProduct);
        //        //command2.Parameters.AddWithValue("@Product_Name", selectedProduct);


        //        try
        //        {
        //            connection.Open();

        //            // Execute the query
        //            object result = command.ExecuteScalar();
        //            //object result2 = command2.ExecuteScalar();


        //            // Check if a price was found
        //            if (result != null) /*&& result2 != null)*/
        //            {

        //                // Display the price in the TextBox
        //                txtPrice.Text = result.ToString();
        //                //NupTotalQty.Text = result2.ToString();
        //            }
        //            else
        //            {
        //                // Product not found in the database
        //                txtPrice.Text = "0"; // or any default value you prefer
        //                //NupTotalQty.Text = "0";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }



        private void btnDeleteAll_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM grid"; // Replace YourTableName with the name of your table
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            MessageBox.Show(rowsAffected + " rows deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadGrid();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}

































