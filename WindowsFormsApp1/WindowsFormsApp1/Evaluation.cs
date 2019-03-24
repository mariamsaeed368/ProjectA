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
    public partial class Evaluation : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
        public Evaluation()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into Evaluation(Name,TotalMarks,TotalWeightage) values('" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data saved");
            conn.Close();
            display_data();
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Evaluation";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void Evaluation_Load(object sender, EventArgs e)
        {
            display_data();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                conn.Open();
                DataGridViewRow dgvRow = dataGridView1.CurrentRow;
                int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
                int t_marks = Convert.ToInt32(dgvRow.Cells["TotalMarks"].Value);
                int t_weight= Convert.ToInt32(dgvRow.Cells["TotalWeightage"].Value);
                string title = dgvRow.Cells["Name"].Value == DBNull.Value ? "" : dgvRow.Cells["Name"].Value.ToString();
                SqlCommand sqlCmd = new SqlCommand("Update Evaluation set Name='" + title + "' ,TotalMarks='" + t_marks + "',TotalWeightage='"+t_weight+"' where Id='" + id + "'", conn);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
                conn.Close();
                display_data();

            }
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
                        SqlCommand cmd1 = new SqlCommand("DeleteByID", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                        cmd.CommandText = "Delete from Evaluation where Id='" + rowID + "'";
                        cmd1.CommandText="Delete from GroupEvaluation where EvaluationId='"+rowID+"'";
                        cmd1.ExecuteNonQuery();
                        cmd.ExecuteNonQuery();
                        display_data();
                        MessageBox.Show("Deleted");
                        sqlCon.Close();
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            display_data();
        }

            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvRow = dataGridView1.CurrentRow;
            int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
            GroupEvaluation g = new GroupEvaluation(id);
            g.Show();
            this.Hide();
        }
    }
}
