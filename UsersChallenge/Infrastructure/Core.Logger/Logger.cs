using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Logger.Config;
using Core.Logger.Interfaces;
using Core.Logger.Models;
using Microsoft.Extensions.Options;

namespace Core.Logger
{
    public class Logger: ILogger
    {
        private readonly string _path;
        private readonly int? _maxFileSize;
        private const string _ext = ".log";

        public Logger(IOptions<LoggerConfig> config)
        {
            _path = config?.Value.LogsPath;
            _maxFileSize = config?.Value.MaxFileSize;
            
            // Creates the directory of log files (only if exists)
            Directory.CreateDirectory(_path);
        }

        public async Task WriteMessageAsync(LogFile file, ILogMessage message)
        {
            var fullName = GetLogFile(file);
            // Write the log messages to the file
            using (var streamWriter = File.AppendText(fullName))
            {
                await streamWriter.WriteAsync($"{message.GetMessage()}{Environment.NewLine}");
            }
        }

        public async Task WriteMessagesAsync(LogFile file, IEnumerable<ILogMessage> messages)
        {
            foreach (var msg in messages)
            {
                var fullName = GetLogFile(file);

                // Write the log messages to the file
                using (var streamWriter = File.AppendText(fullName))
                {
                    await streamWriter.WriteAsync($"{msg.GetMessage()}{Environment.NewLine}");
                }
            }
        }

        private string GetLogFile(LogFile file)
        {
            string fileName = file.FileName;
            var fullName = Path.Combine(_path, $"{fileName}{_ext}");
            var fileInfo = new FileInfo(fullName);

            fileInfo.Refresh();
            var cont = 0;
            while (_maxFileSize != null && fileInfo.Exists && fileInfo.Length > _maxFileSize)
            {
                cont++;
                fileName += cont.ToString();
                fullName = Path.Combine(_path, $"{fileName}{_ext}");
                fileInfo = new FileInfo(fullName);
                fileInfo.Refresh();
            }
            return fullName;
        }

        
    }
}
