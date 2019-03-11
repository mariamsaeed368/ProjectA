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
    public partial class GroupProject : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
        public GroupProject()
        {
            InitializeComponent();
        }
        public GroupProject(int id)
        {
            InitializeComponent();
            textBox1.Text = id.ToString();
        }


        private void GroupProject_Load(object sender, EventArgs e)
        {
            display_data();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Project p = new Project();
            p.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("Select Id from [ProjectA].[dbo].[Group]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["Id"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into GroupProject(ProjectId,GroupId,AssignmentDate) values('" +textBox1.Text+ "','" + comboBox1.Text+ "','" + dateTimePicker1.Value.ToShortDateString() + "')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data saved");
            conn.Close();
            display_data();
            textBox1.Text = " ";
            comboBox1.Text = " ";
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from GroupProject";
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
            if (dataGridView1.CurrentRow.Cells["ProjectId"].Value != DBNull.Value)
            {
                string conn = @"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True";

                if (MessageBox.Show("Are You Sure to Delete this Record ?", "DataGridView", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (SqlConnection sqlCon = new SqlConnection(conn))
                    {
                        sqlCon.Open();
                        SqlCommand cmd = new SqlCommand("EmployeeDeleteByID", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProjectId"].Value);
                        int g_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["GroupId"].Value);
                        cmd.CommandText = "Delete from GroupProject where ProjectId='" + rowID + "' AND GroupId='"+g_id+"'";
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
            if (dataGridView1.CurrentRow != null)
            {

                DataGridViewRow dgvRow = dataGridView1.CurrentRow;
                int id = Convert.ToInt32(dgvRow.Cells["ProjectId"].Value);
                int g_id= Convert.ToInt32(dataGridView1.CurrentRow.Cells["GroupId"].Value);
                DateTime dt = Convert.ToDateTime(dgvRow.Cells["AssignmentDate"].Value == DBNull.Value ? "" : dgvRow.Cells["AssignmentDate"].Value);
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("Update [ProjectA].[dbo].[GroupProject] set AssignmentDate='" + dt + "' where ProjectId='" + id + "' AND GroupId='"+g_id+"'", conn);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                }
                catch(Exception d)
                {
                    MessageBox.Show("Please Enter valid date (Hint: d/m/yy)");
                }

               
            }
            conn.Close();
            display_data();
        }
    }
}
