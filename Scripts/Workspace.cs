using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerPro3000.Scripts
{
    public class Workspace : TabPage
    {
        public string WSTitle = "";

        public MenuStrip _MS_workspace;

        public GrpPanel groupPanel;

        public Workspace(string WorkspaceTitle)
        {
            this.Text = WorkspaceTitle;
            WSTitle = WorkspaceTitle;

            UseVisualStyleBackColor = true;
            Enter += NewtabPage_Entered;

            _MS_workspace = new MenuStrip();

            CreateNewWorkSpace();
        }

        private void CreateNewWorkSpace()
        {
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.IsSplitterFixed = true;
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.BorderStyle = BorderStyle.FixedSingle;
            splitContainer.SplitterDistance = 40;

            //group create & delete menustrip refered as "GTMS"
            MenuStrip MS_workspace = new MenuStrip();
            MS_workspace.Dock = DockStyle.Top;
            MS_workspace.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            MS_workspace.Items.Add(TTP.GTMS_CreateGroup_Name);
            MS_workspace.Items.Add(TTP.GTMS_DeleteGroup_Name);
            _MS_workspace = MS_workspace;

            //panel ontop of panel1 from main splitcontainer which will hold Groups
            //could be unnecessary..
            GrpPanel grpPanel = new GrpPanel();
            grpPanel.Dock = DockStyle.Fill;
            grpPanel.Padding = new Padding(0, 0, 0, 0);
            grpPanel.AutoScroll = true;
            groupPanel = grpPanel;

            splitContainer.Panel1.Controls.Add(grpPanel);
            splitContainer.Panel1.Controls.Add(MS_workspace);

            Controls.Add(splitContainer);
        }

        private void NewtabPage_Entered(object? sender, EventArgs e)
        {
            Refresh();
        }
    }
}
