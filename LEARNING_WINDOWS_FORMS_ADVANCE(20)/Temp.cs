using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
    public static class Temp
    {
        static Temp()
        {
        }

        private static List<MenuStatus> _menuList = null;

        public static List<MenuStatus> MenuList
        {
            get
            {
                if (_menuList == null)
                {
                    _menuList = new List<MenuStatus>();
                }

                return _menuList;
            }
        }

    }
}
