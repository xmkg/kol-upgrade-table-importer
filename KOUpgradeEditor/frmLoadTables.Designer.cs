using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KOUpgradeEditor
{
    partial class frmLoadTables
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbSTotal = new System.Windows.Forms.ProgressBar();
            this.lblFilename = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading tables..";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filename :";
            // 
            // pbSTotal
            // 
            this.pbSTotal.Location = new System.Drawing.Point(12, 60);
            this.pbSTotal.Name = "pbSTotal";
            this.pbSTotal.Size = new System.Drawing.Size(331, 15);
            this.pbSTotal.TabIndex = 2;
            // 
            // lblFilename
            // 
            this.lblFilename.Location = new System.Drawing.Point(73, 31);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(270, 23);
            this.lblFilename.TabIndex = 3;
            this.lblFilename.Text = "-";
            this.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmLoadTables
            // 
            this.ClientSize = new System.Drawing.Size(355, 84);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.pbSTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLoadTables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLoadTables";
            this.Load += new System.EventHandler(this.frmLoadTables_Load_2);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private ProgressBar pbSTotal;
        private Label lblFilename;
    }
}