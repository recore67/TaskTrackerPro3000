using System.Windows.Forms;
using System.IO;
using TaskTrackerPro3000.Scripts;
using System.Net.Http.Headers;

namespace TaskTrackerPro3000
{
    public partial class TTP : Form
    {
        public static string GTMS_CreateGroup_Name = "Create Group";
        public static string GTMS_DeleteGroup_Name = "Delete Group";
        public static string WS_CreatorForm_Text = "WorkSpace Creator";
        public static string WS_CreatorForm_Prompt = "WorkSpace Title:";
        public static string GT_CreatorForm_Text = "Group Creator";
        public static string GT_CreatorForm_Prompt = "Group Title:";
        public static string GT_DeleteForm_Text = "Group Terminator";
        public static string GT_DeleteFrom_Prompt = "Select Groups to delete";

        public TTP()
        {
            InitializeComponent();
            //string saveFileText = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wtf.txt")).index
        }

        private void TTP_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTab(DialogPrompts.CreateDialog(WS_CreatorForm_Text, WS_CreatorForm_Prompt));
        }

        private void NewtabPage_Entered(object? sender, EventArgs e)
        {
            Refresh();
        }

        public void CreateNewTab(string Title)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                string TitleFormated = Title.Trim();

                for (int i = 0; i < WorkSpaceHandler.TabCount; i++)
                {
                    if (WorkSpaceHandler.TabPages[i].Text == TitleFormated)
                    {
                        MessageBox.Show("sorry bub, another Workspace is using that Title");
                        return;
                    }
                }

                Workspace newWorkspacePage = new Workspace();
                newWorkspacePage.Text = TitleFormated;
                newWorkspacePage.UseVisualStyleBackColor = true;
                newWorkspacePage.Enter += NewtabPage_Entered;

                SplitContainer splitContainer = new SplitContainer();
                splitContainer.IsSplitterFixed = true;
                splitContainer.Dock = DockStyle.Fill;
                splitContainer.BorderStyle = BorderStyle.FixedSingle;
                splitContainer.SplitterDistance = 40;

                //group create & delete menustrip refered as "GTMS"
                MenuStrip MS_workspace = new MenuStrip();
                MS_workspace.Dock = DockStyle.Top;
                MS_workspace.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                MS_workspace.Items.Add(GTMS_CreateGroup_Name);
                MS_workspace.Items.Add(GTMS_DeleteGroup_Name);
                MS_workspace.ItemClicked += MS_workspace_ItemClicked;

                //panel child of panel1 of main splitcontainer wihch holds Groups
                GrpPanel grpPanel = new GrpPanel();
                grpPanel.Dock = DockStyle.Fill;
                grpPanel.Padding = new Padding(0, 0, 0, 0);
                grpPanel.AutoScroll = true;

                splitContainer.Panel1.Controls.Add(grpPanel);
                splitContainer.Panel1.Controls.Add(MS_workspace);

                newWorkspacePage.Controls.Add(splitContainer);

                //finally add new workspace to tabcontrol
                WorkSpaceHandler.TabPages.Add(newWorkspacePage);

