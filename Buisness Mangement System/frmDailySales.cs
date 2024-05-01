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
    public partial class frmDailySales : Form
    {
        public frmDailySales()
        {
            InitializeComponent();

            Gridview.SelectionChanged += Gridview_SelectionChanged;
        }

        private void BtnCLose_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }


        private const string connectionString = "Data Source=.;Initial Catalog=Business;Integrated Security=True";


        private void load1_Click(object sender, EventArgs e)
        {

            DateTime startDate = DatefrmPickee.Value;



            string query = "Select * from grid WHERE Customer_Name =   @StartDate  ";

            using (SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True"))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.Parameters.AddWithValue("@StartDate", startDate);


                con.Open();

                MessageBox.Show("Date fetched based on the seleted date range");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                Gridview.DataSource = dataTable;


            }
        }

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Business;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        SqlDataAdapter sda;

        public object endDate { get; private set; }

        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            //con.Open();
            sda = new SqlDataAdapter("select * from  grid", con);
            sda.Fill(ds);
            Gridview.DataSource = ds.Tables[0];
            //con.Close();
        }

        private void FrmDailySales_Load(object sender, EventArgs e)
        {
            LoadGrid();


            DatefrmPickee.Value = DateTime.Today.AddDays(-7);

        }

        private void Gridview_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRowCount = Gridview.SelectedRows.Count;

            // Update the label text to display the count
            lblCount.Text = "Selected Rows Sales: " + selectedRowCount.ToString();
        }

        private void Gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


   
