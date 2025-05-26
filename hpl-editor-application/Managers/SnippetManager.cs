using hpl_editor_application.Code;
using hpl_editor_application.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace hpl_editor_application.Managers
{
    internal static class SnippetManager
    {
        internal static ObservableCollection<CodeSnippetCategory> Snippets = new ObservableCollection<CodeSnippetCategory>();

        public static void Initialise()
        {
            LoadSnippets("Code/DefaultCodeSnippets.xml");
            LoadCustomSnippets();
        }

        private static void LoadCustomSnippets()
        {
            if (Directory.Exists("Code/Custom"))
            {
                foreach (var file in Directory.GetFiles("Code/Custom"))
                {
                    LoadSnippets(file);
                }
            }
        }

        public static void LoadSnippets(string path)
        {
            if(!Path.Exists(path))
            {
                MainWindow.Instance.SetStatus($"Path Not Found: {path}.");
                return;
            }

            using StreamReader reader = new StreamReader(path);
            XmlSerializer serializer = new XmlSerializer(typeof(CodeSnippetFile));
            var result = (CodeSnippetFile?) serializer.Deserialize(reader);

            if(result == null)
            {
                MainWindow.Instance.SetStatus($"Error Loading File, Null Returned.");
                return;
            }

            foreach (var category in result.Categories)
                Snippets.Add(category);
        }
    }
}
