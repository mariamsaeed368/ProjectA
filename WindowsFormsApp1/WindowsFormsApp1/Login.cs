﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="Admin" && textBox2.Text=="123")
            {
                MessageBox.Show("Welcome to FYP management system");
                Person b = new Person();
                b.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Enter valid Username or Password");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
