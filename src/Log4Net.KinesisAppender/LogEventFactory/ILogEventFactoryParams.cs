using log4net.Core;

namespace Log4Net.KinesisAppender.LogEventFactory
{
    public interface ILogEventFactoryParams
    {
        FixFlags FixedFields { get; set; }
        bool SerializeObjects { get; set; }
    }
}