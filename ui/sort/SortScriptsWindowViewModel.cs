using Prism.Commands;
using Prism.Mvvm;
using RequestsSampleProject.data.Script;
using RequestsSampleProject.data.Script.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace RequestsSampleProject.ui.sort
{
    public class SortScriptsWindowViewModel : BindableBase
    {
        private readonly IScriptRepository _scriptRepository;

        private string _title = "順序変更";
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

        public DelegateCommand ClosedCommand { get; }

        public SortScriptsWindowViewModel() : this(new ScriptRepository()) { }
        public SortScriptsWindowViewModel(IScriptRepository scriptRepository)
        {
            _scriptRepository = scriptRepository;
            _scripts = new ObservableCollection<Script>([.. _scriptRepository.GetAll().OrderBy(x => x.Order)]);

            ClosedCommand = new DelegateCommand(Closed);
        }

        private void Closed()
        {
            for (var i = 0; i < _scripts.Count; ++i)
            {
                _scripts[i].Order = i;
            }
            _scriptRepository.UpdateAll();
        }
    }
}
