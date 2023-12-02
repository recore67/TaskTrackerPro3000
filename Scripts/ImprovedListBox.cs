using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerPro3000.Scripts
{
    public class ImprovedListBox : ListBox
    {
        public ImprovedListBox(DockStyle dockStyle = DockStyle.Fill)
        {
            this.Dock = dockStyle;
            this.Leave += ImprovedListBox_Leave;
        }

        private void ImprovedListBox_Leave(object? sender, EventArgs e)
        {
            ImprovedListBox ListBox1 = (ImprovedListBox)sender;


            ListBox1.ClearSelected();
        }
    }
}
