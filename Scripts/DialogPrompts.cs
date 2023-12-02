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
            CheckedListBoxImproved listBox = new CheckedListBoxImproved();
            Button buttonCreate = new Button();
            Button buttonCancel = new Button();

            groupListDeleteManager listDeleteManager = new groupListDeleteManager(GroupListOfWorkspace, listBox);

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

            return dialogResult == DialogResult.OK ? listDeleteManager.GetGrpList() : new List<string>();
        }

        public static List<string> DeleteWSDialogByList(string FormLabel, string PromptText, List<Workspace> ListOfWorkspace)
        {
            if (ListOfWorkspace.Count == 0) return null;

            Form form = new Form();
            Label label = new Label();
            CheckedListBoxImproved listBox = new CheckedListBoxImproved();
            Button buttonCreate = new Button();
            Button buttonCancel = new Button();

            workspaceDeleteListManager listDeleteManager = new workspaceDeleteListManager(ListOfWorkspace, listBox);

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

            return dialogResult == DialogResult.OK ? listDeleteManager.GetWSList() : new List<string>();
        }
    }
}
