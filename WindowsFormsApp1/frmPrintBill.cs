using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmPrintBill : Form
    {
        public frmPrintBill()
        {
            InitializeComponent();
            
        }

        private void frmPrintBill_Load(object sender, EventArgs e)
        {
            lblPrintTotal.Text = frmInvoice.ttl;
            
            
            
        }
    }
}
