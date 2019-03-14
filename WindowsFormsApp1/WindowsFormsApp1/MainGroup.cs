using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class MainGroup : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
        public MainGroup()
        {
            InitializeComponent();
        }

        private void MainGroup_Load(object sender, EventArgs e)
        {
            display_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into [ProjectA].[dbo].[Group] (Created_On) values('" + textBox1.Text + "')", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data saved");
            }
            catch(Exception h)
            {
                MessageBox.Show("Please Enter valid date (Hint: mm/dd/yyyy");
            } 
            conn.Close();
            display_data();
            textBox1.Text = " ";
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM[ProjectA].[dbo].[Group]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            display_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["Id"].Value != DBNull.Value)
            {
                string conn = @"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True";

                if (MessageBox.Show("Are You Sure to Delete this Record ?", "DataGridView", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlCon = new SqlConnection(conn))
                    {
                        sqlCon.Open();
                        SqlCommand cmd = new SqlCommand("DeleteByID", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                        cmd.CommandText = "Delete from  [ProjectA].[dbo].[Group] where Id='" + rowID + "'";
                        cmd.ExecuteNonQuery();
                        display_data();
                        MessageBox.Show("Deleted");
                        sqlCon.Close();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            DataGridViewRow dgvRow = dataGridView1.CurrentRow;
            int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
            DateTime dt = Convert.ToDateTime(dgvRow.Cells["Created_On"].Value == DBNull.Value ? "" : dgvRow.Cells["Created_On"].Value);
            SqlCommand sqlCmd = new SqlCommand("Update [ProjectA].[dbo].[Group]  set Created_On='" +dt + "' where Id='" + id + "'", conn);
            sqlCmd.ExecuteNonQuery();
            MessageBox.Show("Updated");
            conn.Close();
            display_data();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }
    }
}
