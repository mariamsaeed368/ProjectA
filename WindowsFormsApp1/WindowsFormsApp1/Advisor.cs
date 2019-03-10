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
    public partial class Advisor : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source = SONY\SQLEXPRESS; Initial Catalog = ProjectA; Integrated Security = True");
        public Advisor()
        {
            InitializeComponent();
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Advisor";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand comd = new SqlCommand("Select Id from Lookup where Lookup.Value='" + comboBox1.Text + "'", conn);
            int g = (int)comd.ExecuteScalar();
            SqlCommand cmd = new SqlCommand("Insert into Advisor(Id,Designation,Salary) values('" + textBox1.Text + "', '" + g + "','" + textBox2.Text + "')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data saved");
            conn.Close();
            display_data();
            textBox1.Text = " ";
            textBox2.Text = " ";
            comboBox1.Text = " ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                conn.Open();
                DataGridViewRow dgvRow = dataGridView1.CurrentRow;
                int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
                int des = Convert.ToInt32(dgvRow.Cells["Designation"].Value == DBNull.Value ? "" : dgvRow.Cells["Designation"].Value);
                int sal = Convert.ToInt32(dgvRow.Cells["Salary"].Value == DBNull.Value ? "" : dgvRow.Cells["Salary"].Value);
                SqlCommand sqlCmd = new SqlCommand("Update Advisor set Designation='" + des + "',Salary='" + sal + "' where Id='" + id + "'", conn);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
                conn.Close();
                display_data();

            }
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
                        SqlCommand cmd = new SqlCommand("EmployeeDeleteByID", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                        cmd.CommandText = "Delete from Advisor where Id='" + rowID + "'";
                        cmd.ExecuteNonQuery();
                        display_data();
                        MessageBox.Show("Deleted");
                        sqlCon.Close();
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dataGridView1.CurrentRow;
            int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
            ProjectAdvisor g = new ProjectAdvisor(id);
            g.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }

        private void Advisor_Load(object sender, EventArgs e)
        {
            display_data();
        }
    }
}