                SelectWorkSpaceByTitle(TitleFormated);
            }
        }

        private void MS_workspace_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == GTMS_CreateGroup_Name)
            {
                CreateNewGroup(DialogPrompts.CreateDialog(GT_CreatorForm_Text, GT_CreatorForm_Prompt), (GrpPanel)GetNextControl(e.ClickedItem.GetCurrentParent().Parent, true));
            }
            if (e.ClickedItem.Text == GTMS_DeleteGroup_Name)
            {
                GrpPanel grpPanel = (GrpPanel)GetNextControl(e.ClickedItem.GetCurrentParent().Parent, true);
                List<GroupItem> groupItems = grpPanel.groupItems;

                DeleteGroupByList(DialogPrompts.DeleteGroupDialogByList(GT_DeleteForm_Text, GT_DeleteFrom_Prompt, groupItems), groupItems);
            }
        }

        public void CreateNewGroup(string title, GrpPanel grpPanel)
        {
            if (!string.IsNullOrEmpty(title))
            {
                string TitleFormatted = title.Trim();

                foreach (GroupItem item in grpPanel.groupItems)
                {
                    if (item.GroupTitle == TitleFormatted)
                        return;
                }

                SplitContainer MainsplitContainer = (SplitContainer)grpPanel.Parent.Parent;

                GroupItem groupItem = new GroupItem(TitleFormatted);
                groupItem.TaskPanelHolder = CreateNewTaskPanel(groupItem.GroupTitle, MainsplitContainer);
                grpPanel.AddNewGroupItem(groupItem);
            }
        }

        //currently not working, not used
        public void DeleteGroupByList(List<string> selectedGroupsNames, List<GroupItem> GroupList)
        {
            if (selectedGroupsNames == null) return;
            if (selectedGroupsNames.Count < 1) return;

            //string tempS = "";

            //foreach (string selectedName in selectedGroupsNames)
            //{
            //    tempS += selectedName;
            //}

            //MessageBox.Show(tempS);

            //for (int i = 0; i < GroupList.Count; i++)
            //{
            //    if (GroupList[i].GroupTitle == selectedGroupsNames[i])
            //    {
            //        GroupItem grpItem = GroupList[i];
            //        grpItem.Dispose();
            //        grpItem.TaskPanelHolder.Dispose();
            //        GroupList.Remove(grpItem);
            //    }
            //}

            try
            {
                foreach (GroupItem grpitem in GroupList)
                {
                    foreach (string selectedName in selectedGroupsNames)
                    {
                        if (selectedName == grpitem.GroupTitle)
                        {
                            grpitem.Dispose();
                            grpitem.TaskPanelHolder.Dispose();
                        }
                    }
                }
            }
            catch (InvalidOperationException e)
            {

            }

            GroupList.Clear();
        }

        //public void GRPitem_Button_Click(object? sender, EventArgs e)
        //{
        //    GroupItem b = (GroupItem)sender;

        //    SplitContainer splitContainer = (SplitContainer)b.Parent.Parent.Parent.Parent;

        //    foreach (Control panel in splitContainer.Panel2.Controls)
        //    {
        //        panel.Enabled = false;
        //        panel.Visible = false;
        //    }

        //    if (!b.TaskPanelHolder.Enabled)
        //    {
        //        b.TaskPanelHolder.Enabled = true;
        //        b.TaskPanelHolder.Visible = true;
        //    }
        //}

        public Panel CreateNewTaskPanel(string pname, SplitContainer Parent)
        {
            foreach (Control panel in Parent.Panel2.Controls)
            {
                panel.Enabled = false;
                panel.Visible = false;
            }

            Panel Taskpanel = new Panel();
            Taskpanel.Dock = DockStyle.Fill;

            Label label = new Label();
            label.Dock = DockStyle.Top;
            label.TextAlign = ContentAlignment.TopCenter;
            label.Text = pname;

            TaskInputBox inputbox = new TaskInputBox(Taskpanel);

            TaskListBox taskListBox = new TaskListBox(Taskpanel, DockStyle.Fill);

            inputbox.taskListBox = taskListBox;

            Taskpanel.Controls.Add(label);
            Parent.Panel2.Controls.Add(Taskpanel);
            return Taskpanel;
        }

        public void SelectWorkSpaceByTitle(string workSpaceTitle)
        {
            for (int i = 0; i < WorkSpaceHandler.TabCount; i++)
            {
                if (WorkSpaceHandler.TabPages[i].Text == workSpaceTitle)
                {
                    WorkSpaceHandler.SelectTab(i);
                }
            }
        }


        public void DeleteWorkSpaceByTitle(string workSpaceTitle)
        {
            //if (workSpaceTitle != "Dashboard")
            for (int i = 0; i < WorkSpaceHandler.TabCount; i++)
            {
                if (WorkSpaceHandler.TabPages[i].Text == workSpaceTitle)
                {
                    //WorkSpaceHandler.TabPages.Remove(WorkSpaceHandler.TabPages[i]);
                    WorkSpaceHandler.TabPages[i].Dispose();
                }
            }
        }

        private void TTP_FormClosed(object sender, FormClosedEventArgs e)
        {
            string storageText = "";
            //File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "wtf.txt"), storageText);   
        }
    }
}