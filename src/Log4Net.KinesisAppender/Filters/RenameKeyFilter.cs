using System.Collections.Generic;
using Log4Net.KinesisAppender.Extensions;
using Log4Net.KinesisAppender.Kinesis;
using Log4Net.KinesisAppender.SmartFormatters;

namespace Log4Net.KinesisAppender.Filters
{
    public class RenameKeyFilter : IKinesisAppenderFilter
    {
        private LogEventSmartFormatter _key;
        private LogEventSmartFormatter _renameTo;
        private const string FailedToRename = "RenameFailed";

        public bool Overwrite { get; set; }

        [PropertyNotEmpty]
        public string Key
        {
            get => _key;
            set => _key = value;
        }

        [PropertyNotEmpty]
        public string RenameTo
        {
            get => _renameTo;
            set => _renameTo = value;
        }

        public RenameKeyFilter()
        {
            Overwrite = true;
        }

        public void PrepareConfiguration(IKinesisClient client)
        {
        }

        public void PrepareEvent(Dictionary<string, object> logEvent)
        {
            var key = _key.Format(logEvent);
            if (logEvent.TryGetValue(key, out var token))
            {
                logEvent.Remove(key);

                var newName = _renameTo.Format(logEvent);

                if (!Overwrite && logEvent.ContainsKey(newName))
                {
                    logEvent.AddTag(FailedToRename);
                    return;
                }

                logEvent[newName] = token;
            }
        }
    }
}