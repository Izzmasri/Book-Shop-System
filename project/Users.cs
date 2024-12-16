using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();
        }
        private OleDbConnection conn = new OleDbConnection();
        private OleDbDataAdapter sda = new OleDbDataAdapter();
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Asus\OneDrive\Desktop\Visual apps\BookShop.accdb;";
        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void populate()
        {
            conn = new OleDbConnection(connectionString);
            try
            {
                conn.Open();
                string query = "SELECT * FROM Users";
                sda = new OleDbDataAdapter(query, conn);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(sda);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                UserDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }


        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || AddTb.Text == "" || PassTb.Text == "" || PhoneTb.Text == "")
            {

                MessageBox.Show("Missing Information");

            }

            else
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Users (UName, UPhone, UAdd, UPass) VALUES(@UName, @UPhone, @UAdd, @UPass)";

                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UName", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UPhone", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UAdd", AddTb.Text);
                    cmd.Parameters.AddWithValue("@UPass", PassTb.Text);
                    

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User Saved Successfully");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    conn.Close();
                    populate();
                    //Reset();
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || AddTb.Text == "" || PassTb.Text == "" || PhoneTb.Text == "")
            {

                MessageBox.Show("Missing Information");

            }

            else
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Books SET UName=@UName, UPhone=@UPhone, UAdd=@UAdd, UPass=@UPass WHERE UId=@UId";

                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UName", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UPhone", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UAdd", AddTb.Text);
                    cmd.Parameters.AddWithValue("@UPass", PassTb.Text);
                    cmd.Parameters.AddWithValue("@UId", key);

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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {

                MessageBox.Show("Missing Information");

            }

            else
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Users WHERE UId=" + key + ";";

                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UName", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UPhone", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UAdd", AddTb.Text);
                    cmd.Parameters.AddWithValue("@UPass", PassTb.Text);

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
        private void Reset()
        {
            UnameTb.Text = "";
            PhoneTb.Text = "";
            AddTb.Text = "";
            PassTb.Text = "";
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();    
        }
        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = UserDGV.Rows[e.RowIndex];

                UnameTb.Text = selectedRow.Cells[1].Value?.ToString();
                PhoneTb.Text = selectedRow.Cells[2].Value?.ToString();
                AddTb.Text = selectedRow.Cells[3].Value?.ToString();
                PassTb.Text = selectedRow.Cells[4].Value?.ToString();


                key = string.IsNullOrEmpty(UnameTb.Text) ? 0 : Convert.ToInt32(selectedRow.Cells[0].Value);
            }
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

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
