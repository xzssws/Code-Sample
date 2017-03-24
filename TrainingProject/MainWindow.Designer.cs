namespace TrainingProject
{
    partial class MainWindow
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭当前窗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭其他窗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭所有窗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkTabPage = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 30);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(182, 543);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(665, 577);
            this.tabControl1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭当前窗体ToolStripMenuItem,
            this.关闭其他窗体ToolStripMenuItem,
            this.关闭所有窗体ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 70);
            // 
            // 关闭当前窗体ToolStripMenuItem
            // 
            this.关闭当前窗体ToolStripMenuItem.Name = "关闭当前窗体ToolStripMenuItem";
            this.关闭当前窗体ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关闭当前窗体ToolStripMenuItem.Text = "关闭当前窗体";
            this.关闭当前窗体ToolStripMenuItem.Click += new System.EventHandler(this.关闭当前窗体ToolStripMenuItem_Click);
            // 
            // 关闭其他窗体ToolStripMenuItem
            // 
            this.关闭其他窗体ToolStripMenuItem.Name = "关闭其他窗体ToolStripMenuItem";
            this.关闭其他窗体ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关闭其他窗体ToolStripMenuItem.Text = "关闭其他窗体";
            this.关闭其他窗体ToolStripMenuItem.Click += new System.EventHandler(this.关闭其他窗体ToolStripMenuItem_Click);
            // 
            // 关闭所有窗体ToolStripMenuItem
            // 
            this.关闭所有窗体ToolStripMenuItem.Name = "关闭所有窗体ToolStripMenuItem";
            this.关闭所有窗体ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关闭所有窗体ToolStripMenuItem.Text = "关闭所有窗体";
            this.关闭所有窗体ToolStripMenuItem.Click += new System.EventHandler(this.关闭所有窗体ToolStripMenuItem_Click);
            // 
            // chkTabPage
            // 
            this.chkTabPage.AutoSize = true;
            this.chkTabPage.Location = new System.Drawing.Point(48, 8);
            this.chkTabPage.Name = "chkTabPage";
            this.chkTabPage.Size = new System.Drawing.Size(84, 16);
            this.chkTabPage.TabIndex = 3;
            this.chkTabPage.Text = "标签页打开";
            this.chkTabPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 577);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkTabPage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 30);
            this.panel2.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(854, 577);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(854, 577);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximumSize = new System.Drawing.Size(870, 615);
            this.MinimumSize = new System.Drawing.Size(870, 615);
            this.Name = "MainWindow";
            this.Text = "框架";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox chkTabPage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关闭当前窗体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭其他窗体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭所有窗体ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}