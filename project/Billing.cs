using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }
        private OleDbConnection conn = new OleDbConnection();
        private OleDbDataAdapter sda = new OleDbDataAdapter();
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Asus\OneDrive\Desktop\Visual apps\BookShop.accdb;";

        private void populate()
        {
            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open();
                string query = "SELECT * FROM Books";
                sda = new OleDbDataAdapter(query, conn);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(sda);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                BookDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }


        }
        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            try
            {
                conn.Open();
                string query = "UPDATE Books SET BQty=@BQty WHERE BId=@BId";

                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@BQty", newQty);
                cmd.Parameters.AddWithValue("@BId", key);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Book Updated Successfully");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                conn.Close();

                populate();
                Reset();
            }
        }
        int n = 0, Gradtotal=0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            
            if(QtyTb.Text=="" || Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = PriceTb.Text;
                newRow.Cells[4].Value = total;
                 BillDGV.Rows.Add(newRow);
                n++;
                UpdateBook();
                Gradtotal = Gradtotal + total;
                Totallbl.Text = "TL"+Gradtotal;
            }
        }
        int key = 0, stock = 0;
        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = BookDGV.Rows[e.RowIndex];

                BTitleTb.Text = selectedRow.Cells[1].Value?.ToString();
                PriceTb.Text = selectedRow.Cells[5].Value?.ToString();


                key = string.IsNullOrEmpty(BTitleTb.Text) ? 0 : Convert.ToInt32(selectedRow.Cells[0].Value);
                stock = string.IsNullOrEmpty(BTitleTb.Text) ? 0 : Convert.ToInt32(selectedRow.Cells[4].Value);
            }
        }
        private void Reset()
        {
            BTitleTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            ClientNameTb.Text = "";

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        int prodid, prodprice, prodqty, tottal, pos = 60;
        string prodname;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int pos = 100;
            int Gradtotal = 0;

            e.Graphics.DrawString("Book Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));

            // Assuming BillDGV is a DataGridView bound to a Microsoft Access database table

            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                int prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                string prodname = Convert.ToString(row.Cells["Column2"].Value);
                int prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                int prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                int total = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));

                pos = pos + 20;
                Gradtotal += total;
            }

            e.Graphics.DrawString("Grand Total: TL" + Gradtotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos + 50));
            e.Graphics.DrawString("***********BookStore***************", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(235, pos + 85));

            // Assuming BillDGV is a DataGridView that needs to be cleared after printing
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLbl.Text = Form1.UserName;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

           
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO Bill (UName, ClientName, Amount) VALUES (@UName, @ClientName, @Gradtotal)";
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        cmd.Parameters.AddWithValue("@UName", UserNameLbl.Text);
                        cmd.Parameters.AddWithValue("@ClientName", ClientNameTb.Text);
                        cmd.Parameters.AddWithValue("@Gradtotal", Gradtotal);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bill Saved Successfully");
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
          
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
