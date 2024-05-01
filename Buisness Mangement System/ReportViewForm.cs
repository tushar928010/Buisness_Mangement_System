using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Buisness_Mangement_System
{
    public partial class ReportViewForm : Form
    {
        public ReportViewForm()
        {
            InitializeComponent();
        }
        public string ReportName { get; set; }
        public DataTable ReportData { get; set; }

        private void GenerateReport(DataTable dataTable)
        {
            // Load your Crystal Report
            ReportDocument report = new ReportDocument();
            report.Load("GetByIdReport.rpt"); // Replace "YourCrystalReport.rpt" with your actual report file name

            // Pass the DataTable containing selected item data to the report
            report.SetDataSource(dataTable);

            // Display the Crystal Report
            crystalReportViewer1.ReportSource = report;


        }
        private void ReportViewForm_Load(object sender, EventArgs e)
        {

            //ReportDocument rdd = new ReportDocument();
            //rdd.Load(ReportName);
            //rdd.SetDataSource(ReportData);
            //crystalReportViewer1.ReportSource = rdd;


        }

        private void CrystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
  
