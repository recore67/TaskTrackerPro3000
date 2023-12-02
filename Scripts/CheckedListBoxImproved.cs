using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTrackerPro3000.Scripts
{
    public class CheckedListBoxImproved : CheckedListBox
    {

        public delegate void onItemChecked();
        public event onItemChecked ItemChecked;

        public CheckedListBoxImproved(DockStyle dockStyle = DockStyle.None)
        {
            this.Dock = dockStyle;
            this.CheckOnClick = false;
            this.KeyDown += CheckedListBox_KeyDown;
            this.Leave += CheckedListBox_Leave;
            this.ItemCheck += CheckedListBox_ItemCheck;
        }

        //public virtual void CreateNewListBox(CheckedListBox checkedListBox, DockStyle dockStyle, Control controlToParent)
        //{
        //    checkedListBox.Dock = dockStyle;

        //    if (controlToParent != null) controlToParent.Controls.Add(checkedListBox);
        //}

        public virtual void CheckedListBox_KeyDown(object? sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        public virtual void CheckedListBox_Leave(object? sender, EventArgs e)
        {
            CheckedListBox checkedListBox1 = (CheckedListBox)sender;


            checkedListBox1.ClearSelected();
        }

        //checks the item being checked before it does
        public async virtual void CheckedListBox_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            CheckedListBox checkedListBox1 = (CheckedListBox)sender;


            //if the click isn't on an item (and on the white space)
            if (checkedListBox1.IndexFromPoint(checkedListBox1.PointToClient(Cursor.Position).X,
            checkedListBox1.PointToClient(Cursor.Position).Y) <= -1)
            {
                e.NewValue = e.CurrentValue;
                checkedListBox1.ClearSelected();
            }

            await Task.Delay(100);
            ItemChecked?.Invoke();
        }
    }
}
