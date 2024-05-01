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

    public partial class frmTotal_Product : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        public frmTotal_Product()
        {
            InitializeComponent();

            Gridview.SelectionChanged += Gridview_SelectionChanged;
        }

        private void BtnCLose_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();

        }

        SqlDataAdapter sda; 
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string Name = txtSearch.Text.ToString().Trim();
            DataSet ds = new DataSet();
            string query = " select * from tbl_products where Product_Name like '" + Name + "%'";
            sda = new SqlDataAdapter(query, con);
            sda.Fill(ds);
            Gridview.DataSource = ds.Tables[0];
        }


        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            //con.Open();
            sda = new SqlDataAdapter("select * from  tbl_products", con);
            sda.Fill(ds);
            Gridview.DataSource = ds.Tables[0];
            //con.Close();
        }

        private void FrmTotal_Product_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {

            if (txtSearch.Text == "")
            {
                txtSearch.Text = "------------------------------------------------------Serach Here---------------------------------------------------------------------";
            }

        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "------------------------------------------------------Serach Here---------------------------------------------------------------------")
            {
                txtSearch.Text = "";
            }
        }

        private void Gridview_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRowCount = Gridview.SelectedRows.Count;

            // Update the label text to display the count
            lblCount.Text = "Selected Rows Products: " + selectedRowCount.ToString();
        }
    }
}

