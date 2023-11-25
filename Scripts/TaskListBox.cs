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

        public TaskListBox(Control Parent, DockStyle dockStyle) : base(Parent, dockStyle)
        {

        }

        public void CreateNewTask(string TaskName)
        {
            if (!this.Items.Contains(TaskName.Trim()))
                this.Items.Add(TaskName.Trim());
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
