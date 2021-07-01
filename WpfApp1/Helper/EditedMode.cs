using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdressBookW.Helper
{
    public enum EditedMode
    {
        Edit = 1,
        Newly = 0
    };

    public static class DialogMode
    {
        public static bool GetMode(EditedMode mode) { return mode == 0 ? false : true; }

    }
}
