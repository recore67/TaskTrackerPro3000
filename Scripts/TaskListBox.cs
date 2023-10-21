using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTrackerPro3000.Scripts
{
    internal class TaskListBox : CheckedListBox
    {
        CheckedListBox checkedListBoxHolder;

        public TaskListBox(Control Parent, DockStyle dockStyle)
        {
            checkedListBoxHolder = this;
            CreateNewTaskListBox(checkedListBoxHolder, dockStyle, Parent);
        }

        void CreateNewTaskListBox(CheckedListBox checkedListBox, DockStyle dockStyle, Control controlToParent)
        {
            checkedListBox.Dock = dockStyle;
            checkedListBox.CheckOnClick = false;
            checkedListBox.KeyDown += CheckedListBox_KeyDown;
            checkedListBox.Leave += CheckedListBox_Leave;
            checkedListBox.ItemCheck += CheckedListBox_ItemCheck;

            if(controlToParent != null) controlToParent.Controls.Add(checkedListBox);
        }

        public void CreateNewTask(string TaskName)
        {
            if (!checkedListBoxHolder.Items.Contains(TaskName.Trim()))
                checkedListBoxHolder.Items.Add(TaskName.Trim());
        }

        private void CheckedListBox_KeyDown(object? sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void CheckedListBox_Leave(object? sender, EventArgs e)
        {
            CheckedListBox checkedListBox1 = (CheckedListBox)sender;

            checkedListBox1.ClearSelected();
        }

        //checks the item being checked before it does
        private void CheckedListBox_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            CheckedListBox checkedListBox1 = (CheckedListBox)sender;

            //if the click isn't on an item (and on the white space)
            if (checkedListBox1.IndexFromPoint(checkedListBox1.PointToClient(Cursor.Position).X,
            checkedListBox1.PointToClient(Cursor.Position).Y) <= -1)
            {
                e.NewValue = e.CurrentValue;
                checkedListBox1.ClearSelected();
            }
        }
    }
}
