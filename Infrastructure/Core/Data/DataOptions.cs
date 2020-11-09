namespace Infrastructure.Core.Data
{
    public class DataOptions
    {
        public string[] MappingAssemblies { get; set; }
        public string ConnectionStringName { get; set; }
        public string SchemaFileName { get; set; }
        public bool DoUpdate { get; set; }
        public bool SaveToFile { get; set; }
        public PersistenceConfigurerType PersistenceConfigurerType { get; set; }
    }
}
