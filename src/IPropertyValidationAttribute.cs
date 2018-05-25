namespace Log4Net.KinesisAppender
{
    public interface IPropertyValidationAttribute
    {
        bool IsValid<T>(T value);
    }
}