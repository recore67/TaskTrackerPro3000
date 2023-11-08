using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerPro3000.Scripts
{
    public class GrpPanel : Panel
    {
        public List<GroupItem> groupItems;

        public GrpPanel()
        {
            groupItems = new List<GroupItem>();
        }

        public void AddNewGroupItem(GroupItem item)
        {
            Controls.Add(item);

            foreach (GroupItem iteminList in Controls)
            {
                if (iteminList.GroupTitle == item.GroupTitle)
                    groupItems.Add(iteminList);
            }
        }
    }
}
