using System;

namespace ChatterBox.Core.Infrastructure
{
    public static class DateTimeHelper
    {
        private static Func<DateTimeOffset> GetDateTimeOffSetUtcNow = () => DateTimeOffset.UtcNow;

        public static void SetDateTimeOffSetUtcNow(Func<DateTimeOffset> dateTimeOffSetFunc)
        {
            GetDateTimeOffSetUtcNow = dateTimeOffSetFunc;
        }

        public static DateTimeOffset UtcNow
        {
            get { return GetDateTimeOffSetUtcNow(); }
        }
    }
}