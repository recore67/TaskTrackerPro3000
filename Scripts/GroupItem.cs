using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskTrackerPro3000.Scripts
{
    public class GroupItem : Panel
    {
        public TaskPanel TaskPanelHolder = null;

        public string GroupTitle;

        public GroupItem(string Title)
        {
            GroupTitle = Title;
            Dock = DockStyle.Top;
            CreateNewGroupItem(Title);
        }

        private void CreateNewGroupItem(string grpTitle)
        {
            Button grpbutton = new Button();
            grpbutton.Text = grpTitle;
            grpbutton.Dock = DockStyle.Fill;
            grpbutton.Click += GRPitem_Button_Click;

            Controls.Add(grpbutton);
        }

        void GRPitem_Button_Click(object? sender, EventArgs e)
        {
            SplitContainer splitContainer = (SplitContainer)Parent.Parent.Parent;

            foreach (Control panel in splitContainer.Panel2.Controls)
            {
                panel.Enabled = false;
                panel.Visible = false;
            }

            if (!TaskPanelHolder.Enabled)
            {
                TaskPanelHolder.Enabled = true;
                TaskPanelHolder.Visible = true;
            }
        }
    }
}
