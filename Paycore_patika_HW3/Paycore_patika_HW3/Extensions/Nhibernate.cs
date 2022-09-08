
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace Paycore_patika_HW3.Extensions
{
    public static class Nhibernate
    {
        public static IServiceCollection Add(this IServiceCollection services, string connStr)
        {

            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(Nhibernate).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<PostgreSQLDialect>();
                c.ConnectionString = connStr;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Validate;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });
            configuration.AddMapping(domainMapping);

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;

        }
    }
}