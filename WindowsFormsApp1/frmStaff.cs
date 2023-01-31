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
    public partial class frmStaff : Form
    {
        OleDbConnection connection = new OleDbConnection();
        
        public frmStaff()
        {
            InitializeComponent();
            
            

            
           connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStaffId.Text.Length > 0 && txtContactnumber.Text.Length >0 && txtJobStatus.Text.Length >0 && txtSUsername.Text.Length >0 && txtSPassword.Text.Length > 0)
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;

                    String staff = "INSERT INTO Staff  VALUES('" + txtStaffId.Text + "','" + this.dtpDate.Text + "','" + txtContactnumber.Text + "','" + txtJobStatus.Text + "','" + txtSUsername.Text + "','" + txtSPassword.Text + "')";
                    command = new OleDbCommand(staff, connection);
                    //command.CommandText = "SELECT JobStatus FROM Staff WHERE Username ='" + txtSUsername.Text + "'and Password='" + txtSPassword.Text + "'";
                    // OleDbDataReader reader = command.ExecuteReader();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved");

                    txtStaffId.Clear();
                    txtContactnumber.Clear();
                    txtJobStatus.Clear();
                    txtSUsername.Clear();
                    txtSPassword.Clear();
                    dgvStaff.Rows.Clear();
                    dgvStaff.Refresh();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Please enter values ");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;



            String query = "update [Staff] set [DateofBirth] =@dateOfBirth, [ContactNumber] =@cnumber, [JobStatus] =@jsatus, [Username] =@username, [Password] =@password where [StaffID] = @id";

            command.CommandText = query;
            command.Parameters.AddRange(new OleDbParameter[]
                   {
                       new OleDbParameter("@dateOfBirth", dtpDate.Text),
                       new OleDbParameter("@cnumbe", txtContactnumber.Text),
                       new OleDbParameter("@jsatus", txtJobStatus.Text),
                       new OleDbParameter("@username", txtSUsername.Text),
                       new OleDbParameter("@password", txtSPassword.Text),
                       new OleDbParameter("@id", int.Parse(txtStaffId.Text))
                   });
            command.ExecuteNonQuery();
            MessageBox.Show("Data Updated Successfully");
            txtStaffId.Clear();
            txtContactnumber.Clear();
            txtJobStatus.Clear();
            txtSUsername.Clear();
            txtSPassword.Clear();
            dgvStaff.Rows.Clear();
            dgvStaff.Refresh();

            connection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            String query = "DELETE FROM Staff WHERE StaffID=" + txtStaffId.Text + "";
            command.CommandText = query;
            command.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            dgvStaff.Rows.Clear();
            dgvStaff.Refresh();

            txtStaffId.Clear();
            txtContactnumber.Clear();
            txtJobStatus.Clear();
            txtSUsername.Clear();
            txtSPassword.Clear();
            connection.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            connection.Open();
            String query = "SELECT * FROM Staff";
            OleDbCommand command = new OleDbCommand(query);
            command.Connection = connection;
            
           // command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dgvStaff.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
            }
            reader.Close();
           // OleDbDataAdapter da = new OleDbDataAdapter(command);
           // DataTable dt = new DataTable();
          //  da.Fill(dt);
           // dgvStaff.DataSource = dt;
            connection.Close();
        }

        private void dgvStaff_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)

            {

                DataGridViewRow row = this.dgvStaff.Rows[e.RowIndex];

               

                    txtStaffId.Text = row.Cells[0].Value.ToString();

                dtpDate.Text = row.Cells[1].Value.ToString();

                txtContactnumber.Text = row.Cells[2].Value.ToString();

                txtJobStatus.Text = row.Cells[3].Value.ToString();
                txtSUsername.Text = row.Cells[4].Value.ToString();
                txtSPassword.Text = row.Cells[5].Value.ToString();

            }

        }

       
    }
}
