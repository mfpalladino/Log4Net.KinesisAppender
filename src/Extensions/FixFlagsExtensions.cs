using log4net.Core;

namespace Log4Net.KinesisAppender.Extensions
{
    public static class FixFlagsExtensions
    {
        public static bool ContainsFlag(this FixFlags flagsEnum, FixFlags flag)
        {
            return (flagsEnum & flag) != 0;
        }
    }
}