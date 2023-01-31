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
    
    public partial class frmSalesReport : Form
    {
        OleDbConnection connection = new OleDbConnection();
        public frmSalesReport()
        {
            InitializeComponent();
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from Invoice";
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(dt);
            dgvSalesReport.DataSource = dt;
            
            connection.Close();
           
        }
    }
}
