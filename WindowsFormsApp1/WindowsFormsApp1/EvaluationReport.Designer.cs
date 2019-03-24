namespace WindowsFormsApp1
{
    partial class EvaluationReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.EvaluationViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.EvaluationDataset = new WindowsFormsApp1.EvaluationDataset();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.EvaluationViewTableAdapter = new WindowsFormsApp1.EvaluationDatasetTableAdapters.EvaluationViewTableAdapter();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.EvaluationViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EvaluationDataset)).BeginInit();
            this.SuspendLayout();
            // 
            // EvaluationViewBindingSource
            // 
            this.EvaluationViewBindingSource.DataMember = "EvaluationView";
            this.EvaluationViewBindingSource.DataSource = this.EvaluationDataset;
            // 
            // EvaluationDataset
            // 
            this.EvaluationDataset.DataSetName = "EvaluationDataset";
            this.EvaluationDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource2.Name = "EvaluationDataset";
            reportDataSource2.Value = this.EvaluationViewBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp1.EvaluationReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, -1);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1094, 425);
            this.reportViewer1.TabIndex = 0;
            // 
            // EvaluationViewTableAdapter
            // 
            this.EvaluationViewTableAdapter.ClearBeforeFill = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(1016, 441);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(49, 13);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Go Back";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // EvaluationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 468);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "EvaluationReport";
            this.Text = "EvaluationReport";
            this.Load += new System.EventHandler(this.EvaluationReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EvaluationViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EvaluationDataset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource EvaluationViewBindingSource;
        private EvaluationDataset EvaluationDataset;
        private EvaluationDatasetTableAdapters.EvaluationViewTableAdapter EvaluationViewTableAdapter;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}