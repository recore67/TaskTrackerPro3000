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
    }
}
