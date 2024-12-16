using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
        }
        private OleDbConnection conn = new OleDbConnection();
        private OleDbDataAdapter sda = new OleDbDataAdapter();
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Asus\OneDrive\Desktop\Visual apps\BookShop.accdb;";
        public static string UserName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            conn = new OleDbConnection(connectionString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM Users WHERE UName = @uname AND UPass = @upass", conn);
            cmd.Parameters.AddWithValue("@uname", UnameTb.Text);
            cmd.Parameters.AddWithValue("@upass", UPassTb.Text);

            int count = (int)cmd.ExecuteScalar();

            if (count == 1)
            {
                UserName = UnameTb.Text;
                Billing obj = new Billing();
                obj.Show();
                this.Hide();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }

        private void UPassTb_TextChanged(object sender, EventArgs e)
        {

        }
    }

       
    
}
