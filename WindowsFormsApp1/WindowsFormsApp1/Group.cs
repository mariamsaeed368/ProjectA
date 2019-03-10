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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1
{
    public partial class Group : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");

        public Group(int id)
        {
            InitializeComponent();
            textBox1.Text = id.ToString();
        }

        private void Group_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("Select Id from [ProjectA].[dbo].[Group]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt.Rows[i]["Id"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand comd = new SqlCommand("Select Id from Lookup where Lookup.Value='" + comboBox1.Text + "'", conn);
            int g = (int)comd.ExecuteScalar();
            SqlCommand cmd1 = new SqlCommand("Insert into GroupStudent(GroupId,StudentId,Status,AssignmentDate) values('" + comboBox2.Text + "','" + textBox1.Text + "','" + g + "','" + dateTimePicker1.Value.ToShortDateString()+ "')", conn);
            MessageBox.Show("Data saved");
            cmd1.ExecuteNonQuery();
            conn.Close();
            display_data();
            textBox1.Text = " ";
            comboBox1.Text = " ";
            comboBox2.Text = " ";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (dataGridView1.CurrentRow != null)
            {
               // conn.Open();
                DataGridViewRow dgvRow = dataGridView1.CurrentRow;
                int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
                DateTime dt = Convert.ToDateTime(dgvRow.Cells["Created_On"].Value == DBNull.Value ? "" : dgvRow.Cells["Created_On"].Value);
                string role = dgvRow.Cells["Status"].Value == DBNull.Value ? "" : dgvRow.Cells["Status"].Value.ToString();
                if(role != "3" && role != "4")
                {
                    MessageBox.Show("Please Enter the valid Role (Hint : Select 3 or 4)");
                }
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("Update [ProjectA].[dbo].[Group] set Created_On='" + dt + "' where Id='" + id + "'", conn);
                    //try
                    //{
                        SqlCommand sqlCmd2 = new SqlCommand("Update [ProjectA].[dbo].[GroupStudent] set AssignmentDate='" + dt + "',Status='" + role + "' where GroupId='" + id + "'", conn);
                        sqlCmd2.ExecuteNonQuery();
                    //}
                    //catch (Exception d)
                    //{
                      //  MessageBox.Show("Please Enter the valid Role (Hint : Select 11,12 or 14)");
                  //  }
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                   // conn.Close();   
                }
            }
            conn.Close();
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
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                        cmd.CommandText = "Delete from  [ProjectA].[dbo].[Group] where Id='" + rowID + "'";
                        cmd2.CommandText = "Delete from GroupStudent where GroupId='" + rowID + "'";
                        cmd2.ExecuteNonQuery();
                        cmd.ExecuteNonQuery();
                        display_data();
                        MessageBox.Show("Deleted");
                        sqlCon.Close();
                    }
                }
            }
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  a.*,s.Status,s.AssignmentDate,s.StudentId,s.GroupId FROM[ProjectA].[dbo].[Group] as a JOIN GroupStudent s ON a.Id = s.GroupId";
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            display_data();
        }
    }
}
