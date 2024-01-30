using System.Collections.Generic;

namespace CronParser.Engine.Parsing.SubParsers
{
    public interface IListParser
    {
        bool TryGetList(string input, int minValue, int maxValue, out IList<int> listElements);
    }
}