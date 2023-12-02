using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTrackerPro3000.Scripts
{
    public class TaskListBox : CheckedListBoxImproved
    {
        //bad code, whole idea is rough

        public ImprovedListBox listBox;

        public TaskListBox(SplitContainer container, DockStyle dockStyle) : base(dockStyle)
        {
            listBox = new ImprovedListBox();
            Font listBoxFont = new Font(listBox.Font, FontStyle.Strikeout);
            listBox.Font = listBoxFont;
            listBox.MultiColumn = true;
            this.ItemChecked += TransferCheckedTask;
            CreateNewTaskListBox(this, listBox, container);
        }

        private void CreateNewTaskListBox(CheckedListBoxImproved taskListBox, ImprovedListBox ListBox, SplitContainer container)
        {
            Button clearListButton = new Button();
            clearListButton.Click += ClearListButton_Click;
            clearListButton.Dock = DockStyle.Top;
            clearListButton.Text = "Clear List";

            container.Panel1.Controls.Add(taskListBox);
            container.Panel2.Controls.Add(ListBox);
            container.Panel2.Controls.Add(clearListButton);
        }

        public void CreateNewTask(string TaskName)
        {
            string trimTaskName = TaskName.Trim();
            if (!this.Items.Contains(trimTaskName) && !listBox.Items.Contains(trimTaskName)) 
            {
                this.Items.Add(trimTaskName);
            }
        }
        private void ClearListButton_Click(object? sender, EventArgs e)
        {
            listBox.Items.Clear();
        }

        public void TransferCheckedTask()
        {
            while (this.CheckedItems.Count > 0)
            {
                object checkedItem = this.CheckedItems[0];
                listBox.Items.Add(checkedItem);
                this.Items.Remove(checkedItem);
            }
        }
    }
}
