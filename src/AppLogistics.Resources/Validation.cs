namespace AppLogistics.Resources
{
    public static class Validation
    {
        public static string For(string key, params object[] args)
        {
            string validation = Resource.Localized("Form", "Validations", key);

            return validation == null || args.Length == 0 ? validation : string.Format(validation, args);
        }

        public static string For<TView>(string key, params object[] args)
        {
            string validation = Resource.Localized(typeof(TView).Name, "Validations", key);

            return validation == null || args.Length == 0 ? validation : string.Format(validation, args);
        }
    }
}
