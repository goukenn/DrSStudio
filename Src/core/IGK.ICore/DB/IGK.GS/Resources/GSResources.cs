using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Resources
{
    public static class GSResources
    {
        public static readonly string TaskButtonDocument = "task_button";
        public static readonly string TaskButtonBackgroundDocument = "task_button_background";
        public static readonly string task_group_item_layout = "task_group_item_layout";
        private static GSResourcesID sm_id;


        public static GSResourcesID Id
        {
            get {
                return sm_id;
            }
        }

        static GSResources() {
            sm_id = new GSResourcesID();
        }
        public class GSResourcesID {
            public readonly string Title = "title";
        }
    }
}
