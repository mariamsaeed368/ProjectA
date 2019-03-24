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
    public partial class GroupEvaluation : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
        public GroupEvaluation()
        {
            InitializeComponent();
        }
        public GroupEvaluation(int id)
        {
            InitializeComponent();
            textBox1.Text = id.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Evaluation f = new Evaluation();
            f.Show();
            this.Hide();
        }
        public void display_data()
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  a.*,s.EvaluationDate,s.GroupId,s.ObtainedMarks,s.EvaluationId FROM[ProjectA].[dbo].[Evaluation] as a JOIN GroupEvaluation s ON a.Id = s.EvaluationId";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void GroupEvaluation_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
            SqlDataAdapter da = new SqlDataAdapter("Select Id from [ProjectA].[dbo].[Group]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["Id"]);
            }
            display_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into GroupEvaluation(GroupId,EvaluationId,ObtainedMarks,EvaluationDate) values('" + comboBox1.Text + "','" + textBox1.Text + "','"+textBox2.Text+"','" + dateTimePicker1.Value.ToShortDateString() + "')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data saved");
            conn.Close();
            display_data();
            textBox1.Text = " ";
            comboBox1.Text = " ";
            textBox2.Text = " ";
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
                        int rowID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["EvaluationId"].Value);
                        int g_id= Convert.ToInt32(dataGridView1.CurrentRow.Cells["GroupId"].Value);
                        cmd.CommandText = "Delete from  [ProjectA].[dbo].[GroupEvaluation] where EvaluationId='" + rowID + "' AND GroupId='"+g_id+"'";
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
                int id = Convert.ToInt32(dgvRow.Cells["EvaluationId"].Value);
                int obt_marks = Convert.ToInt32(dgvRow.Cells["ObtainedMarks"].Value);
                int g_id = Convert.ToInt32(dgvRow.Cells["GroupId"].Value);
                DateTime dt = Convert.ToDateTime(dgvRow.Cells["EvaluationDate"].Value == DBNull.Value ? "" : dgvRow.Cells["EvaluationDate"].Value);
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("Update [ProjectA].[dbo].[GroupEvaluation] set EvaluationDate='" + dt + "',ObtainedMarks='"+obt_marks+"',GroupId='"+g_id+"' where EvaluationId='" + id + "'", conn);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Updated");
                }
                catch (Exception d)
                {
                    MessageBox.Show("Please Enter valid GroupId (Hint:Choose  from combobox)");
                }


            }
            conn.Close();
            display_data();
        }
    }
}
