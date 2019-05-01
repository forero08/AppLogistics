using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AppLogistics.Resources
{
    public static class Resource
    {
        private static ConcurrentDictionary<string, ResourceSet> Resources { get; }

        static Resource()
        {
            Resources = new ConcurrentDictionary<string, ResourceSet>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Resources";

            foreach (string resource in Directory.GetFiles(path, "*.json", SearchOption.AllDirectories))
            {
                string type = Path.GetFileNameWithoutExtension(resource);
                string language = Path.GetExtension(type).TrimStart('.');
                type = Path.GetFileNameWithoutExtension(type);

                Set(type).Override(language, File.ReadAllText(resource));
            }
        }

        public static ResourceSet Set(string type)
        {
            if (!Resources.ContainsKey(type))
            {
                Resources[type] = new ResourceSet();
            }

            return Resources[type];
        }

        public static string ForAction(string name)
        {
            return Localized("Shared", "Actions", name);
        }

        public static string ForLookup(string type)
        {
            return Localized("Lookup", "Titles", type);
        }

        public static string ForString(string value)
        {
            return Localized("Shared", "Strings", value);
        }

        public static string ForHeader(string model)
        {
            return Localized("Page", "Headers", model);
        }

        public static string ForPage(string path)
        {
            return Localized("Page", "Titles", path);
        }

        public static string ForPage(IDictionary<string, object> path)
        {
            string area = path["area"] as string;
            string action = path["action"] as string;
            string controller = path["controller"] as string;

            return ForPage(area + controller + action);
        }

        public static string ForSiteMap(string area, string controller, string action)
        {
            return Localized("SiteMap", "Titles", area + controller + action);
        }

        public static string ForPermission(string area)
        {
            return Localized("Permission", "Areas", area ?? "");
        }

        public static string ForPermission(string area, string controller)
        {
            return Localized("Permission", "Controllers", area + controller);
        }

        public static string ForPermission(string area, string controller, string action)
        {
            return Localized("Permission", "Actions", area + controller + action);
        }

        public static string ForProperty<TView, TProperty>(Expression<Func<TView, TProperty>> expression)
        {
            return ForProperty(expression.Body);
        }

        public static string ForProperty(string view, string name)
        {
            if (Localized(view, "Titles", name) is string title)
            {
                return title;
            }

            string[] properties = SplitCamelCase(name);
            string language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            for (int skipped = 0; skipped < properties.Length; skipped++)
            {
                for (int viewSize = 1; viewSize < properties.Length - skipped; viewSize++)
                {
                    string relation = string.Concat(properties.Skip(skipped).Take(viewSize)) + "View";
                    string property = string.Concat(properties.Skip(viewSize + skipped));

                    if (Localized(relation, "Titles", property) is string relationTitle)
                    {
                        return Set(view)[language, "Titles", name] = relationTitle;
                    }
                }
            }

            return null;
        }

        public static string ForProperty(Type view, string name)
        {
            return ForProperty(view.Name, name ?? "");
        }

        public static string ForProperty(Expression expression)
        {
            return expression is MemberExpression member ? ForProperty(member.Expression.Type, member.Member.Name) : null;
        }

        internal static string Localized(string type, string group, string key)
        {
            ResourceSet resources = Set(type);
            string language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            return resources[language, group, key] ?? resources["", group, key];
        }

        private static string[] SplitCamelCase(string value)
        {
            return Regex.Split(value, "(?<!^)(?=[A-Z])");
        }
    }
}
