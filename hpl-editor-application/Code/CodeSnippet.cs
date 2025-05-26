using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace hpl_editor_application.Code
{

    public class CodeSnippetCategory
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        public CodeSnippet[]? Snippets { get; set; }
    }

    public class CodeSnippet
    {
        public readonly Guid Id = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
