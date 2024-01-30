using System.Text;

namespace CronParser.Engine.Formatting
{
    public class CronRowFormatter : ICronRowFormatter
    {
        /// <summary>
        /// Formats cron results in the format specified in the assignment
        /// </summary>
        /// <param name="col1">The first column, which will be the field name</param>
        /// <param name="col2">The second column, which will be the cron expression string</param>
        /// <param name="col1Width">The desired width of the first column</param>
        /// <returns>A formatted string in the format specified in the assignment</returns>
        public string Format(string col1, string col2, int col1Width = 14)
        {
            var resultStringBuilder = new StringBuilder();

            // Assume col1 length is not greater than the provided width
            // It's a little outside the scope of this task to be worrying about formatting exceptions
            resultStringBuilder.Append(col1.PadRight(col1Width));
            resultStringBuilder.Append(col2);

            return resultStringBuilder.ToString();
        }
    }
}