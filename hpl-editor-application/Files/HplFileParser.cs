using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hpl_editor_application.Files
{
    internal class HplFileParser : HplFile
    {
        private readonly string Contents;

        public HplFileParser(string path) : base()
        {
            var contents = File.ReadAllText(path);
            Contents = contents;

            Load();

            FilePath = path;
        }

        private void Load()
        {
            OnStartCode = GetOnStart();
            OnEnterCode = GetOnEnter();
            OnLeaveCode = GetOnLeave();
            MainCode = GetMainCode();
            GlobalCode = GetGlobalCode();
        }

        private string GetOnStart() => GetSection("OnStart");
        private string GetOnEnter() => GetSection("OnEnter");
        private string GetOnLeave() => GetSection("OnLeave");
        private string GetMainCode() => GetSection("MainCode");
        private string GetGlobalCode() => GetSection("GlobalCode");

        private string GetSection(string name)
        {
            var reader = new StringReader(Contents);
            var builder = new StringBuilder();
            var reading = false;
            var finished = false;


            while(!finished)
            {
                var line = reader.ReadLine();

                if (line == $"// @{name}")
                {
                    reading = true;
                }
                else if (line == $"// @{name}End" || line == null)
                {
                    finished = true;
                }
                else if (reading)
                {
                    builder.AppendLine(line);
                }
            }

            return builder.ToString().Trim();
        }

    }
}
