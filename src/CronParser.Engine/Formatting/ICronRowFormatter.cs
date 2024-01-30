namespace CronParser.Engine.Formatting
{
    public interface ICronRowFormatter
    {
        public string Format(string col1, string col2, int col1Width = 14);
    }
}