using Pluralize.NET;
using System;
using System.Linq;

namespace SimpleAPI.Framework.Utils.Pluralizers
{
    public class PluralizerService : IPluralizerService
    {
        private IPluralize pluralizer { get; set; }

        public IPluralize Pluralizer
        {
            get
            {
                if (pluralizer == null)
                {
                    pluralizer = new Pluralizer();
                }

                return pluralizer;
            }
        }

        public string Pluralize(string word)
        {
            return Pluralizer.Pluralize(word);
        }

        public string Singularize(string word)
        {
            return Pluralizer.Singularize(word);
        }

        public bool IsPlural(string word)
        {
            return Pluralizer.IsPlural(word);
        }

        public void ConfigureUnpluralizables(string[] unpluralizables)
        {
            foreach (var item in unpluralizables)
            {
                Pluralizer.AddIrregularRule(item, item);
            }
        }
    }
}
