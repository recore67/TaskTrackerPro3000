using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTrackerPro3000.Scripts
{
    internal class DialogPrompts
    {
        //static List<string> selectedGroupsList = new();
        //static TaskListBox _listBox;


        public static string CreateDialog(string FormLabel, string PromptText)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonCreate = new Button();
            Button buttonCancel = new Button();

            form.Text = FormLabel;
            label.Text = PromptText;

            buttonCreate.Text = "Create";
            buttonCancel.Text = "Cancel";
            buttonCreate.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(90, 36, 140, 13);
            textBox.SetBounds(36, 86, 200, 20);
            buttonCreate.SetBounds(36, 140, 90, 35);
            buttonCancel.SetBounds(150, 140, 90, 35);

            label.AutoSize = true;
            form.ClientSize = new Size(280, 207);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            form.Controls.AddRange(new Control[] { label, textBox, buttonCreate, buttonCancel });
            form.AcceptButton = buttonCreate;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();

            return dialogResult == DialogResult.OK ? textBox.Text : string.Empty;
        }

        public static List<string> DeleteGroupDialogByList(string FormLabel, string PromptText, List<GroupItem> GroupListOfWorkspace)
        {
            if (GroupListOfWorkspace.Count == 0) return null;

            Form form = new Form();
            Label label = new Label();
            TaskListBox listBox = new TaskListBox(null, DockStyle.None);
            Button buttonCreate = new Button();
            Button buttonCancel = new Button();

            groupListDeleteManager listDeleteManager = new groupListDeleteManager(GroupListOfWorkspace, listBox);

            //listBox.SelectedGroupsDelete += SelectedNameResult;

            //foreach (GroupItem item in GroupListOfWorkspace)
            //{
            //    listBox.Items.Add(item.GroupTitle);
            //}

            //_listBox = listBox;

            form.Text = FormLabel;
            label.Text = PromptText;

            buttonCreate.Text = "Delete";
            buttonCancel.Text = "Cancel";
            buttonCreate.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(90, 36, 140, 13);
            listBox.SetBounds(36, 86, 200, 120);
            buttonCreate.SetBounds(36, 240, 90, 35);
            buttonCancel.SetBounds(150, 240, 90, 35);

            label.AutoSize = true;
            form.ClientSize = new Size(280, 300);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            form.Controls.AddRange(new Control[] { label, listBox, buttonCreate, buttonCancel });
            form.AcceptButton = buttonCreate;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();

            return dialogResult == DialogResult.OK ? listDeleteManager.selectedGroupsList : new List<string>();
        }

        //static void SelectedNameResult()
        //{
        //    for (int i = 0; i < _listBox.SelectedItems.Count; i++)
        //    {
        //        selectedGroupsList.Add(_listBox.GetItemText(_listBox.Items[i]));
        //    }
        //}

        //static List<string> SelectedNameResult(TaskListBox _listBox)
        //{
        //    List<string> result = new List<string>();

        //    for (int i = 0; i < _listBox.SelectedItems.Count; i++)
        //    {
        //        result.Add(_listBox.GetItemText(_listBox.Items[i]));
        //    }

        //    return result;
        //}
    }
}
