using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace WindowsFormsApp1
{
    public partial class Person : Form
    {
        int gender;
        SqlConnection conn = new SqlConnection(@"Data Source=SONY\SQLEXPRESS;Initial Catalog=ProjectA;Integrated Security=True");
        public Person()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Insert into Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) OUTPUT INSERTED.ID values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + gender + "')";
            cmd.ExecuteNonQuery();
            //int ID = (int)cmd.ExecuteScalar();
            conn.Close();
            MessageBox.Show("Data saved");
            //  conn.Open();
            /* SqlCommand cmd1 = new SqlCommand("Insert into Student(Id,RegistrationNo) values('"+ID+ "','"+ textBox7.Text + "')", conn);
             cmd1.ExecuteNonQuery();
             MessageBox.Show("Data is saved!");
             textBox1.Text = " ";
             textBox2.Text = " ";
             textBox3.Text = " ";
             textBox4.Text = " ";
             textBox5.Text = " ";
             textBox7.Text = " ";
            /* String Query = "Insert into Person(FirstName,LastName,Contact,Email,DateOfBirth,Gender) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
             String Query1 = "Insert into Student(RegistrationNo) values('" + textBox7.Text + "')";
             SqlCommand cmd = new SqlCommand(Query, conn);
             SqlCommand cmd1 = new SqlCommand(Query1, conn);
             SqlDataReader myreader;
             SqlDataReader myreader2;
             try
             {
                 conn.Open();
                 myreader = cmd.ExecuteReader();
                 myreader2 = cmd1.ExecuteReader();
                 MessageBox.Show("Saved");
                 if(myreader !=null)
                 {
                     while (myreader.Read())
                     {

                     }
                 }

             }
             catch(Exception es)
             {
                 MessageBox.Show(es.Message);
             }*/
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = 2;
        }
        public void display_data()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Person";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = 1;
        }

        private void Person_Load(object sender, EventArgs e)
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
                        cmd.CommandText = "Delete from Person where Id='" + rowID + "'";
                        cmd.ExecuteNonQuery();
                        display_data();
                        MessageBox.Show("Deleted");
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow dgvRow = dataGridView1.CurrentRow;
                int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
                string fname = dgvRow.Cells["FirstName"].Value == DBNull.Value ? "" : dgvRow.Cells["FirstName"].Value.ToString();
                string lname = dgvRow.Cells["LastName"].Value == DBNull.Value ? "" : dgvRow.Cells["LastName"].Value.ToString();
                string contact = dgvRow.Cells["Contact"].Value == DBNull.Value ? "" : dgvRow.Cells["Contact"].Value.ToString();
                string email = dgvRow.Cells["Email"].Value == DBNull.Value ? "" : dgvRow.Cells["Email"].Value.ToString();
                DateTime dob = Convert.ToDateTime(dgvRow.Cells["DateOfBirth"].Value == DBNull.Value ? "" : dgvRow.Cells["DateOfBirth"].Value);
                int gender = Convert.ToInt32(dgvRow.Cells["Gender"].Value == DBNull.Value ? "" : dgvRow.Cells["Gender"].Value);
                SqlCommand sqlCmd = new SqlCommand("Update Person set FirstName='" + fname + "' ,LastName='" + lname + "' ,Contact='" + contact + "' ,Email='" + email + "', DateOfBirth='" + dob + "' ,Gender='" + gender + "' where Id='" + id + "'", conn);
                sqlCmd.ExecuteNonQuery();
                display_data();
                MessageBox.Show("Updated");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                conn.Open();
                DataGridViewRow dgvRow = dataGridView1.CurrentRow;
                int id = Convert.ToInt32(dgvRow.Cells["Id"].Value);
                string fname = dgvRow.Cells["FirstName"].Value == DBNull.Value ? "" : dgvRow.Cells["FirstName"].Value.ToString();
                string lname = dgvRow.Cells["LastName"].Value == DBNull.Value ? "" : dgvRow.Cells["LastName"].Value.ToString();
                string contact = dgvRow.Cells["Contact"].Value == DBNull.Value ? "" : dgvRow.Cells["Contact"].Value.ToString();
                string email = dgvRow.Cells["Email"].Value == DBNull.Value ? "" : dgvRow.Cells["Email"].Value.ToString();
                DateTime dob = Convert.ToDateTime(dgvRow.Cells["DateOfBirth"].Value == DBNull.Value ? "" : dgvRow.Cells["DateOfBirth"].Value);
                int gender = Convert.ToInt32(dgvRow.Cells["Gender"].Value == DBNull.Value ? "" : dgvRow.Cells["Gender"].Value);
                SqlCommand sqlCmd = new SqlCommand("Update Person set FirstName='" + fname + "' ,LastName='" + lname + "' ,Contact='" + contact + "' ,Email='" + email + "', DateOfBirth='" + dob + "' ,Gender='" + gender + "' where Id='" + id + "'", conn);
                sqlCmd.ExecuteNonQuery();
                display_data();
                MessageBox.Show("Updated");

            }
        }
    }
}
