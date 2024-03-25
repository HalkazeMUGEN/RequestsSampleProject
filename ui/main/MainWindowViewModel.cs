using Prism.Commands;
using Prism.Mvvm;
using RequestsSampleProject.data.Script;
using RequestsSampleProject.data.Script.model;
using RequestsSampleProject.ui.sort;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace RequestsSampleProject.ui.main
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IScriptRepository _scriptRepository;

        private string _title = "NASM_GUI-Reborn モドキ";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<Script> _scripts;
        public ObservableCollection<Script> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        public DelegateCommand SortCommand { get; }

        public MainWindowViewModel() : this(new ScriptRepository()) { }
        public MainWindowViewModel(IScriptRepository scriptRepository)
        {
            _scriptRepository = scriptRepository;
            _scripts = new ObservableCollection<Script>([.. _scriptRepository.GetAll().OrderBy(x => x.Order)]);

            SortCommand = new DelegateCommand(Sort);
        }

        private void Sort()
        {
            var window = new SortScriptsWindow();
            window.ShowDialog();

            Scripts = new ObservableCollection<Script>([.. _scriptRepository.GetAll().OrderBy(x => x.Order)]);
        }
    }
}
