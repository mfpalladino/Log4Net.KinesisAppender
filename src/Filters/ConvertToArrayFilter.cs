using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Log4Net.KinesisAppender.Extensions;
using Log4Net.KinesisAppender.Kinesis;
using Log4Net.KinesisAppender.SmartFormatters;

namespace Log4Net.KinesisAppender.Filters
{
    public class ConvertToArrayFilter : IKinesisAppenderFilter
    {
        private Regex _seperateRegex;
        private LogEventSmartFormatter _sourceKey;

        [PropertyNotEmpty]
        public string SourceKey
        {
            get => _sourceKey;
            set => _sourceKey = value;
        }

        [PropertyNotEmpty]
        public string Seperators
        {
            get => _seperateRegex != null ? _seperateRegex.ToString() : string.Empty;
            set => _seperateRegex = new Regex("[" + value + "]+", RegexOptions.Compiled | RegexOptions.Multiline);
        }

        public ConvertToArrayFilter()
        {
            SourceKey = "Message";
            Seperators = ", ";
        }

        public bool EventShouldBePrepared()
        {
            return true;
        }

        public void PrepareConfiguration(IKinesisClient client)
        {
        }

        public void PrepareEvent(Dictionary<string, object> logEvent)
        {
            var formattedKey = _sourceKey.Format(logEvent);
            if (!logEvent.TryGetStringValue(formattedKey, out var value))
            {
                return;
            }

            logEvent[formattedKey] = ValueToArray(value);
        }

        private List<string> ValueToArray(string value)
        {
            return _seperateRegex.Split(value).Where(s => !string.IsNullOrEmpty(s)).ToList();
        }

        public object ValueToArrayObject(object value)
        {
            return ValueToArray((string)value);
        }
    }
}
