namespace CUCM_AXL_Query
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerData = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblHosts = new System.Windows.Forms.Label();
            this.tvHosts = new System.Windows.Forms.TreeView();
            this.splitContainerNav = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTables = new System.Windows.Forms.Label();
            this.tvTables = new System.Windows.Forms.TreeView();
            this.splitContainerSQLPane = new System.Windows.Forms.SplitContainer();
            this.splitContainerSQLCmd = new System.Windows.Forms.SplitContainer();
            this.tbSQLQuery = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainerWhole = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuHosts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerData)).BeginInit();
            this.splitContainerData.Panel1.SuspendLayout();
            this.splitContainerData.Panel2.SuspendLayout();
            this.splitContainerData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerNav)).BeginInit();
            this.splitContainerNav.Panel1.SuspendLayout();
            this.splitContainerNav.Panel2.SuspendLayout();
            this.splitContainerNav.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSQLPane)).BeginInit();
            this.splitContainerSQLPane.Panel1.SuspendLayout();
            this.splitContainerSQLPane.Panel2.SuspendLayout();
            this.splitContainerSQLPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSQLCmd)).BeginInit();
            this.splitContainerSQLCmd.Panel1.SuspendLayout();
            this.splitContainerSQLCmd.Panel2.SuspendLayout();
            this.splitContainerSQLCmd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerWhole)).BeginInit();
            this.splitContainerWhole.Panel1.SuspendLayout();
            this.splitContainerWhole.Panel2.SuspendLayout();
            this.splitContainerWhole.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuHosts.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(703, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItemFile.Text = "&File";
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItemHelp.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemOpen.Text = "&Open";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // splitContainerData
            // 
            this.splitContainerData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerData.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerData.Location = new System.Drawing.Point(0, 0);
            this.splitContainerData.Name = "splitContainerData";
            // 
            // splitContainerData.Panel1
            // 
            this.splitContainerData.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainerData.Panel2
            // 
            this.splitContainerData.Panel2.Controls.Add(this.splitContainerSQLPane);
            this.splitContainerData.Size = new System.Drawing.Size(703, 375);
            this.splitContainerData.SplitterDistance = 181;
            this.splitContainerData.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainerNav);
            this.splitContainer1.Size = new System.Drawing.Size(181, 375);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvHosts);
            this.splitContainer2.Size = new System.Drawing.Size(181, 109);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblHosts);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(181, 25);
            this.panel2.TabIndex = 1;
            // 
            // lblHosts
            // 
            this.lblHosts.AutoSize = true;
            this.lblHosts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHosts.Location = new System.Drawing.Point(3, 5);
            this.lblHosts.Name = "lblHosts";
            this.lblHosts.Size = new System.Drawing.Size(51, 20);
            this.lblHosts.TabIndex = 0;
            this.lblHosts.Text = "Hosts";
            // 
            // tvHosts
            // 
            this.tvHosts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvHosts.Location = new System.Drawing.Point(0, 0);
            this.tvHosts.Name = "tvHosts";
            this.tvHosts.Size = new System.Drawing.Size(181, 80);
            this.tvHosts.TabIndex = 0;
            this.tvHosts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvHosts_AfterSelect);
            this.tvHosts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvHosts_MouseDown);
            // 
            // splitContainerNav
            // 
            this.splitContainerNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerNav.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerNav.Location = new System.Drawing.Point(0, 0);
            this.splitContainerNav.Name = "splitContainerNav";
            this.splitContainerNav.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerNav.Panel1
            // 
            this.splitContainerNav.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainerNav.Panel2
            // 
            this.splitContainerNav.Panel2.Controls.Add(this.tvTables);
            this.splitContainerNav.Size = new System.Drawing.Size(181, 262);
            this.splitContainerNav.SplitterDistance = 25;
            this.splitContainerNav.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTables);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 25);
            this.panel1.TabIndex = 1;
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTables.Location = new System.Drawing.Point(3, 5);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(56, 20);
            this.lblTables.TabIndex = 0;
            this.lblTables.Text = "Tables";
            // 
            // tvTables
            // 
            this.tvTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTables.Location = new System.Drawing.Point(0, 0);
            this.tvTables.Name = "tvTables";
            this.tvTables.Size = new System.Drawing.Size(181, 233);
            this.tvTables.TabIndex = 0;
            this.tvTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTables_AfterSelect);
            // 
            // splitContainerSQLPane
            // 
            this.splitContainerSQLPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSQLPane.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSQLPane.Name = "splitContainerSQLPane";
            this.splitContainerSQLPane.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSQLPane.Panel1
            // 
            this.splitContainerSQLPane.Panel1.Controls.Add(this.splitContainerSQLCmd);
            // 
            // splitContainerSQLPane.Panel2
            // 
            this.splitContainerSQLPane.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainerSQLPane.Size = new System.Drawing.Size(518, 375);
            this.splitContainerSQLPane.SplitterDistance = 57;
            this.splitContainerSQLPane.TabIndex = 3;
            // 
            // splitContainerSQLCmd
            // 
            this.splitContainerSQLCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSQLCmd.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerSQLCmd.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSQLCmd.Name = "splitContainerSQLCmd";
            // 
            // splitContainerSQLCmd.Panel1
            // 
            this.splitContainerSQLCmd.Panel1.Controls.Add(this.tbSQLQuery);
            // 
            // splitContainerSQLCmd.Panel2
            // 
            this.splitContainerSQLCmd.Panel2.Controls.Add(this.button1);
            this.splitContainerSQLCmd.Size = new System.Drawing.Size(518, 57);
            this.splitContainerSQLCmd.SplitterDistance = 413;
            this.splitContainerSQLCmd.TabIndex = 0;
            // 
            // tbSQLQuery
            // 
            this.tbSQLQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSQLQuery.Location = new System.Drawing.Point(0, 0);
            this.tbSQLQuery.Multiline = true;
            this.tbSQLQuery.Name = "tbSQLQuery";
            this.tbSQLQuery.Size = new System.Drawing.Size(413, 57);
            this.tbSQLQuery.TabIndex = 0;
            this.tbSQLQuery.Text = "select first 10 * from device";
            this.tbSQLQuery.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            this.tbSQLQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSQLQuery_KeyDown);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(23, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Execute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(518, 314);
            this.dataGridView1.TabIndex = 0;
            // 
            // splitContainerWhole
            // 
            this.splitContainerWhole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerWhole.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerWhole.Location = new System.Drawing.Point(0, 24);
            this.splitContainerWhole.Name = "splitContainerWhole";
            this.splitContainerWhole.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerWhole.Panel1
            // 
            this.splitContainerWhole.Panel1.Controls.Add(this.splitContainerData);
            // 
            // splitContainerWhole.Panel2
            // 
            this.splitContainerWhole.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainerWhole.Size = new System.Drawing.Size(703, 404);
            this.splitContainerWhole.SplitterDistance = 375;
            this.splitContainerWhole.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(703, 25);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // contextMenuHosts
            // 
            this.contextMenuHosts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newServerToolStripMenuItem,
            this.deleteServerToolStripMenuItem});
            this.contextMenuHosts.Name = "contextMenuHosts";
            this.contextMenuHosts.Size = new System.Drawing.Size(143, 48);
            // 
            // newServerToolStripMenuItem
            // 
            this.newServerToolStripMenuItem.Name = "newServerToolStripMenuItem";
            this.newServerToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.newServerToolStripMenuItem.Text = "Add Server";
            this.newServerToolStripMenuItem.Click += new System.EventHandler(this.newServerToolStripMenuItem_Click);
            // 
            // deleteServerToolStripMenuItem
            // 
            this.deleteServerToolStripMenuItem.Name = "deleteServerToolStripMenuItem";
            this.deleteServerToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.deleteServerToolStripMenuItem.Text = "Delete Server";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 428);
            this.Controls.Add(this.splitContainerWhole);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "CUCM AXL Query";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainerData.Panel1.ResumeLayout(false);
            this.splitContainerData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerData)).EndInit();
            this.splitContainerData.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainerNav.Panel1.ResumeLayout(false);
            this.splitContainerNav.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerNav)).EndInit();
            this.splitContainerNav.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainerSQLPane.Panel1.ResumeLayout(false);
            this.splitContainerSQLPane.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSQLPane)).EndInit();
            this.splitContainerSQLPane.ResumeLayout(false);
            this.splitContainerSQLCmd.Panel1.ResumeLayout(false);
            this.splitContainerSQLCmd.Panel1.PerformLayout();
            this.splitContainerSQLCmd.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSQLCmd)).EndInit();
            this.splitContainerSQLCmd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainerWhole.Panel1.ResumeLayout(false);
            this.splitContainerWhole.Panel2.ResumeLayout(false);
            this.splitContainerWhole.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerWhole)).EndInit();
            this.splitContainerWhole.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuHosts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.SplitContainer splitContainerData;
        private System.Windows.Forms.SplitContainer splitContainerWhole;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblHosts;
        private System.Windows.Forms.TreeView tvHosts;
        private System.Windows.Forms.SplitContainer splitContainerNav;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.TreeView tvTables;
        private System.Windows.Forms.SplitContainer splitContainerSQLPane;
        private System.Windows.Forms.SplitContainer splitContainerSQLCmd;
        private System.Windows.Forms.TextBox tbSQLQuery;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuHosts;
        private System.Windows.Forms.ToolStripMenuItem newServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

