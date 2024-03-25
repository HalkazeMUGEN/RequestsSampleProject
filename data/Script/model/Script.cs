namespace RequestsSampleProject.data.Script.model
{
    public class Script
    {
        public int ScriptId { get; set; }
        public int Order {  get; set; }
        public string PackageName { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }

        public Script()
        {
            ScriptId = 0;
            Order = 0;
            PackageName = string.Empty;
            DisplayName = string.Empty;
            FileName = string.Empty;
        }

        public bool IsEmpty()
        {
            return (DisplayName == string.Empty && PackageName == string.Empty && FileName == string.Empty);
        }
    }
}
