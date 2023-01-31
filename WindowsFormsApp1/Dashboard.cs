using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{

    public partial class frmDashboard : Form
    {
        string jobrole;
        OleDbConnection connection = new OleDbConnection();
        public frmDashboard(string usernamelog, string jobRole)
        {
            InitializeComponent();
            //txtUsername.text = usernamelog;
            this.jobrole = jobRole;


            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";




        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            connection.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            if (jobrole == "admin")
            {
                frmStaff frm = new frmStaff();
                frm.Show();
            }
            else
            {
                MessageBox.Show("You cannot access to the staff ");
            }
            //command.CommandText = "SELECT JobStatus FROM Staff WHERE Username ='" + txtSu + "'and Password='" + txtSPassword.Text + "'";
            connection.Close();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory(jobrole);
            frm.Show();

            connection.Open();
           

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            
            connection.Close();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            frmItem frm = new frmItem(jobrole);
            frm.Show();

            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;

            connection.Close();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            frmInvoice frm = new frmInvoice();
            frm.Show();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            if (jobrole == "admin")
            {
                frmSuppliers frm = new frmSuppliers();
                frm.Show();
            }
            else
            {
                MessageBox.Show("You cannot access to this Module");
            }
            
        }

       
    }
}
