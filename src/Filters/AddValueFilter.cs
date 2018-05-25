using System.Collections.Generic;
using Log4Net.KinesisAppender.Extensions;
using Log4Net.KinesisAppender.Kinesis;
using Log4Net.KinesisAppender.SmartFormatters;

namespace Log4Net.KinesisAppender.Filters
{
    public class AddValueFilter : IKinesisAppenderFilter
    {
        private LogEventSmartFormatter _key;
        private LogEventSmartFormatter _value;

        [PropertyNotEmpty]
        public string Key
        {
            get => _key;
            set => _key = value;
        }

        [PropertyNotEmpty]
        public string Value
        {
            get => _value;
            set => _value = value;
        }

        public bool Overwrite { get; set; }

        public bool EventShouldBePrepared()
        {
            return true;
        }

        public void PrepareConfiguration(IKinesisClient client)
        {
        }

        public void PrepareEvent(Dictionary<string, object> logEvent)
        {
            var key = _key.Format(logEvent);
            var value = _value.Format(logEvent);

            if (Overwrite)
            {
                logEvent[key] = value;
            }
            else
            {
                logEvent.AddOrSet(key, value);
            }
        }
    }
}