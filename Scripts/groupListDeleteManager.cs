using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerPro3000.Scripts
{
    internal class groupListDeleteManager
    {
        public List<string> selectedGroupsList;
         TaskListBox _listBox;

        public groupListDeleteManager(List<GroupItem> groupList, TaskListBox newlistBox)
        {
            selectedGroupsList = new List<string>();
            _listBox = newlistBox;
            foreach (GroupItem item in groupList)
            {
                _listBox.Items.Add(item.GroupTitle);
            }

            _listBox.SelectedGroupsDelete += SelectedNameResult;
        }

        void InitiateList(List<GroupItem> _groupList)
        {
            foreach (GroupItem item in _groupList)
            {
                _listBox.Items.Add(item.GroupTitle);
            }

            _listBox.SelectedGroupsDelete += SelectedNameResult;
        }

        void SelectedNameResult()
        {
            for (int i = 0; i < _listBox.SelectedItems.Count; i++)
            {
                selectedGroupsList.Add(_listBox.GetItemText(_listBox.Items[i]));
            }
        }
    }
}
