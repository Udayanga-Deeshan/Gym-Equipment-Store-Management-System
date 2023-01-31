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
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string jobStatus="";
            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Staff WHERE Username='" + txtUsername.Text + "' and Password='" + txtPassword.Text + "'";
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read() == true)
                {
                    jobStatus = reader["JobStatus"].ToString();
                    
                    MessageBox.Show("Username and password is correct");
                    txtUsername.Clear();
                    txtPassword.Clear();
                    frmDashboard frm = new frmDashboard(txtUsername.Text,jobStatus);
                    frm.Show();
                }


                else
                {
                    MessageBox.Show("Username and password is not correct");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();

            //int count = 0;
            //while (reader.Read())
            //{
            //    count = count++;
            //}











        }
    }
}
