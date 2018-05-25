using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Log4Net.KinesisAppender.Exceptions;
using Log4Net.KinesisAppender.Filters;
using Log4Net.KinesisAppender.Kinesis;

namespace Log4Net.KinesisAppender
{
    public class KinesisAppenderFilters : IKinesisAppenderFilter
    {
        private readonly List<IKinesisAppenderFilter> _filters = new List<IKinesisAppenderFilter>();

        public void PrepareConfiguration(IKinesisClient client)
        {
            foreach (var filter in _filters)
            {
                ValidateFilterProperties(filter);
                filter.PrepareConfiguration(client);
            }
        }

        public void PrepareEvent(Dictionary<string, object> logEvent)
        {
            foreach (var filter in _filters)
            {
                filter.PrepareEvent(logEvent);
            }
        }

        public void AddFilter(IKinesisAppenderFilter filter)
        {
            _filters.Add(filter);
        }
        
        public static void ValidateFilterProperties(IKinesisAppenderFilter filter)
        {
            var invalidProperties =
                filter.GetType().GetProperties()
                    .Where(prop => !IsValidProperty(prop, filter))
                    .Select(p => p.Name).ToList();

            if (invalidProperties.Any())
            {
                var properties = string.Join(",", invalidProperties.ToArray());
                throw new InvalidFilterConfigurationException(
                    string.Format("The properties ({0}) of {1} are invalid.", properties, filter.GetType().Name));
            }
        }

        private static bool IsValidProperty(PropertyInfo prop, IKinesisAppenderFilter filter)
        {
            if (!(prop.GetCustomAttributes(typeof (IPropertyValidationAttribute), true).FirstOrDefault() is IPropertyValidationAttribute validation))
            {
                return true;
            }

            return validation.IsValid(prop.GetValue(filter, null));
        }

        public void AddAdd(AddValueFilter filter)
        {
            AddFilter(filter);
        }

        public void AddRemove(RemoveKeyFilter filter)
        {
            AddFilter(filter);
        }

        public void AddRename(RenameKeyFilter filter)
        {
            AddFilter(filter);
        }

        public void AddConvertToArray(ConvertToArrayFilter filter)
        {
            AddFilter(filter);
        }
    }
}