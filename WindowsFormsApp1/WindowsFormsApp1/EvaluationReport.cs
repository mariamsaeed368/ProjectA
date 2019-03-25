using System;
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
    public partial class EvaluationReport : Form
    {
        public EvaluationReport()
        {
            InitializeComponent();
        }

        private void EvaluationReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.View' table. You can move, or remove it, as needed.
            this.ViewTableAdapter.Fill(this.DataSet1.View);
            // TODO: This line of code loads data into the 'EvaluationDataset.EvaluationView' table. You can move, or remove it, as needed.
            this.EvaluationViewTableAdapter.Fill(this.EvaluationDataset.EvaluationView);
            this.reportViewer1.RefreshReport();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person p = new Person();
            p.Show();
            this.Hide();
        }
    }
}
