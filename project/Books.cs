using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.CodeDom;

namespace project
{
    public partial class Books : Form
    {

        public Books()
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
        private void Filter()
        {

            conn.Open();
            string query = "SELECT* FROM Books WHERE Bcat = '" + CatCbSearchCb.SelectedItem.ToString() + "'";
            sda = new OleDbDataAdapter(query, conn);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {


            if (BTitleTb.Text == "" || BautTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BCatCb.SelectedIndex == -1)
            {

                MessageBox.Show("Missing Information");

            }

            else
            {

                try
                {
                    conn.Open();
                    string query = "INSERT INTO Books (BTitle, BAuther, BCat, BQty, BPrice) VALUES(@BTitle, @BAuther, @BCat, @BQty, @BPrice)";

                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BTitle", BTitleTb.Text);
                    cmd.Parameters.AddWithValue("@BAuther", BautTb.Text);
                    cmd.Parameters.AddWithValue("@BCat", BCatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@BQty", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@BPrice", PriceTb.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Book Saved Successfully");
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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Books Obj = new Books();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void Books_Load(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            BTitleTb.Text = "";
            BautTb.Text = "";
            BCatCb.SelectedIndex = -1;
            PriceTb.Text = "";
            QtyTb.Text = "";
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();

        }
        int key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = BookDGV.Rows[e.RowIndex];

                BTitleTb.Text = selectedRow.Cells[1].Value?.ToString();
                BautTb.Text = selectedRow.Cells[2].Value?.ToString();
                BCatCb.SelectedItem = selectedRow.Cells[3].Value?.ToString();
                QtyTb.Text = selectedRow.Cells[4].Value?.ToString();
                PriceTb.Text = selectedRow.Cells[5].Value?.ToString();

                
                key = string.IsNullOrEmpty(BTitleTb.Text) ? 0 : Convert.ToInt32(selectedRow.Cells[0].Value);
            }

        }


        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (BTitleTb.Text == "" || BautTb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "" || BCatCb.SelectedIndex == -1)
            {

                MessageBox.Show("Missing Information");

            }

            else
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Books SET BTitle=@BTitle, BAuther=@BAuther, BCat=@BCat, BQty=@BQty, BPrice=@BPrice WHERE BId=@BId";

                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BTitle", BTitleTb.Text);
                    cmd.Parameters.AddWithValue("@BAuther", BautTb.Text);
                    cmd.Parameters.AddWithValue("@BCat", BCatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@BQty", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@BPrice", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@BId", key); 

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Book Updated Successfully");
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
        }

        private void CatCbSearchCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key==0)
            {

                MessageBox.Show("Missing Information");

            }

            else
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Books WHERE BId=" + key + ";";

                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BTitle", BTitleTb.Text);
                    cmd.Parameters.AddWithValue("@BAuther", BautTb.Text);
                    cmd.Parameters.AddWithValue("@BCat", BCatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@BQty", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@BPrice", PriceTb.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Book Deleted Successfully");
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
        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }


        private void BTitleTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
