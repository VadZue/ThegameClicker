using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TheGame
{
    public static class ClassOpenWindow
    {
        public static void OpenWindow(this Window pref, Window Opened)
        {
            Opened.Show();
            pref.Hide();
            Opened.Closing += (x, y) => pref.Show();
        }
    }
}
