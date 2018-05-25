using System;
using System.Collections.Generic;
using log4net.Util;
using log4net.Appender;
using log4net.Core;
using Log4Net.KinesisAppender.Kinesis;
using Log4Net.KinesisAppender.LogEventFactory;

namespace Log4Net.KinesisAppender
{
    public class KinesisAppender : AppenderSkeleton, ILogEventFactoryParams
    {
        private IKinesisClient _client;

        public string Region { get; set; }
        public string StreamName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string AppName { get; set; }
        public KinesisAppenderFilters Filters { get; set; }
        public FixFlags FixedFields { get; set; }
        public bool SerializeObjects { get; set; }
        public ILogEventFactory LogEventFactory { get; set; }

        public KinesisAppender()
        {
            LogEventFactory = new BasicLogEventFactory();
            Filters = new KinesisAppenderFilters();
        }

        public override void ActivateOptions()
        {
            _client = KinesisClientSingleton.GetKinesisClient(Region, StreamName,  AccessKey, SecretKey, AppName);
            FixedFields = FixFlags.Partial;
            LogEventFactory.Configure(this);
            Filters.PrepareConfiguration(_client);
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_client == null || loggingEvent == null)
                return;

            var logEvent = LogEventFactory.CreateLogEvent(loggingEvent, AppName);

            PrepareEvent(logEvent);

            SafeSendToKinesis(logEvent);
        }

        private void PrepareEvent(Dictionary<string, object> logEvent)
        {
            Filters.PrepareEvent(logEvent);
        }

        private void SafeSendToKinesis(Dictionary<string, object> logEvent)
        {
            try
            {
                _client.Send(logEvent);
            }
            catch (Exception ex)
            {
                LogLog.Error(GetType(), "KinesisClient inner exception occurred", ex);
            }
        }
    }
}
