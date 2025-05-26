using hpl_editor_application.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace hpl_editor_application.Files
{
    [Serializable, XmlRoot("Categories")]
    public class CodeSnippetFile
    {
        [XmlElement(ElementName = "Category")]
        public CodeSnippetCategory[] Categories { get; set; }
    }
}
