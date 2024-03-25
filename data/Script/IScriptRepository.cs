using System.Collections.ObjectModel;

namespace RequestsSampleProject.data.Script
{
    public interface IScriptRepository
    {
        public ObservableCollection<model.Script> GetAll();
        public void UpdateAll();
    }
}
