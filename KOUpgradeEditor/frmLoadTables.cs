/**
 * ______________________________________________________
 * This file is part of ko-item-tbl-importer project.
 * 
 * @author       Mustafa Kemal Gılor <mustafagilor@gmail.com> (2016)
 * .
 * SPDX-License-Identifier:	MIT
 * ______________________________________________________
 */

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace KOUpgradeEditor
{
    public partial class frmLoadTables : Form
    {
        public frmLoadTables()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private const byte extCount = 44;

        void LoadTables(object x)
        {
            StaticReference.TableSet.Clear();
            Trace.TraceWarning("LoadTables");
            lblFilename.Text = String.Format("item_org_us.tbl");
            StaticReference.LoadTable("item_org_us.tbl");
            pbSTotal.Value = 16;
            for (int i = 0; i <= extCount; i++)
            {
                pbSTotal.PerformStep();
                lblFilename.Text = String.Format("item_ext_{0}_us.tbl", i);
                StaticReference.LoadTable(String.Format("item_ext_{0}_us.tbl", i));
                pbSTotal.PerformStep();
            }

            Thread.Sleep(750);
            Close();
        }

        public void vLoad()
        {
            Thread thr = new Thread(LoadTables);
            thr.Start();
            ShowDialog();
        }

        private void frmLoadTables_Load(object sender, EventArgs e)
        {

        }

        private void frmLoadTables_Load_1(object sender, EventArgs e)
        {

        }

        private void frmLoadTables_Load_2(object sender, EventArgs e)
        {

        }
    }
}
