using System;

namespace Log4Net.KinesisAppender
{
    public class PropertyNotEmptyAttribute : Attribute, IPropertyValidationAttribute
    {
        public bool IsValid<T>(T value)
        {
            return InnerIsValid(value as string);
        }

        private bool InnerIsValid(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}