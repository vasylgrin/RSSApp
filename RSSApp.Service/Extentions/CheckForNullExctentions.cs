namespace RSSApp.Service.Extentions
{
    public static class CheckForNullExctentions
    {
        public static void CheckForNull(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException($"'{nameof(str)}' cannot be null or whitespace.", nameof(str));
            }
        }
        public static void CheckForNull(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"'{nameof(obj)}' cannot be null or whitespace.", nameof(obj));

            }
        }
    }
}
