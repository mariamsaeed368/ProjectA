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
    public partial class ProjectAdvisor : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
        public ProjectAdvisor()
        {
            InitializeComponent();
        }
        public ProjectAdvisor(int id)
        {
            InitializeComponent();
            textBox1.Text = id.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand comd = new SqlCommand("Select Id from Lookup where Lookup.Value='" + comboBox2.Text + "'", conn);
            int g = (int)comd.ExecuteScalar();
            SqlCommand cmd = new SqlCommand("Insert into ProjectAdvisor(AdvisorId, ProjectId,AdvisorRole,AssignmentDate) values('" + textBox1.Text + "', '" + comboBox1.Text + "', '" + g + "', '" + dateTimePicker1.Value.ToShortDateString() + "')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data saved");
            conn.Close();
            display_data();
            textBox1.Text = " ";
            comboBox1.Text = " ";
            comboBox2.Text = " ";
            //dateTimePicker1.Text = " ";
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  s.AdvisorId,a.*,s.AdvisorRole,s.AssignmentDate,s.ProjectId FROM[ProjectA].[dbo].[Project] as a JOIN ProjectAdvisor s ON a.Id = s.ProjectId";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void ProjectAdvisor_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("Select Id from Project", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["Id"]);
            }
            display_data();
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
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["AdvisorId"].Value);
                        // cmd.CommandText = "Delete from  [ProjectA].[dbo].[Project] where Id='" + rowID + "'";
                        cmd2.CommandText = "Delete from ProjectAdvisor where AdvisorId='" + rowID + "'";
                        cmd2.ExecuteNonQuery();
                        // cmd.ExecuteNonQuery();
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
                int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
                int aid = Convert.ToInt32(dgvRow.Cells["AdvisorId"].Value);
                // string desc = dgvRow.Cells["Description"].Value == DBNull.Value ? "" : dgvRow.Cells["Description"].Value.ToString();
                //  string title = dgvRow.Cells["Title"].Value == DBNull.Value ? "" : dgvRow.Cells["Title"].Value.ToString();
                DateTime dt = Convert.ToDateTime(dgvRow.Cells["AssignmentDate"].Value == DBNull.Value ? "" : dgvRow.Cells["AssignmentDate"].Value);
                string role = dgvRow.Cells["AdvisorRole"].Value == DBNull.Value ? "" : dgvRow.Cells["AdvisorRole"].Value.ToString();
                if (role != "11" && role != "12" && role != "14")
                {
                    MessageBox.Show("Please Enter the valid Role (Hint : Select 11,12 or 14)");
                }
                else
                {
                    /*SqlCommand comd = new SqlCommand("Select Id from Lookup where Lookup.Value='" + role + "'", conn);
                    int g = (int)comd.ExecuteScalar();*/
                    // SqlCommand sqlCmd = new SqlCommand("Update [ProjectA].[dbo].[Project] set Title='" + title + "',Description='" + desc + "' where Id='" + id + "'", conn);
                    //  try
                    //   {
                    SqlCommand sqlCmd2 = new SqlCommand("Update ProjectAdvisor set AssignmentDate='" + dt + "',AdvisorRole='" + role + "' where AdvisorId='" + aid + "' AND ProjectId='" + id + "'", conn);
                    sqlCmd2.ExecuteNonQuery();

                    //  }
                    // catch (Exception d)
                    //  {
                    //     MessageBox.Show("Please Enter the valid Role (Hint : Select 11,12 or 14)");
                    //  }
                    //  sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");

                }

            }
            conn.Close();
            display_data();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Advisor a = new Advisor();
            a.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Project p = new Project();
            p.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
