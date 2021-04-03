namespace KOUpgradeEditor
{
    partial class frmMain
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
            this.lbScrolls = new System.Windows.Forms.ListBox();
            this.txtScrollID = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmsScrollList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbAccessory = new System.Windows.Forms.CheckBox();
            this.cbArmor = new System.Windows.Forms.CheckBox();
            this.cbWeapon = new System.Windows.Forms.CheckBox();
            this.txtTrinaPercent = new System.Windows.Forms.TextBox();
            this.txtModifier = new System.Windows.Forms.TextBox();
            this.lblSelectedScroll = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTrinaPercent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCost = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtPercent = new System.Windows.Forms.TextBox();
            this.gbType = new System.Windows.Forms.GroupBox();
            this.rbReverse = new System.Windows.Forms.RadioButton();
            this.rbDispell = new System.Windows.Forms.RadioButton();
            this.rbAccessoryEnchant = new System.Windows.Forms.RadioButton();
            this.rbAccessoryCompound = new System.Windows.Forms.RadioButton();
            this.rbStat = new System.Windows.Forms.RadioButton();
            this.rbElemental = new System.Windows.Forms.RadioButton();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.gbGrade = new System.Windows.Forms.GroupBox();
            this.rbReverseConversion = new System.Windows.Forms.RadioButton();
            this.rbReverseStrength = new System.Windows.Forms.RadioButton();
            this.rbLow = new System.Windows.Forms.RadioButton();
            this.rbMiddle = new System.Windows.Forms.RadioButton();
            this.rbHigh = new System.Windows.Forms.RadioButton();
            this.rbBlessed = new System.Windows.Forms.RadioButton();
            this.lbGrades = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDump = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmsScrollList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbType.SuspendLayout();
            this.gbGrade.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbScrolls
            // 
            this.lbScrolls.FormattingEnabled = true;
            this.lbScrolls.ItemHeight = 16;
            this.lbScrolls.Location = new System.Drawing.Point(16, 38);
            this.lbScrolls.Margin = new System.Windows.Forms.Padding(4);
            this.lbScrolls.Name = "lbScrolls";
            this.lbScrolls.Size = new System.Drawing.Size(374, 228);
            this.lbScrolls.TabIndex = 1;
            this.lbScrolls.SelectedIndexChanged += new System.EventHandler(this.lbScrolls_SelectedIndexChanged);
            this.lbScrolls.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbScrolls_MouseDown);
            // 
            // txtScrollID
            // 
            this.txtScrollID.Location = new System.Drawing.Point(16, 284);
            this.txtScrollID.Margin = new System.Windows.Forms.Padding(4);
            this.txtScrollID.Name = "txtScrollID";
            this.txtScrollID.Size = new System.Drawing.Size(283, 22);
            this.txtScrollID.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(307, 283);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(83, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add New";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmsScrollList
            // 
            this.cmsScrollList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsScrollList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.cmsScrollList.Name = "cmsScrollList";
            this.cmsScrollList.Size = new System.Drawing.Size(133, 28);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(132, 24);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtTrinaPercent);
            this.groupBox1.Controls.Add(this.txtModifier);
            this.groupBox1.Controls.Add(this.lblSelectedScroll);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblTrinaPercent);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCost);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.txtPercent);
            this.groupBox1.Controls.Add(this.gbType);
            this.groupBox1.Controls.Add(this.gbGrade);
            this.groupBox1.Controls.Add(this.lbGrades);
            this.groupBox1.Location = new System.Drawing.Point(398, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(736, 260);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cbAccessory
            // 
            this.cbAccessory.AutoSize = true;
            this.cbAccessory.Location = new System.Drawing.Point(27, 120);
            this.cbAccessory.Margin = new System.Windows.Forms.Padding(4);
            this.cbAccessory.Name = "cbAccessory";
            this.cbAccessory.Size = new System.Drawing.Size(92, 21);
            this.cbAccessory.TabIndex = 12;
            this.cbAccessory.Text = "Accessory";
            this.cbAccessory.UseVisualStyleBackColor = true;
            // 
            // cbArmor
            // 
            this.cbArmor.AutoSize = true;
            this.cbArmor.Location = new System.Drawing.Point(27, 91);
            this.cbArmor.Margin = new System.Windows.Forms.Padding(4);
            this.cbArmor.Name = "cbArmor";
            this.cbArmor.Size = new System.Drawing.Size(65, 21);
            this.cbArmor.TabIndex = 11;
            this.cbArmor.Text = "Armor";
            this.cbArmor.UseVisualStyleBackColor = true;
            // 
            // cbWeapon
            // 
            this.cbWeapon.AutoSize = true;
            this.cbWeapon.Location = new System.Drawing.Point(27, 58);
            this.cbWeapon.Margin = new System.Windows.Forms.Padding(4);
            this.cbWeapon.Name = "cbWeapon";
            this.cbWeapon.Size = new System.Drawing.Size(80, 21);
            this.cbWeapon.TabIndex = 10;
            this.cbWeapon.Text = "Weapon";
            this.cbWeapon.UseVisualStyleBackColor = true;
            // 
            // txtTrinaPercent
            // 
            this.txtTrinaPercent.Location = new System.Drawing.Point(179, 202);
            this.txtTrinaPercent.Margin = new System.Windows.Forms.Padding(4);
            this.txtTrinaPercent.Name = "txtTrinaPercent";
            this.txtTrinaPercent.Size = new System.Drawing.Size(103, 22);
            this.txtTrinaPercent.TabIndex = 9;
            this.txtTrinaPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTrinaPercent.Visible = false;
            // 
            // txtModifier
            // 
            this.txtModifier.Location = new System.Drawing.Point(179, 154);
            this.txtModifier.Margin = new System.Windows.Forms.Padding(4);
            this.txtModifier.Name = "txtModifier";
            this.txtModifier.Size = new System.Drawing.Size(103, 22);
            this.txtModifier.TabIndex = 8;
            this.txtModifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSelectedScroll
            // 
            this.lblSelectedScroll.AutoSize = true;
            this.lblSelectedScroll.Location = new System.Drawing.Point(176, 236);
            this.lblSelectedScroll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedScroll.Name = "lblSelectedScroll";
            this.lblSelectedScroll.Size = new System.Drawing.Size(102, 17);
            this.lblSelectedScroll.TabIndex = 7;
            this.lblSelectedScroll.Text = "Selected Scroll";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cost";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 133);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Modifier";
            // 
            // lblTrinaPercent
            // 
            this.lblTrinaPercent.AutoSize = true;
            this.lblTrinaPercent.Location = new System.Drawing.Point(176, 182);
            this.lblTrinaPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrinaPercent.Name = "lblTrinaPercent";
            this.lblTrinaPercent.Size = new System.Drawing.Size(94, 17);
            this.lblTrinaPercent.TabIndex = 5;
            this.lblTrinaPercent.Text = "Trina Percent";
            this.lblTrinaPercent.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Percent";
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(180, 105);
            this.txtCost.Margin = new System.Windows.Forms.Padding(4);
            this.txtCost.Name = "txtCost";
            this.txtCost.Size = new System.Drawing.Size(103, 22);
            this.txtCost.TabIndex = 4;
            this.txtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(585, 218);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 33);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtPercent
            // 
            this.txtPercent.Location = new System.Drawing.Point(180, 48);
            this.txtPercent.Margin = new System.Windows.Forms.Padding(4);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new System.Drawing.Size(103, 22);
            this.txtPercent.TabIndex = 3;
            this.txtPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.rbReverse);
            this.gbType.Controls.Add(this.rbDispell);
            this.gbType.Controls.Add(this.rbAccessoryEnchant);
            this.gbType.Controls.Add(this.rbAccessoryCompound);
            this.gbType.Controls.Add(this.rbStat);
            this.gbType.Controls.Add(this.rbElemental);
            this.gbType.Controls.Add(this.rbNormal);
            this.gbType.Location = new System.Drawing.Point(296, 23);
            this.gbType.Margin = new System.Windows.Forms.Padding(4);
            this.gbType.Name = "gbType";
            this.gbType.Padding = new System.Windows.Forms.Padding(4);
            this.gbType.Size = new System.Drawing.Size(125, 228);
            this.gbType.TabIndex = 2;
            this.gbType.TabStop = false;
            this.gbType.Text = "Type";
            // 
            // rbReverse
            // 
            this.rbReverse.AutoSize = true;
            this.rbReverse.Location = new System.Drawing.Point(10, 195);
            this.rbReverse.Margin = new System.Windows.Forms.Padding(4);
            this.rbReverse.Name = "rbReverse";
            this.rbReverse.Size = new System.Drawing.Size(79, 21);
            this.rbReverse.TabIndex = 5;
            this.rbReverse.TabStop = true;
            this.rbReverse.Tag = "REVERSE";
            this.rbReverse.Text = "Reverse";
            this.rbReverse.UseVisualStyleBackColor = true;
            this.rbReverse.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbDispell
            // 
            this.rbDispell.AutoSize = true;
            this.rbDispell.Location = new System.Drawing.Point(10, 109);
            this.rbDispell.Margin = new System.Windows.Forms.Padding(4);
            this.rbDispell.Name = "rbDispell";
            this.rbDispell.Size = new System.Drawing.Size(68, 21);
            this.rbDispell.TabIndex = 4;
            this.rbDispell.TabStop = true;
            this.rbDispell.Tag = "DISPELL";
            this.rbDispell.Text = "Dispell";
            this.rbDispell.UseVisualStyleBackColor = true;
            this.rbDispell.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbAccessoryEnchant
            // 
            this.rbAccessoryEnchant.AutoSize = true;
            this.rbAccessoryEnchant.Location = new System.Drawing.Point(9, 166);
            this.rbAccessoryEnchant.Margin = new System.Windows.Forms.Padding(4);
            this.rbAccessoryEnchant.Name = "rbAccessoryEnchant";
            this.rbAccessoryEnchant.Size = new System.Drawing.Size(95, 21);
            this.rbAccessoryEnchant.TabIndex = 3;
            this.rbAccessoryEnchant.TabStop = true;
            this.rbAccessoryEnchant.Tag = "ACCESSORYENCHANT";
            this.rbAccessoryEnchant.Text = "A. Enchant";
            this.rbAccessoryEnchant.UseVisualStyleBackColor = true;
            this.rbAccessoryEnchant.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbAccessoryCompound
            // 
            this.rbAccessoryCompound.AutoSize = true;
            this.rbAccessoryCompound.Location = new System.Drawing.Point(9, 138);
            this.rbAccessoryCompound.Margin = new System.Windows.Forms.Padding(4);
            this.rbAccessoryCompound.Name = "rbAccessoryCompound";
            this.rbAccessoryCompound.Size = new System.Drawing.Size(94, 21);
            this.rbAccessoryCompound.TabIndex = 3;
            this.rbAccessoryCompound.TabStop = true;
            this.rbAccessoryCompound.Tag = "ACCESSORYCOMPOUND";
            this.rbAccessoryCompound.Text = "Compound";
            this.rbAccessoryCompound.UseVisualStyleBackColor = true;
            this.rbAccessoryCompound.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbStat
            // 
            this.rbStat.AutoSize = true;
            this.rbStat.Location = new System.Drawing.Point(10, 81);
            this.rbStat.Margin = new System.Windows.Forms.Padding(4);
            this.rbStat.Name = "rbStat";
            this.rbStat.Size = new System.Drawing.Size(78, 21);
            this.rbStat.TabIndex = 2;
            this.rbStat.TabStop = true;
            this.rbStat.Tag = "STAT";
            this.rbStat.Text = "Enchant";
            this.rbStat.UseVisualStyleBackColor = true;
            this.rbStat.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbElemental
            // 
            this.rbElemental.AutoSize = true;
            this.rbElemental.Location = new System.Drawing.Point(9, 52);
            this.rbElemental.Margin = new System.Windows.Forms.Padding(4);
            this.rbElemental.Name = "rbElemental";
            this.rbElemental.Size = new System.Drawing.Size(88, 21);
            this.rbElemental.TabIndex = 1;
            this.rbElemental.TabStop = true;
            this.rbElemental.Tag = "ELEMENT";
            this.rbElemental.Text = "Elemental";
            this.rbElemental.UseVisualStyleBackColor = true;
            this.rbElemental.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Location = new System.Drawing.Point(9, 23);
            this.rbNormal.Margin = new System.Windows.Forms.Padding(4);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(71, 21);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Tag = "REGULAR";
            this.rbNormal.Text = "Normal";
            this.rbNormal.UseVisualStyleBackColor = true;
            this.rbNormal.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // gbGrade
            // 
            this.gbGrade.Controls.Add(this.rbReverseConversion);
            this.gbGrade.Controls.Add(this.rbReverseStrength);
            this.gbGrade.Controls.Add(this.rbLow);
            this.gbGrade.Controls.Add(this.rbMiddle);
            this.gbGrade.Controls.Add(this.rbHigh);
            this.gbGrade.Controls.Add(this.rbBlessed);
            this.gbGrade.Location = new System.Drawing.Point(429, 23);
            this.gbGrade.Margin = new System.Windows.Forms.Padding(4);
            this.gbGrade.Name = "gbGrade";
            this.gbGrade.Padding = new System.Windows.Forms.Padding(4);
            this.gbGrade.Size = new System.Drawing.Size(148, 228);
            this.gbGrade.TabIndex = 1;
            this.gbGrade.TabStop = false;
            this.gbGrade.Text = "Scroll Grade";
            // 
            // rbReverseConversion
            // 
            this.rbReverseConversion.AutoSize = true;
            this.rbReverseConversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F);
            this.rbReverseConversion.Location = new System.Drawing.Point(8, 39);
            this.rbReverseConversion.Margin = new System.Windows.Forms.Padding(4);
            this.rbReverseConversion.Name = "rbReverseConversion";
            this.rbReverseConversion.Size = new System.Drawing.Size(134, 19);
            this.rbReverseConversion.TabIndex = 5;
            this.rbReverseConversion.TabStop = true;
            this.rbReverseConversion.Tag = "REVERSE_CONVERSION";
            this.rbReverseConversion.Text = "Reverse Conversion";
            this.rbReverseConversion.UseVisualStyleBackColor = true;
            this.rbReverseConversion.CheckedChanged += new System.EventHandler(this.rbGrade_CheckedChanged);
            // 
            // rbReverseStrength
            // 
            this.rbReverseStrength.AutoSize = true;
            this.rbReverseStrength.Location = new System.Drawing.Point(8, 68);
            this.rbReverseStrength.Margin = new System.Windows.Forms.Padding(4);
            this.rbReverseStrength.Name = "rbReverseStrength";
            this.rbReverseStrength.Size = new System.Drawing.Size(137, 21);
            this.rbReverseStrength.TabIndex = 4;
            this.rbReverseStrength.TabStop = true;
            this.rbReverseStrength.Tag = "REVERSE_STRENGTH";
            this.rbReverseStrength.Text = "Reverse Strength";
            this.rbReverseStrength.UseVisualStyleBackColor = true;
            this.rbReverseStrength.CheckedChanged += new System.EventHandler(this.rbGrade_CheckedChanged);
            // 
            // rbLow
            // 
            this.rbLow.AutoSize = true;
            this.rbLow.Location = new System.Drawing.Point(8, 183);
            this.rbLow.Margin = new System.Windows.Forms.Padding(4);
            this.rbLow.Name = "rbLow";
            this.rbLow.Size = new System.Drawing.Size(51, 21);
            this.rbLow.TabIndex = 3;
            this.rbLow.TabStop = true;
            this.rbLow.Tag = "LOW";
            this.rbLow.Text = "Low";
            this.rbLow.UseVisualStyleBackColor = true;
            this.rbLow.CheckedChanged += new System.EventHandler(this.rbGrade_CheckedChanged);
            // 
            // rbMiddle
            // 
            this.rbMiddle.AutoSize = true;
            this.rbMiddle.Location = new System.Drawing.Point(8, 155);
            this.rbMiddle.Margin = new System.Windows.Forms.Padding(4);
            this.rbMiddle.Name = "rbMiddle";
            this.rbMiddle.Size = new System.Drawing.Size(67, 21);
            this.rbMiddle.TabIndex = 2;
            this.rbMiddle.TabStop = true;
            this.rbMiddle.Tag = "MIDDLE";
            this.rbMiddle.Text = "Middle";
            this.rbMiddle.UseVisualStyleBackColor = true;
            this.rbMiddle.CheckedChanged += new System.EventHandler(this.rbGrade_CheckedChanged);
            // 
            // rbHigh
            // 
            this.rbHigh.AutoSize = true;
            this.rbHigh.Location = new System.Drawing.Point(8, 125);
            this.rbHigh.Margin = new System.Windows.Forms.Padding(4);
            this.rbHigh.Name = "rbHigh";
            this.rbHigh.Size = new System.Drawing.Size(55, 21);
            this.rbHigh.TabIndex = 1;
            this.rbHigh.TabStop = true;
            this.rbHigh.Tag = "HIGH";
            this.rbHigh.Text = "High";
            this.rbHigh.UseVisualStyleBackColor = true;
            this.rbHigh.CheckedChanged += new System.EventHandler(this.rbGrade_CheckedChanged);
            // 
            // rbBlessed
            // 
            this.rbBlessed.AutoSize = true;
            this.rbBlessed.Location = new System.Drawing.Point(8, 97);
            this.rbBlessed.Margin = new System.Windows.Forms.Padding(4);
            this.rbBlessed.Name = "rbBlessed";
            this.rbBlessed.Size = new System.Drawing.Size(76, 21);
            this.rbBlessed.TabIndex = 0;
            this.rbBlessed.TabStop = true;
            this.rbBlessed.Tag = "BLESSED";
            this.rbBlessed.Text = "Blessed";
            this.rbBlessed.UseVisualStyleBackColor = true;
            this.rbBlessed.CheckedChanged += new System.EventHandler(this.rbGrade_CheckedChanged);
            // 
            // lbGrades
            // 
            this.lbGrades.FormattingEnabled = true;
            this.lbGrades.ItemHeight = 16;
            this.lbGrades.Location = new System.Drawing.Point(8, 23);
            this.lbGrades.Margin = new System.Windows.Forms.Padding(4);
            this.lbGrades.Name = "lbGrades";
            this.lbGrades.Size = new System.Drawing.Size(159, 228);
            this.lbGrades.TabIndex = 0;
            this.lbGrades.SelectedIndexChanged += new System.EventHandler(this.lbGrades_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(398, 283);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(190, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save Current Scroll List";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDump
            // 
            this.btnDump.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F);
            this.btnDump.Location = new System.Drawing.Point(827, 283);
            this.btnDump.Margin = new System.Windows.Forms.Padding(4);
            this.btnDump.Name = "btnDump";
            this.btnDump.Size = new System.Drawing.Size(307, 22);
            this.btnDump.TabIndex = 8;
            this.btnDump.Text = "Dump";
            this.btnDump.UseVisualStyleBackColor = true;
            this.btnDump.Click += new System.EventHandler(this.btnDump_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(596, 283);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(223, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "this-is-here-for-test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbAccessory);
            this.groupBox2.Controls.Add(this.cbWeapon);
            this.groupBox2.Controls.Add(this.cbArmor);
            this.groupBox2.Location = new System.Drawing.Point(584, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(141, 187);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate for";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Scrolls";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Scroll Item ID";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 313);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbScrolls);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDump);
            this.Controls.Add(this.txtScrollID);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upgrade Editor";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.cmsScrollList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbType.ResumeLayout(false);
            this.gbType.PerformLayout();
            this.gbGrade.ResumeLayout(false);
            this.gbGrade.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbScrolls;
        private System.Windows.Forms.TextBox txtScrollID;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip cmsScrollList;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbGrades;
        private System.Windows.Forms.GroupBox gbGrade;
        private System.Windows.Forms.RadioButton rbBlessed;
        private System.Windows.Forms.RadioButton rbHigh;
        private System.Windows.Forms.RadioButton rbMiddle;
        private System.Windows.Forms.RadioButton rbLow;
        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.RadioButton rbStat;
        private System.Windows.Forms.RadioButton rbElemental;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.TextBox txtPercent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDump;
        private System.Windows.Forms.RadioButton rbAccessoryCompound;
        private System.Windows.Forms.RadioButton rbDispell;
        private System.Windows.Forms.RadioButton rbAccessoryEnchant;
        private System.Windows.Forms.Label lblSelectedScroll;
        private System.Windows.Forms.TextBox txtModifier;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTrinaPercent;
        private System.Windows.Forms.Label lblTrinaPercent;
        private System.Windows.Forms.CheckBox cbAccessory;
        private System.Windows.Forms.CheckBox cbArmor;
        private System.Windows.Forms.CheckBox cbWeapon;
        private System.Windows.Forms.RadioButton rbReverse;
        private System.Windows.Forms.RadioButton rbReverseStrength;
        private System.Windows.Forms.RadioButton rbReverseConversion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

