using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace hpl_editor_application.Files
{
    internal class HplFile
    {
        public string? FilePath { get => filePath; protected set => filePath = value; }
        private string? filePath = null;

        public string GlobalCode { get => Window.GlobalEditor.Text; set { Window.GlobalEditor.Text = value; } }
        public string OnStartCode { get => Window.OnStartEditor.Text; set { Window.OnStartEditor.Text = value; } }
        public string OnEnterCode { get => Window.OnEnterEditor.Text; set { Window.OnEnterEditor.Text = value; } }
        public string OnLeaveCode { get => Window.OnLeaveEditor.Text; set { Window.OnLeaveEditor.Text = value; } }
        public string MainCode { get => Window.MainEditor.Text; set { Window.MainEditor.Text = value; } }

        private MainWindow Window => MainWindow.Instance;

        protected HplFile(string? path = null)
        {
        }

        public void SaveDialog(bool force = false)
        {
            if(FilePath != null && !force)
            {
                Save(FilePath);
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.DefaultExt = "hps";

            dialog.ShowDialog();

            if (dialog.FileName != string.Empty)
            {
                Save(dialog.FileName);
                MainWindow.Instance.SetStatus($"Saved File: {dialog.FileName}", 4000);
            }
            else
                MainWindow.Instance.SetStatus($"Save Cancelled.");
        }

        public void Save(string path)
        {
            var builder = new HplFileBuilder(this);

            File.WriteAllText(path, builder.ToString());

            FilePath = path;
            MainWindow.Instance.SetStatus($"Saved File: {path}", 4000);
        }

        public static HplFile Create()
        {
            var file = new HplFile();

            file.OnStartCode = "void OnStart()\n{\n\n}";
            file.OnEnterCode = "void OnEnter()\n{\n\n}";
            file.OnLeaveCode = "void OnLeave()\n{\n\n}";

            return file;
        }

        public static HplFile? Load(string? path = null)
        {
            if(path == null)
            {
                var dialog = new OpenFileDialog()
                {
                    DefaultExt = "hpl"
                };

                dialog.ShowDialog();

                if (dialog.FileName != string.Empty)
                    path = dialog.FileName;
                else
                    return null;
            }

            var loader = new HplFileParser(path);

            MainWindow.Instance.SetTitle(System.IO.Path.GetFileName(path));

            MainWindow.Instance.SetStatus($"Loaded File: {path}");

            return loader;
        }
    }
}
