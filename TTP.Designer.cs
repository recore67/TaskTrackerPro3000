namespace TaskTrackerPro3000
{
    partial class TTP
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            workspacesToolStripMenuItem = new ToolStripMenuItem();
            createNewToolStripMenuItem = new ToolStripMenuItem();
            WorkSpaceHandler = new TabControl();
            DashboardTab = new TabPage();
            splitContainer1 = new SplitContainer();
            menuStrip1.SuspendLayout();
            WorkSpaceHandler.SuspendLayout();
            DashboardTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, workspacesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // workspacesToolStripMenuItem
            // 
            workspacesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createNewToolStripMenuItem });
            workspacesToolStripMenuItem.Name = "workspacesToolStripMenuItem";
            workspacesToolStripMenuItem.Size = new Size(82, 20);
            workspacesToolStripMenuItem.Text = "Workspaces";
            // 
            // createNewToolStripMenuItem
            // 
            createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
            createNewToolStripMenuItem.Size = new Size(133, 22);
            createNewToolStripMenuItem.Text = "Create new";
            createNewToolStripMenuItem.Click += createNewToolStripMenuItem_Click;
            // 
            // WorkSpaceHandler
            // 
            WorkSpaceHandler.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WorkSpaceHandler.Controls.Add(DashboardTab);
            WorkSpaceHandler.Location = new Point(12, 27);
            WorkSpaceHandler.Multiline = true;
            WorkSpaceHandler.Name = "WorkSpaceHandler";
            WorkSpaceHandler.SelectedIndex = 0;
            WorkSpaceHandler.Size = new Size(776, 411);
            WorkSpaceHandler.TabIndex = 1;
            // 
            // DashboardTab
            // 
            DashboardTab.Controls.Add(splitContainer1);
            DashboardTab.Location = new Point(4, 24);
            DashboardTab.Name = "DashboardTab";
            DashboardTab.Padding = new Padding(3);
            DashboardTab.Size = new Size(768, 383);
            DashboardTab.TabIndex = 0;
            DashboardTab.Text = "Dashboard";
            DashboardTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Size = new Size(762, 377);
            splitContainer1.SplitterDistance = 245;
            splitContainer1.TabIndex = 0;
            // 
            // TTP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(WorkSpaceHandler);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "TTP";
            Text = "TTP";
            FormClosed += TTP_FormClosed;
            Load += TTP_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            WorkSpaceHandler.ResumeLayout(false);
            DashboardTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem workspacesToolStripMenuItem;
        private ToolStripMenuItem createNewToolStripMenuItem;
        private TabPage DashboardTab;
        private SplitContainer splitContainer1;
        private TabControl WorkSpaceHandler;
    }
}