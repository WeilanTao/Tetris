using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tetris.Utils
{
    public static class CloseWindow
    {
        public static Window WinObject;

        public static void CloseParent()
        {
            try
            {
                ((Window)WinObject).Close();
            }
            catch (Exception e)
            {
                string value = e.Message.ToString(); // do whatever with this
            }
        }
    }
}
