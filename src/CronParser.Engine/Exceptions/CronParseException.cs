using System;

namespace CronParser.Engine.Exceptions
{
    public class CronParseException : Exception
    {
        public CronParseException(string message) : base(message)
        {
        }
    }
}