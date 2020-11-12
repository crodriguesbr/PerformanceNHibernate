using System;
using System.Reflection;
using NHibernate;
using NHibernate.Caches.StackExchangeRedis;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NHPerformance
{
    public class SessionFactoryBuild2Cache
    {
        public SessionFactoryBuild2Cache()
        {
        }

        protected ISessionFactory Build()
        {
            Configuration cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Data Source=localhost,1433;User Id=sa;Password=abc,12345678;Initial Catalog=NHPerformance;Application Name=NHPerformance";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
                x.LogSqlInConsole = false;
            });
            cfg.SetProperty("adonet.batch_size", "10");

            cfg.SetProperty("cache.configuration", "localhost:6379,ssl=False,abortConnect=False");
            cfg.SetProperty("cache.use_query_cache", "true");
            var regionClienteConfig = new RegionConfig("Cliente", TimeSpan.FromHours(1), false, null, null, true);
            NHibernate.Caches.StackExchangeRedis.RedisCacheProvider.SetRegionConfiguration("cliente", regionClienteConfig);
            cfg.Cache(c =>
            {
                c.Provider<NHibernate.Caches.StackExchangeRedis.RedisCacheProvider>();
            });

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);

           // new SchemaUpdate(cfg).Execute(false, true);

            return cfg.BuildSessionFactory();
        }
    }
}
