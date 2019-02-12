namespace Core.Logger.Config
{
    public class LoggerConfig
    {
        /// <summary>
        /// Full path of directory where will be written the log files.
        /// </summary>
        public string LogsPath { get; set; }
        /// <summary>
        /// Maximum size, in bytes, of the log file.
        /// </summary>
        public int? MaxFileSize { get; set; }
    }
}
