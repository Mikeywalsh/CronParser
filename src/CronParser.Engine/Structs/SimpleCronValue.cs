using CronParser.Engine.Enums;

namespace CronParser.Engine.Structs
{
    public readonly struct SimpleCronValue
    {
        public SimpleCronValueType CronValueType { get; }
        public int Value { get; }

        public SimpleCronValue(SimpleCronValueType cronValueType, int value = -1)
        {
            CronValueType = cronValueType;
            Value = value;
        }
    }
}