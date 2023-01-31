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
    public partial class frmItem : Form
    {
        OleDbConnection connection = new OleDbConnection();
        string jobrole;
        public frmItem(String jobRole)
        {
            InitializeComponent();
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";
            this.jobrole = jobRole;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (jobrole == "admin")
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                String item = "INSERT INTO  Item VALUES('" + txtItemId.Text + "','" + txtItemName.Text + "','" + txtPrice.Text + "','" + txtQuantity.Text + "','" + txtSupplierName.Text +  "')";
                command = new OleDbCommand(item, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Data Saved");
                dgvItem.Rows.Clear();
                dgvItem.Refresh();

                txtItemId.Clear();
                txtItemName.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
                txtSupplierName.Clear();
                connection.Close();
            }
            else
            {
                txtItemId.Enabled = false;
                txtItemName.Enabled = false;
                txtPrice.Enabled = false;
                txtQuantity.Enabled = false;
                txtSupplierName.Enabled = false;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (jobrole == "admin")
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                String query = "update [Item] set [ItemName] =@iname, [Price] =@price, [Quantity] =@qty, [SupplierName] =@supname where [ItemId] = @id";
                command.CommandText = query;
                command.Parameters.AddRange(new OleDbParameter[]
                      {
                       new OleDbParameter("@iname", txtItemName.Text),
                       new OleDbParameter("@price", txtPrice.Text),
                       new OleDbParameter("@qty", txtQuantity.Text),
                       new OleDbParameter("@supname", txtSupplierName.Text),

                       new OleDbParameter("@id", int.Parse(txtItemId.Text))
                      });
                command.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully");
                dgvItem.Rows.Clear();
                dgvItem.Refresh();
                connection.Close();
            }
            else
            {
                btnUpdate.Enabled = false;
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String query = "DELETE FROM Item WHERE ItemId=" + txtItemId.Text + "";
            command.CommandText = query;
            command.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            txtItemId.Clear();
            txtItemName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtSupplierName.Clear();
            dgvItem.Rows.Clear();
            dgvItem.Refresh();
            connection.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            connection.Open();
            String query = "SELECT * FROM Item";
            OleDbCommand command = new OleDbCommand(query);
            command.Connection = connection;

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dgvItem.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }
            reader.Close();
            connection.Close();
        }

        private void dgvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (jobrole == "admin")
            {
                if (e.RowIndex >= 0)

                {

                    DataGridViewRow row = this.dgvItem.Rows[e.RowIndex];



                    txtItemId.Text = row.Cells[0].Value.ToString();

                    txtItemName.Text = row.Cells[1].Value.ToString();

                    txtPrice.Text = row.Cells[2].Value.ToString();
                    txtQuantity.Text = row.Cells[3].Value.ToString();
                    txtSupplierName.Text = row.Cells[4].Value.ToString();
                }
            else
            {
                    
                 //   txtItemId.Enabled = false;
                    txtItemName.Enabled = false;
                    txtPrice.Enabled = false;
                    txtQuantity.Enabled = false;
                    txtSupplierName.Enabled = false;
                }
           



            }
        }
    }
}
