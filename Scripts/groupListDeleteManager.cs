﻿using System;
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

        void SelectedNameResult()
        {
            //for (int i = 0; i < _listBox.SelectedItems.Count; i++)
            //{
            //    selectedGroupsList.Add(_listBox.GetItemText(_listBox.Items[i]));
            //}

            selectedGroupsList.Clear();

            foreach (object itemChecked in _listBox.CheckedItems)
            {
                string itemTitle = _listBox.GetItemText(itemChecked);
                selectedGroupsList.Add(itemTitle);

                //foreach (string itemInList in selectedGroupsList)
                //{
                //    if (itemTitle != itemInList)
                //        selectedGroupsList.Add(itemTitle);
                //}
            }
        }
    }
}
