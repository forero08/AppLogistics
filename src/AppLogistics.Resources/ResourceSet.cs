using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AppLogistics.Resources
{
    public class ResourceSet
    {
        private ConcurrentDictionary<string, ConcurrentDictionary<string, ResourceDictionary>> Source { get; }

        public ResourceSet()
        {
            Source = new ConcurrentDictionary<string, ConcurrentDictionary<string, ResourceDictionary>>();
        }

        public string this[string language, string group, string key]
        {
            get
            {
                if (!Source.ContainsKey(language))
                {
                    return null;
                }

                if (!Source[language].ContainsKey(group))
                {
                    return null;
                }

                return Source[language][group].TryGetValue(key, out string title) ? title : null;
            }
            set
            {
                if (!Source.ContainsKey(language))
                {
                    Source[language] = new ConcurrentDictionary<string, ResourceDictionary>();
                }

                if (!Source[language].ContainsKey(group))
                {
                    Source[language][group] = new ResourceDictionary();
                }

                Source[language][group][key] = value;
            }
        }

        public void Override(string language, string source)
        {
            Dictionary<string, ResourceDictionary> resources = JsonConvert.DeserializeObject<Dictionary<string, ResourceDictionary>>(source);

            foreach (string group in resources.Keys)
            {
                foreach (string key in resources[group].Keys)
                {
                    this[language, group, key] = resources[group][key];
                }
            }
        }

        public void Inherit(ResourceSet resources)
        {
            foreach (string language in resources.Source.Keys)
            {
                foreach (string group in resources.Source[language].Keys)
                {
                    foreach (string key in resources.Source[language][group].Keys)
                    {
                        if (this[language, group, key] == null)
                        {
                            this[language, group, key] = resources.Source[language][group][key];
                        }
                    }
                }
            }
        }
    }
}
