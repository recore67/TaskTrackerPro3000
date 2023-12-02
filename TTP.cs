using System.Windows.Forms;
using System.IO;
using TaskTrackerPro3000.Scripts;
using System.Net.Http.Headers;
using System.Collections;

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
        public static string WS_DeleteForm_Text = "Workspace Terminator";
        public static string WS_DeleteFrom_Prompt = "Select Workspace to delete";

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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Workspace> WSItems = WorkSpaceHandler.TabPages.Cast<Workspace>().ToList<Workspace>();
            DeleteWorkspaceByList(DialogPrompts.DeleteWSDialogByList(WS_DeleteForm_Text, WS_DeleteFrom_Prompt, WSItems), WSItems);
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

                Workspace newWorkspacePage = new Workspace(TitleFormated);
                newWorkspacePage._MS_workspace.ItemClicked += MS_workspace_ItemClicked;

                //finally add new workspace to tabcontrol
                WorkSpaceHandler.TabPages.Add(newWorkspacePage);

                //SelectWorkSpaceByTitle(TitleFormated);
                WorkSpaceHandler.SelectTab(newWorkspacePage);
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

            List<GroupItem> grpItemsRemove = new();


            foreach (GroupItem grpitem in GroupList)
            {
                foreach (string selectedName in selectedGroupsNames)
                {
                    if (selectedName == grpitem.GroupTitle)
                    {
                        grpItemsRemove.Add(grpitem);
                        grpitem.Dispose();
                        grpitem.TaskPanelHolder.Dispose();
                    }
                }
            }

            foreach (GroupItem item in grpItemsRemove)
            {
                GroupList.Remove(item);
            }

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public void DeleteWorkspaceByList(List<string> selectedWSNames, List<Workspace> WSList)
        {
            if (selectedWSNames == null) return;
            if (selectedWSNames.Count < 1) return;

            //string tempS = "";

            //foreach (string selectedName in selectedGroupsNames)
            //{
            //    tempS += selectedName;
            //}

            //MessageBox.Show(tempS);

            foreach (Workspace wsitem in WSList)
            {
                foreach (string selectedName in selectedWSNames)
                {
                    if (selectedName == wsitem.WSTitle)
                    {
                        wsitem.Dispose();
                    }
                }
            }

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public Panel CreateNewTaskPanel(string pname, SplitContainer Parent)
        {
            foreach (Control panel in Parent.Panel2.Controls)
            {
                panel.Enabled = false;
                panel.Visible = false;
            }

            Panel Taskpanel = new Panel();
            Taskpanel.Dock = DockStyle.Fill;

            SplitContainer tasklistpanel = new SplitContainer();
            tasklistpanel.Dock = DockStyle.Fill;
            tasklistpanel.SplitterDistance = 100;

            Label label = new Label();
            label.Dock = DockStyle.Top;
            label.TextAlign = ContentAlignment.TopCenter;
            label.Text = pname;

            TaskInputBox inputbox = new TaskInputBox(Taskpanel);

            TaskListBox taskListBox = new TaskListBox(tasklistpanel, DockStyle.Fill);

            inputbox.taskListBox = taskListBox;

            Taskpanel.Controls.Add(tasklistpanel);
            Taskpanel.Controls.Add(label);
            Parent.Panel2.Controls.Add(Taskpanel);
            return Taskpanel;
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
            //File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "wtf.txt"), storageText);   
        }
    }
}