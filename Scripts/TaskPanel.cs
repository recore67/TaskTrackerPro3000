using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerPro3000.Scripts
{
    public class TaskPanel : Panel
    {
        public TaskListBox taskListBox = null;

        public TaskPanel(string Title, SplitContainer ParentContainer)
        {
            CreateNewTaskPanel(Title, ParentContainer);
        }

        public void CreateNewTaskPanel(string pname, SplitContainer Parent)
        {
            foreach (Control panel in Parent.Panel2.Controls)
            {
                panel.Enabled = false;
                panel.Visible = false;
            }

            this.Dock = DockStyle.Fill;

            SplitContainer tasklistpanel = new SplitContainer();
            tasklistpanel.Dock = DockStyle.Fill;
            tasklistpanel.SplitterDistance = 100;

            Label label = new Label();
            label.Dock = DockStyle.Top;
            label.TextAlign = ContentAlignment.TopCenter;
            label.Text = pname;

            TaskInputBox inputbox = new TaskInputBox(this);

            taskListBox = new TaskListBox(tasklistpanel, DockStyle.Fill);

            inputbox.taskListBox = taskListBox;

            this.Controls.Add(tasklistpanel);
            this.Controls.Add(label);
            Parent.Panel2.Controls.Add(this);
        }
    }
}
