using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Log4Net.KinesisAppender.SmartFormatters
{
    public class LogEventSmartFormatter : SmartFormatter
    {
        private static readonly Regex InnerRegex = new Regex(@"%\{([^\}]+)\}", RegexOptions.Compiled);
        

        public LogEventSmartFormatter(string input) 
            : base(input, InnerRegex.Matches(input))
        {

        }

        protected override bool TryProcessMatch(Dictionary<string, object> logEvent, Match match, out string replacementString)
        {
            replacementString = string.Empty;
            var innerMatch = match.Groups[1].Value;

            // "+" means dateTime format
            if (innerMatch.StartsWith("+"))
            {
                replacementString = DateTime.Now.ToString(innerMatch.Substring(1), CultureInfo.InvariantCulture);
                return true;
            }

            if (logEvent.TryGetValue(innerMatch, out var token))
            {
                replacementString = token.ToString();
                return true;
            }

            return false;
        }

        public static implicit operator LogEventSmartFormatter(string s)
        {
            return new LogEventSmartFormatter(s);
        }
    }
}