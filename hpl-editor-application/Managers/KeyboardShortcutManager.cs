using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hpl_editor_application.Managers
{
    internal class KeyboardShortcutManager
    {


        public static void Initialise()
        {
            Keyboard.AddKeyUpHandler(MainWindow.Instance, KeyListener);
        }

        private static void KeyListener(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.S)
                    SaveShortcut();
                else if (e.Key == Key.O)
                    OpenShortcut();
            }
        }

        private static void SaveShortcut()
        {
            MainWindow.File.SaveDialog();
        }

        private static void OpenShortcut()
        {
            MainWindow.Instance.OpenFile();
        }
    }
}
