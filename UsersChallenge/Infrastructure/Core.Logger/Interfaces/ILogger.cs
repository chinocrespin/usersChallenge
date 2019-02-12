using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Logger.Models;

namespace Core.Logger.Interfaces
{
    public interface ILogger
    {
        Task WriteMessageAsync(LogFile file, ILogMessage message);
        Task WriteMessagesAsync(LogFile file, IEnumerable<ILogMessage> messages);
    }
}
