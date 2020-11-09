using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Options;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using SimpleAPI.Framework.Extensions;
using System;
using System.IO;
using System.Reflection;

namespace Infrastructure.Core.Data
{
    public class FluentSessionFactoryBuilder : ISessionFactoryBuilder
    {
        private readonly DataOptions options;

        public FluentSessionFactoryBuilder(IOptions<DataOptions> options)
        {
            this.options = options.Value;
        }

        public ISessionFactory BuildSessionFactory()
        {
            var fluentConfig = Fluently.Configure(new Configuration());

            fluentConfig
                .Database(GetPersistenceConfigurer())
                .Mappings(mappings =>
                {
                    foreach (var assemblyName in options.MappingAssemblies)
                    {
                        mappings.FluentMappings.AddFromAssembly(Assembly.Load(assemblyName));
                    }
                }).ExposeConfiguration(c =>
                {
                    SchemaUpdate(c);
                });

            return fluentConfig
              .BuildConfiguration()
              .BuildSessionFactory();
        }

        public IPersistenceConfigurer GetPersistenceConfigurer()
        {
            switch (options.PersistenceConfigurerType)
            {
                case PersistenceConfigurerType.PostgreSQL82:
                    return PostgreSQLConfiguration.PostgreSQL82.ConnectionString(options.ConnectionStringName);
                default:
                    throw new Exception("Bank configuration not implemented");
            }
        }

        protected virtual void SchemaUpdate(Configuration config)
        {
            if (!(options.DoUpdate || options.SaveToFile) || options.SchemaFileName.IsNullOrEmpty())
            {
                return;
            }

            if (options.SaveToFile)
            {
                var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.SchemaFileName);

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                new SchemaUpdate(config).Execute((script) => GenerateSchemaUpdateScriptFile(fileName, script), options.DoUpdate);
            }
            else
            {
                new SchemaUpdate(config).Execute(false, options.DoUpdate);
            }
        }

        protected virtual void GenerateSchemaUpdateScriptFile(string fileName, string script)
        {
            using (var file = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            {
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine(script.Trim() + ";");
                    writer.Close();
                }
            }
        }

    }
}
