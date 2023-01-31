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
    public partial class frmCategory : Form
    {
        OleDbConnection connection = new OleDbConnection();
        string jobrole;
        public frmCategory(string jobRole)
        {
            InitializeComponent();
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";
            this.jobrole = jobRole;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (jobrole == "admin")
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                String category = "INSERT INTO Category  VALUES('" + txtCategoryId.Text + "','" + txtCategoryType.Text + "','"  + txtDescription.Text + "')";
                command = new OleDbCommand(category, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Data Saved");
                dgvCategory.Rows.Clear();
                dgvCategory.Refresh();
                connection.Close();

            }
            else
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String query = "update [Category] set [Categorytype] =@ctype, [Description] =@description where [CategoryID] = @id";
            command.CommandText = query;
            command.Parameters.AddRange(new OleDbParameter[]
                   {
                       new OleDbParameter("@ctype", txtCategoryType.Text),
                       new OleDbParameter("@description", txtDescription.Text),
                      
                       new OleDbParameter("@id", int.Parse(txtCategoryId.Text))
                   });
            command.ExecuteNonQuery();
            MessageBox.Show("Data Updated Successfully");
            dgvCategory.Rows.Clear();
            dgvCategory.Refresh();
            txtCategoryId.Clear();
            txtCategoryType.Clear();
            txtDescription.Clear();
            connection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String query = "DELETE FROM Category WHERE CategoryID=" + txtCategoryId.Text + "";
            command.CommandText = query;
            command.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            dgvCategory.Rows.Clear();
            dgvCategory.Refresh();
            connection.Close();
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            connection.Open();

            String query = "SELECT * FROM Category";
            OleDbCommand command = new OleDbCommand(query);
            command.Connection = connection;

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dgvCategory.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            reader.Close();
           
            connection.Close();

        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (jobrole == "admin")
            {
                if (e.RowIndex >= 0)

                {

                    DataGridViewRow row = this.dgvCategory.Rows[e.RowIndex];



                    txtCategoryId.Text = row.Cells[0].Value.ToString();

                    txtCategoryType.Text = row.Cells[1].Value.ToString();

                    txtDescription.Text = row.Cells[2].Value.ToString();



                }
            }
            else
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
               
            }
           
        }
    }
}
