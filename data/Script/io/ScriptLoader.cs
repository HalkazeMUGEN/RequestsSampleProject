using IniParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RequestsSampleProject.data.Script.io
{
    internal class ScriptLoader
    {
        private static readonly string _SCRIPT_DIRNAME = "Scripts";
        private static readonly string _SCRIPT_INI_FILENAME = "script.ini";

        private static readonly string _INI_GROUP_NAME = "ScriptDef";
        private static readonly string _INI_DISPLAYNAME = "DisplayName";
        private static readonly string _INI_FILENAME = "FileName";

        private readonly string _scriptsPath;
        private readonly FileIniDataParser _parser;

        public ScriptLoader()
        {
            _scriptsPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, _SCRIPT_DIRNAME);
            _parser = new FileIniDataParser();
        }

        public List<model.Script> LoadDir()
        {
            if (!Directory.Exists(_scriptsPath))
            {
                return [];
            }

            var scripts = new List<model.Script>();

            foreach (var package in Directory.GetDirectories(_scriptsPath))
            {
                var filepath = Path.Join(package, _SCRIPT_INI_FILENAME);
                if (File.Exists(filepath))
                {
                    try
                    {
                        var data = _parser.ReadFile(filepath, Encoding.UTF8);
                        var displayName = data[_INI_GROUP_NAME][_INI_DISPLAYNAME];
                        var fileName = data[_INI_GROUP_NAME][_INI_FILENAME];

                        model.Script script = new model.Script()
                        {
                            PackageName = Path.GetFileName(package),
                            DisplayName = displayName,
                            FileName = fileName
                        };

                        scripts.Add(script);
                    }
                    catch (Exception) { }
                }
            }

            return scripts;
        }
    }
}
