using Microsoft.EntityFrameworkCore;
using RequestsSampleProject.data.Script.database;
using RequestsSampleProject.data.Script.io;
using System.Collections.ObjectModel;
using System.Linq;

namespace RequestsSampleProject.data.Script
{
    public class ScriptRepository : IScriptRepository
    {
        private static readonly model.Script[] _DEFAULT_SCRIPTS = [
            new model.Script()
            {
                Order = 1,
                PackageName = "SampleA",
                DisplayName = "Sample-A",   // ここではわざと「script.ini」での定義と変えることにより、DBでの設定が優先されることを示しています
                FileName = "SampleA.csx"
            },
            new model.Script()
            {
                Order = 0,
                PackageName = "SampleB",
                DisplayName = "Sample-B",   // 同上
                FileName = "SampleB.csx"
            },
            new model.Script()
            {
                Order = 2,
                PackageName = "SampleC",
                DisplayName = "Sample-C",   // 同上
                FileName = "SampleC.csx"
            }
        ];

        private readonly ScriptContext _context;

        public ScriptRepository()
        {
            _context = new ScriptContext();
            _context.Database.EnsureCreated();
            _context.Scripts.Load();

            if (_context.Scripts.Local.Count == 0)
            {
                InitDB();
            }

            LoadScriptsDir();
        }

        ~ScriptRepository()
        {
            _context.Dispose();
        }

        public ObservableCollection<model.Script> GetAll()
        {
            return _context.Scripts.Local.ToObservableCollection();
        }

        public void UpdateAll()
        {
            _context.SaveChanges();
        }

        private void InitDB()
        {
            _context.Scripts.AddRange(_DEFAULT_SCRIPTS);
            _context.SaveChanges();
        }

        private void LoadScriptsDir()
        {
            int orderMax = _context.Scripts.Local.Max(x => x.Order);

            ScriptLoader loader = new();
            foreach (var script in loader.LoadDir())
            {
                var dbData = _context.Scripts.Local.FirstOrDefault(x => x.PackageName == script.PackageName);
                if (dbData == null)
                {
                    script.ScriptId = 0;
                    script.Order = ++orderMax;
                    _context.Scripts.Add(script);
                } else
                {
                    dbData.DisplayName = script.DisplayName;
                    dbData.FileName = script.FileName;
                }
            }

            _context.SaveChanges();
        }
    }
}
