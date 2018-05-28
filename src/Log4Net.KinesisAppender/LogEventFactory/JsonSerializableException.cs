using System;

namespace Log4Net.KinesisAppender.LogEventFactory
{
    public class JsonSerializableException
    {
        public int HResult { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string HelpLink { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
        public System.Collections.IDictionary Data { get; set; }
        public JsonSerializableException InnerException { get; set; }

        public static JsonSerializableException Create(Exception exception)
        {
            if (exception == null)
                return null;

            var serializable = new JsonSerializableException
            {
                Type = exception.GetType().FullName,
                Message = exception.Message,
                HelpLink = exception.HelpLink,
                Source = exception.Source,
                StackTrace = exception.StackTrace,
                TargetSite = exception.TargetSite != null ? exception.TargetSite.ToString() : null,
                Data = exception.Data
            };

            if (exception.InnerException != null)
            {
                serializable.InnerException = Create(exception.InnerException);
            }
            return serializable;
        }
    }
}