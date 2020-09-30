using System;

namespace GhostSharp.Tests.TestHelpers
{
    public static class TestHelpers
    {
        public static DateTime RemoveTicks(this DateTime dateTime)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerSecond));
        }
    }
}
