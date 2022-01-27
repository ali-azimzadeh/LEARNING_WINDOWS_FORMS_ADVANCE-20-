using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
    public class MenuStatus : Object
    {
        public MenuStatus(int menuID, bool menuIsActive)
            : base()
        {
            MenuID = menuID;
            MenuIsActive = menuIsActive;
        }

        public MenuStatus() : base()
        {
        }

        public int MenuID { get; set; }
        public bool MenuIsActive { get; set; }
    }
}
