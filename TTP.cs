using System.Windows.Forms;
using System.IO;
using TaskTrackerPro3000.Scripts;
using System.Net.Http.Headers;
using System.Collections;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskTrackerPro3000
{
    public partial class TTP : Form
    {
        private bool LoadSessionFile = false;
        private bool SaveSessionFile = false;

        string pathsession = Path.Combine(Directory.GetCurrentDirectory(), "storage.json");

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
        }

        private void TTP_Load(object sender, EventArgs e)
        {
#if DEBUG
            if (LoadSessionFile)
                loadSession();
#else
            loadSession();
#endif
        }

        private void loadSession()
        {
            if (File.Exists(pathsession))
            {
                string sessionFile = File.ReadAllText(pathsession);

                var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<bool, Dictionary<string, Dictionary<string, bool>>>>>(sessionFile);

                TabPage lastSelectedTab = new TabPage();

                // Access the deserialized data
                foreach (var workspaceData in deserializedData)
                {
                    Workspace newWorkspacePage = new Workspace(workspaceData.Key.Trim());
                    newWorkspacePage._MS_workspace.ItemClicked += MS_workspace_ItemClicked;

                    WorkSpaceHandler.TabPages.Add(newWorkspacePage);

                    foreach (var selectedTab in workspaceData.Value)
                    {
                        if (selectedTab.Key == true)
                        {
                            lastSelectedTab = newWorkspacePage;
                        }

                        foreach (var groupData in selectedTab.Value)
                        {

                            GroupItem newGroup = newWorkspacePage.groupPanel.CreateNewGroup(groupData.Key.Trim());

                            foreach (var taskData in groupData.Value)
                            {
                                Dictionary<string, bool> taskslist = new Dictionary<string, bool>
                            {
                                { taskData.Key, taskData.Value }
                            };

                                newGroup.TaskPanelHolder.taskListBox.AddTasks(taskslist);
                            }
                        }
                    }
                }

                WorkSpaceHandler.SelectTab(lastSelectedTab);
            }
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            string aboutMenuText = "TaskTrackerPro3000 is an open-source extensive task tracker app\n" +
                "https://github.com/recore67/TaskTrackerPro3000";
            MessageBox.Show(aboutMenuText);
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
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

        public void CreateRecursiveNewWorkspace(string Title, string groupTitle, Dictionary<string, bool> tasks)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                string TitleFormated = Title.Trim();

                Workspace newWorkspacePage = new Workspace(TitleFormated);
                newWorkspacePage._MS_workspace.ItemClicked += MS_workspace_ItemClicked;

                GroupItem groupItem = newWorkspacePage.groupPanel.CreateNewGroup(groupTitle);

                groupItem.TaskPanelHolder.taskListBox.AddTasks(tasks);

                WorkSpaceHandler.TabPages.Add(newWorkspacePage);

                WorkSpaceHandler.SelectTab(newWorkspacePage);
            }
        }

        private void MS_workspace_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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
                groupItem.TaskPanelHolder = new TaskPanel(groupItem.GroupTitle, MainsplitContainer);
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

            List<GroupItem> grpItemsRemove = new List<GroupItem>();


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

            //GC.WaitForPendingFinalizers();
            //GC.Collect();
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

            //GC.WaitForPendingFinalizers();
            //GC.Collect();
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

        //disaster
        private void TTP_FormClosed(object sender, FormClosedEventArgs e)
        {
#if DEBUG
            if (SaveSessionFile)
                SaveSession();
#else
                SaveSession();
#endif
        }

        private void SaveSession()
        {
            if (WorkSpaceHandler.TabCount != 0)
            {
                List<Workspace> WSItems = WorkSpaceHandler.TabPages.Cast<Workspace>().ToList<Workspace>();
                Workspace selectedWS = (Workspace)WorkSpaceHandler.SelectedTab;

                var data = new Dictionary<string, Dictionary<bool, Dictionary<string, Dictionary<string, bool>>>>();
                foreach (Workspace ws in WSItems)
                {
                    Dictionary<string, Dictionary<string, bool>> groupNameWithTasks = new Dictionary<string, Dictionary<string, bool>>();

                    foreach (GroupItem grp in ws.groupPanel.groupItems)
                    {
                        groupNameWithTasks.Add(grp.GroupTitle, grp.TaskPanelHolder.taskListBox.getCorrectTasks());
                    }

                    Dictionary<bool, Dictionary<string, Dictionary<string, bool>>> lastSelectedandgroupNameWithTasks = new Dictionary<bool, Dictionary<string, Dictionary<string, bool>>>();

                    lastSelectedandgroupNameWithTasks.Add(selectedWS == ws ? true : false, groupNameWithTasks);

                    data.Add(ws.WSTitle, lastSelectedandgroupNameWithTasks);

                    if (WorkSpaceHandler.SelectedTab == ws)
                    {
                        selectedWS = ws;
                    }
                }

                File.WriteAllText(pathsession, JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }

    }
}