using System;
using System.Collections.Generic;
using System.Linq;
using CronParser.Engine.Exceptions;

namespace CronParser.Engine.Parsing.SubParsers
{
    public class ListParser : IListParser
    {
        /// <summary>
        /// Given an input cron string, will try to extract a list from it. E.g 1, 2, 4, 8 returns a list of length 4, containing these 4 integers
        /// </summary>
        /// <param name="input">The input cron string</param>
        /// <param name="minValue">The minimum allowed value for parsed results</param>
        /// <param name="maxValue">The max allowed value for parsed results</param>
        /// <param name="listElements">The extracted list</param>
        /// <returns>True if a list was present</returns>
        /// <exception cref="CronParseException">Thrown if we ran into any issues whilst parsing</exception>
        public bool TryGetList(string input, int minValue, int maxValue, out IList<int> listElements)
        {
            // We only care about everything before the step character (if there even is one)
            var steplessInput = input.Split('/')[0];

            var splitInput = steplessInput.Split(',');
            var resultList = new List<int>(splitInput.Length);

            // Return false immediately if no list is present
            if (splitInput.Length == 1)
            {
                listElements = Array.Empty<int>();
                return false;
            }

            // List is present, iterate over each element and attempt to parse
            foreach (var elementString in splitInput)
            {
                if (!int.TryParse(elementString, out var parsedValue))
                {
                    throw new CronParseException($"Failed to parse list element: {elementString}");
                }

                if (parsedValue < minValue)
                {
                    throw new CronParseException($"Value of list element ({parsedValue}) cannot be below min allowed value of {minValue}");
                }

                if (parsedValue > maxValue)
                {
                    throw new CronParseException(
                        $"Value of list element ({parsedValue}) exceeds max allowed value of {maxValue}");
                }

                resultList.Add(parsedValue);
            }

            // Remove duplicates and sort the list elements before returning
            resultList.Sort();
            listElements = resultList.Distinct().ToList();
            return true;
        }
    }
}