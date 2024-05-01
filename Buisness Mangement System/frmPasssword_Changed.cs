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
    public partial class frmPasssword_Changed : Form
    {
        public frmPasssword_Changed()
        {
            InitializeComponent();
        }



        private void BtnCLose_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
