using System;
using System.Text;
using Core.Logger.Config;
using Core.Logger.Enums;
using Core.Logger.Interfaces;
using Newtonsoft.Json;

namespace Core.Logger.Models
{
    public class LogMessage<T>: ILogMessage
    {
        public LogMessage(T message, LogLevel level = LogLevel.INFO)
        {
            Message = message;
            LogLevel = level;
        }

        public T Message { get; }
        public LogLevel LogLevel { get; private set; }
        public bool IsString => Message is string;
        public bool IsSerializable => Message.GetType().IsSerializable;

        public string GetMessage()
        {
            StringBuilder msg = new StringBuilder();
            // Level
            msg.Append(GetLogLevel(LogLevel));
            // Log time
            LogTime time = new LogDateTime();
            msg.Append(" [")
                .Append(time.GetLogTime())
                .Append("]");
            // Message
            msg.Append(": ");
            msg.Append(GetStyledMessage(Message));
            return msg.ToString();
        }

        private string GetLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.DEBUG: return "debug";
                case LogLevel.INFO: return "info";
                case LogLevel.WARNING: return "warning";
                case LogLevel.ERROR: return "error";
                case LogLevel.EXCEPTION: return "exception";
                default: return null;
            }
        }

        private string GetStyledMessage(T msg)
        {
            if (IsString) return msg as string;
            try
            {
                string styledMessage = Serialize(msg);
                return styledMessage;
            }
            catch (Exception e)
            {
                if (!IsSerializable) return null;
                LogLevel = LogLevel.EXCEPTION;
                return e.Message;
            }
        }

        private string Serialize(T msg)
        {
            return JsonConvert.SerializeObject(
                msg,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
