using System.Collections.Generic;

namespace Log4Net.KinesisAppender.Kinesis
{
    public interface IKinesisClient
    {
        void Send(Dictionary<string, object> logEvent);
    }
}