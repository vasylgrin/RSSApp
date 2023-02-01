namespace RSSApp.Service.Extentions
{
    public static class ParseDateTimeExtension
    {
        public static DateTime ParseToDateTime(this string str)
        {
            if (DateTime.TryParse(str, out DateTime dateTime))
            {
                return dateTime;
            }

            throw new InvalidDataException($"{nameof(str)} cannot be parsed, because incorrect input data.");
        }
    }
}
