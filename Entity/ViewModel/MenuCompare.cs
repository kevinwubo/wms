using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class MenuCompare : IEqualityComparer<MenuEntity>
    {
        public bool Equals(MenuEntity b1, MenuEntity b2)
        {
            if (b1.MenuID == b2.MenuID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode(MenuEntity bx)
        {
            int hCode = bx.MenuID.GetHashCode() ^ bx.MenuID.GetHashCode();
            return hCode.GetHashCode();
        }
    }
}
