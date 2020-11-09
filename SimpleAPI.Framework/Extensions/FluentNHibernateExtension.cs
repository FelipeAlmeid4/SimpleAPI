using FluentNHibernate.Mapping;

namespace SimpleAPI.Framework.Extensions
{
    public static class FluentNHibernateExtension
    {
        public static PropertyPart Indexable(this PropertyPart propertyPart)
        {
            return propertyPart.Index("___Indexable");
        }
    }
}
