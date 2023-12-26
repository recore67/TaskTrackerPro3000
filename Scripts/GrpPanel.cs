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

        public GroupItem CreateNewGroup(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                string TitleFormatted = title.Trim();

                foreach (GroupItem item in groupItems)
                {
                    if (item.GroupTitle == TitleFormatted)
                        return null;
                }

                SplitContainer MainsplitContainer = (SplitContainer)this.Parent.Parent;

                GroupItem groupItem = new GroupItem(TitleFormatted);
                groupItem.TaskPanelHolder = new TaskPanel(groupItem.GroupTitle, MainsplitContainer);
                this.AddNewGroupItem(groupItem);

                return groupItem;
            }
            else
            {
                return null;
            }
        }
    }
}
