namespace CronParser.Engine.Structs
{
    public readonly struct CronRange
    {
        public int Start { get; }
        public int End { get; }

        public CronRange(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}