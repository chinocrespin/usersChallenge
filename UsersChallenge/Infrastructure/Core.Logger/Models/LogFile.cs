namespace Core.Logger.Models
{
    public class LogFile
    {
        public string FileName { get; set; }

        public LogFile(string fileName)
        {
            FileName = fileName;
        }

        public LogFile(string fileName, string append, char separator = '_')
        {
            FileName = fileName + separator + append;
        }

        public LogFile(string fileName, string[] appends, char appendSeparator = '_')
        {
            FileName = fileName + string.Join(appendSeparator, appends);
        }
    }
}
