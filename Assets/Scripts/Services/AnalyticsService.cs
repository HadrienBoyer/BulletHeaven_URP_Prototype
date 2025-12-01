namespace TappyTale
{
    public static class AnalyticsService
    {
        public static void Track(string eventName, params (string key, object value)[] parameters)
        {
            // Hook your analytics provider here (UGS, Firebase, etc.)
        }
    }
}
