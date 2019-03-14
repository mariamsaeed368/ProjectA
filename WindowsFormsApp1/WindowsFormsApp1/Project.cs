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
    public partial class Project : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
        public Project()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into Project(Description,Title) values('" + richTextBox1.Text + "','" + textBox1.Text + "')", conn);
            cmd.ExecuteNonQuery();
            /* SqlCommand cmd3 = new SqlCommand("Select IDENT_CURRENT('Project')", conn);
             //  cmd3.ExecuteNonQuery();*/
            // int modified = Convert.ToInt32(cmd3.ExecuteScalar());
            MessageBox.Show("Data saved");
            conn.Close();
            display_data();
            textBox1.Text = " ";
            richTextBox1.Text = " ";

        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Project";
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
                        SqlCommand cmd2 = new SqlCommand("DeleteByID", sqlCon);
                        cmd2.CommandType = CommandType.Text;
                        SqlCommand cmd3 = new SqlCommand("DeleteByID", sqlCon);
                        cmd2.CommandType = CommandType.Text;
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                        cmd.CommandText = "Delete from Project where Id='" + rowID + "'";
                        cmd2.CommandText = "Delete from GroupProject where ProjectId='" + rowID + "'";
                        cmd2.ExecuteNonQuery();
                        cmd3.CommandText="Delete from ProjectAdvisor where ProjectId='"+rowID+"'";
                        cmd3.ExecuteNonQuery();
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

            if (dataGridView1.CurrentRow != null)
            {
                conn.Open();
                DataGridViewRow dgvRow = dataGridView1.CurrentRow;
                int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
                string title = dgvRow.Cells["Title"].Value == DBNull.Value ? "" : dgvRow.Cells["Title"].Value.ToString();
                string desc = dgvRow.Cells["Description"].Value == DBNull.Value ? "" : dgvRow.Cells["Description"].Value.ToString();
                SqlCommand sqlCmd = new SqlCommand("Update Project set Title='" + title + "' ,Description='" + desc + "' where Id='" + id + "'", conn);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
                conn.Close();
                display_data();

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dataGridView1.CurrentRow;
            int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
            GroupProject g = new GroupProject(id);
            g.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            display_data();
        }

        private void Project_Load(object sender, EventArgs e)
        {
            display_data();
        }
    }
}
