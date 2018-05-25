using System.Collections.Generic;
using Log4Net.KinesisAppender.Kinesis;

namespace Log4Net.KinesisAppender
{
    public interface IKinesisAppenderFilter
    {
        void PrepareConfiguration(IKinesisClient client);
        void PrepareEvent(Dictionary<string, object> logEvent);
    }
}