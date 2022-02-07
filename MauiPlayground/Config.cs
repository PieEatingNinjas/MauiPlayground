namespace MauiPlayground
{
    internal static class Config
    {
        public static string BaseUrl
        {
            get =>
#if TEST
                    "http://www.google.com";
#elif PRD
                    "http://www.google.be";
#else
                    "http://www.microsoft.com";
#endif
        }
    }
}
