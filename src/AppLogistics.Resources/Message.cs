namespace AppLogistics.Resources
{
    public static class Message
    {
        public static string For<TView>(string key, params object[] args)
        {
            string message = Resource.Localized(typeof(TView).Name, "Messages", key);

            return message == null || args.Length == 0 ? message : string.Format(message, args);
        }
    }
}
