using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Resources;
using hpl_editor_application.Files;
using Microsoft.Win32;
using hpl_editor_application.Managers;

namespace hpl_editor_application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;
        internal static HplFile File;

        public MainWindow()
        {
            Instance = this;

            KeyboardShortcutManager.Initialise();

            InitializeComponent();
            LoadEditor();

            File = HplFile.Create();
        }

        private void LoadEditor()
        {
            using Stream s = new MemoryStream(Properties.Resources.HplRules);
            using XmlReader reader = XmlReader.Create(s);

            var highlighter = HighlightingLoader.Load(reader, HighlightingManager.Instance);

            MainEditor.SyntaxHighlighting = 
                OnStartEditor.SyntaxHighlighting = 
                OnEnterEditor.SyntaxHighlighting = 
                OnLeaveEditor.SyntaxHighlighting = 
                GlobalEditor.SyntaxHighlighting = highlighter;

            SetTitle();
        }

        public void OpenFile()
        {
            var file = HplFile.Load();

            if (file != null)
                File = file;
            else
                SetStatus("Open File Cancelled");
        }

        public void SetStatus(string status, int time = 4000)
        {
            Task.Run(async () =>
            {
                Dispatcher.Invoke(() => StatusBarLabel.Content = status);
                Thread.Sleep(time);
                Dispatcher.Invoke(() => StatusBarLabel.Content = string.Empty);
            });
        }

        public void SetTitle(string? title = null)
        {
            if (title == null)
                Title = "HPL Script Studio";
            else
                Title = $"{title} – HPL Script Studio";
        }

        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            File = HplFile.Create();
        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            File.SaveDialog();
        }

        private void MenuLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            File.SaveDialog(true);
        }
    }
}   