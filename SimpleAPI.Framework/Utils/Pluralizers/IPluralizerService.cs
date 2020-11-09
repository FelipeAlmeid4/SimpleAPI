using Pluralize.NET;

namespace SimpleAPI.Framework.Utils.Pluralizers
{
    public interface IPluralizerService
    {
        public IPluralize Pluralizer { get; }
        string Singularize(string word);
        string Pluralize(string word);
        bool IsPlural(string word);
        void ConfigureUnpluralizables(string[] unpluralizables);
    }
}
