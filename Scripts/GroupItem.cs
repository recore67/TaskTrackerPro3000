using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTrackerPro3000.Scripts
{
    public class GroupItem : Button
    {
        public Panel TaskPanelHolder = null;

        public string GroupTitle;

        public GroupItem(string Title)
        {
            GroupTitle = Title;
        }
    }
}
