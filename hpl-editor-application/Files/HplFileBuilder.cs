using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hpl_editor_application.Files
{
    internal class HplFileBuilder
    {
        private readonly HplFile File;
        private StringBuilder Builder;

        public HplFileBuilder(HplFile file)
        {
            File = file;
        }

        public override string ToString()
        {
            Builder = new StringBuilder();

            AddSection("GlobalCode", File.GlobalCode);
            AddSection("OnStart", File.OnStartCode);
            AddSection("OnEnter", File.OnEnterCode);
            AddSection("OnLeave", File.OnLeaveCode);
            AddSection("MainCode", File.MainCode);

            return Builder.ToString();
        }

        private void AddSection(string name, string contents)
        {
            Builder.AppendLine($"// @{name}");
            Builder.AppendLine(contents);
            Builder.AppendLine($"// @{name}End\n\n");
        }
    }
}
