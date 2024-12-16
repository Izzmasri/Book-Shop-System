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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            FillChart();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Books Obj = new Books();
            Obj.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Asus\OneDrive\Desktop\Visual apps\BookShop.accdb;";
        private void Dashboard_Load(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                using (OleDbCommand cmd1 = new OleDbCommand("SELECT SUM(BQty) FROM Books", conn))
                {
                    object bookQuantityObj = cmd1.ExecuteScalar();
                    if (bookQuantityObj != DBNull.Value)
                    {
                        int bookQuantity = Convert.ToInt32(bookQuantityObj);
                        BooksStockLbl.Text = bookQuantity.ToString();
                    }
                    else
                    {
                        BooksStockLbl.Text = "0";
                    }
                }

                using (OleDbCommand cmd2 = new OleDbCommand("SELECT SUM(Amount) FROM Bill", conn))
                {
                    object totalAmountObj = cmd2.ExecuteScalar();
                    if (totalAmountObj != DBNull.Value)
                    {
                        int totalAmount = Convert.ToInt32(totalAmountObj);
                        AmountLbl.Text = totalAmount.ToString();
                    }
                    else
                    {
                        AmountLbl.Text = "0";
                    }
                }

                using (OleDbCommand cmd3 = new OleDbCommand("SELECT COUNT(*) FROM Users", conn))
                {
                    int userCount = (int)cmd3.ExecuteScalar();
                    UserTotalLbl.Text = userCount.ToString();
                }
            }
            
        }
        void FillChart()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Asus\OneDrive\Desktop\Visual apps\BookShop.accdb;";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OleDbDataAdapter sda = new OleDbDataAdapter("SELECT BCat, BQty FROM Books", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // Assigning DataTable as the DataSource for the chart
                    chart1.DataSource = dt;

                    // Setting X and Y value members for the series
                    chart1.Series["chart1"].XValueMember = "BCat"; // Assuming BCat represents Category
                    chart1.Series["chart1"].YValueMembers = "BQty"; // Assuming BQty represents Quantity

                    // Adding a title to the chart
                    chart1.Titles.Add("Cat/Qty");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }  

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
