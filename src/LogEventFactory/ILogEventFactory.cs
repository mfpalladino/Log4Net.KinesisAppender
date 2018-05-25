using System.Collections.Generic;
using log4net.Core;

namespace Log4Net.KinesisAppender.LogEventFactory
{
    public interface ILogEventFactory
    {
        void Configure(ILogEventFactoryParams factoryParams);

        Dictionary<string, object> CreateLogEvent(LoggingEvent loggingEvent, string appName);
    }
}