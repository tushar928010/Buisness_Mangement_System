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
    public partial class Dashboard : Form
    {

        bool sidebarExpand;

        public Dashboard()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
       
        SqlDataAdapter sda;

        private void Dashboard_Load(object sender, EventArgs e)
        {
             
            //lbl_Totol_Products.Text = ("select count(*) from Stock ").ToString();
            string query = "select * from tbl_products";
            sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int Count = dt.Rows.Count;
            lbl_Totol_Products.Text = Count.ToString();
            ShowSales();

        }


        public void ShowSales()
        {
            string query = "select * from grid";
            sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int Count = dt.Rows.Count;
            lblSales.Text = Count.ToString();


        }
 

        private void Daily_Sales_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDailySales dailysale = new frmDailySales();
            dailysale.Show();
        }

        private void Total_Product_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTotal_Product totolproduct = new frmTotal_Product();
            totolproduct.Show();
        }

        private void BtnCLose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();   
        }

        private void New_Transaction_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEntryOfStock addstock = new frmEntryOfStock();
            addstock.Show();
        }


        private void SidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 150;

                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    SidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 150;
                if (sidebar.Width == sidebar.MinimumSize.Width) ;
                sidebarExpand = true;
                SidebarTimer.Stop();

            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            SidebarTimer.Start();
        }


        private void btnBilling_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewTransaction bill = new frmNewTransaction();
            bill.Show();
        }

        private void btnSuppiler_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSupplier supplier = new frmSupplier();   
            supplier.Show();
        }

        private void btnCategory_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmaddCategory categories = new frmaddCategory(); 
            categories.Show();
        }

        private void btnaddProduct_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmProducts frmProducts = new frmProducts();
            frmProducts.Show();
        }

        private void Pboxusersetting_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegistation frmRegistation = new frmRegistation();
            frmRegistation.Show();
        }

    }
    
}




