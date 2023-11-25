using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerPro3000.Scripts
{
    internal class workspaceDeleteListManager
    {
        public List<string> selectedWSList;
        CheckedListBoxImproved _listBox;

        public workspaceDeleteListManager(List<Workspace> WSList, CheckedListBoxImproved newlistBox)
        {
            selectedWSList = new List<string>();
            _listBox = newlistBox;
            foreach (Workspace item in WSList)
            {
                _listBox.Items.Add(item.WSTitle);
            }

            _listBox.ItemChecked += UpdateStringWSList;
        }

        void UpdateStringWSList()
        {
            //for (int i = 0; i < _listBox.SelectedItems.Count; i++)
            //{
            //    selectedGroupsList.Add(_listBox.GetItemText(_listBox.Items[i]));
            //}

            selectedWSList.Clear();

            foreach (object itemChecked in _listBox.CheckedItems)
            {
                string itemTitle = _listBox.GetItemText(itemChecked);
                selectedWSList.Add(itemTitle);

                //foreach (string itemInList in selectedGroupsList)
                //{
                //    if (itemTitle != itemInList)
                //        selectedGroupsList.Add(itemTitle);
                //}
            }
        }

        public List<string> GetWSList()
        {
            UpdateStringWSList();
            return selectedWSList;
        }
    }
}
