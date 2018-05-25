using System.Collections.Generic;
using Log4Net.KinesisAppender.Kinesis;
using Log4Net.KinesisAppender.SmartFormatters;

namespace Log4Net.KinesisAppender.Filters
{
    public class RemoveKeyFilter : IKinesisAppenderFilter
    {
        private LogEventSmartFormatter _key;

        [PropertyNotEmpty]
        public string Key
        {
            get => _key;
            set => _key = value;
        }

        public void PrepareConfiguration(IKinesisClient client)
        {
        }

        public void PrepareEvent(Dictionary<string, object> logEvent)
        {
            logEvent.Remove(_key.Format(logEvent));
        }
    }
}