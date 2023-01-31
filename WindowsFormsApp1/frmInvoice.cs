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
    public partial class frmInvoice : Form
    {
        OleDbConnection connection = new OleDbConnection();
        DataTable db = new DataTable();
        public frmInvoice()
        {
            InitializeComponent();
            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\acer\\Documents\\ICT Project.accdb";
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Item where Itemname LIKE '"+txtsearch.Text+"%'",connection);
             DataTable dt = new DataTable();
            da.Fill(dt);
            dgvInvoice.DataSource = dt;
            
           


            connection.Close();
            // dgvInvoice.Rows.Clear();
            //dgvInvoice.Refresh();
        }

        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var o = dgvInvoice.Rows[e.RowIndex];
            txtItemName.Text = o.Cells[1].Value.ToString();
            txtPrice.Text = o.Cells[2].Value.ToString();
            lblStock.Text = o.Cells[3].Value.ToString();

            
        }

        private void btnAddtoCart_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            if (  txtQuantity.Text.Length > 0)
            {
                if (Convert.ToInt32(lblStock.Text) >= Convert.ToInt32(txtQuantity.Text))
                {
                    String query = "update Item set Quantity= Quantity -@quantity where ItemName= @id";
                    command.CommandText = query;
                    command.Parameters.AddRange(new OleDbParameter[]
                          {
                       new OleDbParameter("@quantity", txtQuantity.Text),
                       new OleDbParameter("@id", txtItemName.Text),



                          });


                    string fourthColum = txtItemName.Text;
                    string fifthColum = txtQuantity.Text;
                    string priceColum = txtPrice.Text;
                    int x = Convert.ToInt32(txtQuantity.Text);
                    int y = Convert.ToInt32(txtPrice.Text);
                    string totalcolum = (x * y).ToString();
                    string[] row = { fourthColum, fifthColum, priceColum, totalcolum };
                    dgvaddtocart.Rows.Add(row);

                    command.ExecuteNonQuery();

                    var currentFullTotal = 0;

                    foreach (DataGridViewRow item in dgvaddtocart.Rows)
                    {
                        currentFullTotal += Convert.ToInt32(item.Cells["Total"].Value);
                    }

                    lbltotal.Text = currentFullTotal.ToString();
                    
                }
                else
                {
                    MessageBox.Show("Stock quantity is Not sufficient");
                }
            }
            else
            {
                MessageBox.Show("Please Enter the details");
            }
           


            //String invoice = "INSERT INTO Invoice  VALUES('" + txtInvoiceNumber.Text + "','" + txtCustomerName.Text + "','" + txtAdddress.Text + "','"+txtItemName.Text + "','" +txtQuantity.Text + "','" + txtPrice.Text +)";
            //command = new OleDbCommand(invoice, connection);
            //  command.ExecuteNonQuery();
            //  MessageBox.Show("Data Saved");


            //dgvInvoice.Rows.Clear();
            //dgvInvoice.Refresh();
            


            connection.Close();
            
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connection.Open();
           
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            if (txtInvoiceNumber.Text.Length > 0 && txtCustomerName.Text.Length > 0 && txtAdddress.Text.Length > 0)
            {
                String invoice = "INSERT INTO Invoice  VALUES('" + txtInvoiceNumber.Text + "','" + txtCustomerName.Text + "','" + txtAdddress.Text + "','" + lbltotal.Text + "')";
                command = new OleDbCommand(invoice, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Data Saved");
                
                

                txtInvoiceNumber.Clear();
                txtCustomerName.Clear();
                txtAdddress.Clear();
                txtQuantity.Clear();
                txtPrice.Clear();
                dgvaddtocart.Rows.Clear();
                dgvaddtocart.Refresh();



            }
            else
            {
                MessageBox.Show("Some Details are Missing Please check again");
            }
           
            connection.Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            frmSalesReport frm = new frmSalesReport();
            frm.Show();
            connection.Open();
            String query = "SELECT * FROM Invoice";
            OleDbCommand command = new OleDbCommand(query);
            command.Connection = connection;
            
            
           
            connection.Close();
        }
        public static String ttl;
        //public static String customername;
        private void btnPrint_Click(object sender, EventArgs e)
        {
           
                ttl = lbltotal.Text;

                frmPrintBill frm = new frmPrintBill();
                frm.Show();
           
           
           
        }
    }
}
