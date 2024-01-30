namespace CronParser.Engine.Parsing
{
    public interface ICronElementParser
    {
        public string ParseElement(string input, int minValue, int maxValue);
    }
}