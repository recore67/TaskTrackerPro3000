using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTrackerPro3000.Scripts
{
    public class TaskListBox : CheckedListBoxImproved
    {
        //bad code, whole idea is rough

        public ImprovedListBox checkedtasklistBox;

        public TaskListBox(SplitContainer container, DockStyle dockStyle) : base(dockStyle)
        {
            checkedtasklistBox = new ImprovedListBox();
            Font listBoxFont = new Font(checkedtasklistBox.Font, FontStyle.Strikeout);
            checkedtasklistBox.Font = listBoxFont;
            checkedtasklistBox.MultiColumn = true;
            this.ItemChecked += TransferCheckedTask;
            CreateNewTaskListBox(this, checkedtasklistBox, container);
        }

        private void CreateNewTaskListBox(CheckedListBoxImproved taskListBox, ImprovedListBox ListBox, SplitContainer container)
        {
            Button clearListButton = new Button();
            clearListButton.Text = "Clear List";
            clearListButton.Dock = DockStyle.Top;
            clearListButton.Click += ClearListButton_Click;

            container.Panel1.Controls.Add(taskListBox);
            container.Panel2.Controls.Add(ListBox);
            container.Panel2.Controls.Add(clearListButton);
        }

        public void AddTasks(Dictionary<string, bool> tasks)
        {
            foreach (KeyValuePair<string, bool> task in tasks)
            {
                string trimTaskName = task.Key.Trim();

                if (!task.Value)
                {
                    this.Items.Add(trimTaskName);
                }
                else
                {
                    checkedtasklistBox.Items.Add(trimTaskName);
                }
            }

        }

        public void CreateNewTask(string TaskName)
        {
            string trimTaskName = TaskName.Trim();
            if (!this.Items.Contains(trimTaskName) && !checkedtasklistBox.Items.Contains(trimTaskName))
            {
                this.Items.Add(trimTaskName);
            }
        }

        private void ClearListButton_Click(object? sender, EventArgs e)
        {
            checkedtasklistBox.Items.Clear();

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public void TransferCheckedTask()
        {
            while (this.CheckedItems.Count > 0)
            {
                object checkedItem = this.CheckedItems[0];
                checkedtasklistBox.Items.Add(checkedItem);
                this.Items.Remove(checkedItem);
            }
        }

        public List<string> getTasks()
        {
            List<string> tasks = new List<string>();

            foreach (var item in Items)
            {
                tasks.Add(GetItemText(item));
            }

            return tasks;
        }

        public Dictionary<string, bool> getCorrectTasks()
        {
            Dictionary<string, bool> tasks = new Dictionary<string, bool>();

            foreach (var item1 in Items)
            {
                tasks.Add(GetItemText(item1), false);
            }

            foreach (var item2 in checkedtasklistBox.Items)
            {
                tasks.Add(GetItemText(item2), true);
            }

            return tasks;
        }
    }
}
