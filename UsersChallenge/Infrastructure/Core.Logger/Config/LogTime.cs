using System;

namespace Core.Logger.Config
{
    internal abstract class LogTime
    {
        protected DateTime Time => DateTime.Now;
        internal abstract string GetLogTime();
    }

    internal class LogTimeOnly : LogTime
    {
        internal override string GetLogTime()
        {
            return Time.ToString("HH:mm:ss");
        }
    }

    internal class LogDateTime : LogTime
    {
        internal override string GetLogTime()
        {
            return Time.ToString("dd-MM-yyyy HH:mm:ss");
        }
    }
}
