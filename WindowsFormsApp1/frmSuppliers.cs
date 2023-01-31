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
    public partial class frmSuppliers : Form
    {
        OleDbConnection connection = new OleDbConnection();
        public frmSuppliers()
        {
            InitializeComponent();
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";
        }

        private void btnAddSup_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command  = new OleDbCommand();
            String supplier = "INSERT INTO  Supplier VALUES('" + txtSupplierId.Text + "','" + txtSupName.Text + "','" + txtCompanyName.Text + "','" + txtContactNumber.Text + "','" + txtDescription.Text + "')";
            command = new OleDbCommand(supplier, connection);
            command.ExecuteNonQuery();
            MessageBox.Show("Data Saved");
            dgvSuppliers.Rows.Clear();
            dgvSuppliers.Refresh();
            connection.Close();
        }

        private void btnUpdateSup_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String query = "update [Supplier] set [Suppliername] =@name, [CompanyName] =@coname, [ContactNumber] =@phone, [Description] =@des where [SupplierID] = @id";
            command.CommandText = query;
            command.Parameters.AddRange(new OleDbParameter[]
                  {
                       new OleDbParameter("@name", txtSupName.Text),
                       new OleDbParameter("@coname", txtCompanyName.Text),
                       new OleDbParameter("@phone", txtContactNumber.Text),
                       new OleDbParameter("@des", txtDescription.Text),

                       new OleDbParameter("@id", int.Parse(txtSupplierId.Text))
                  });
            command.ExecuteNonQuery();
            MessageBox.Show("Data Updated Successfully");
            dgvSuppliers.Rows.Clear();
            dgvSuppliers.Refresh();
            connection.Close();
        }

        private void btnDelSup_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String query = "DELETE FROM Supplier WHERE ItemId=" + txtSupplierId.Text + "";
            command.CommandText = query;
            command.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            dgvSuppliers.Rows.Clear();
            dgvSuppliers.Refresh();
            connection.Close();
        }

        private void btnViewSup_Click(object sender, EventArgs e)
        {
            connection.Open();
            String query = "SELECT * FROM Supplier";
            OleDbCommand command = new OleDbCommand(query);
            command.Connection = connection;

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dgvSuppliers.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            reader.Close();
            connection.Close();
        }

        private void dgvSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)

            {

                DataGridViewRow row = this.dgvSuppliers.Rows[e.RowIndex];



                txtSupplierId.Text = row.Cells[0].Value.ToString();

                txtSupName.Text = row.Cells[1].Value.ToString();

                txtCompanyName.Text = row.Cells[2].Value.ToString();
                txtContactNumber.Text = row.Cells[3].Value.ToString();
                txtDescription.Text = row.Cells[4].Value.ToString();



            }
        }
    }
}
