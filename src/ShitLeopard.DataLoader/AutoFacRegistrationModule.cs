using Autofac;
using ShitLeopard.DataLoader.Configuration;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Metadata;
using ShitLeopard.DataLoader.Parsers;
using ShitLeopard.DataLoader.Shows;

namespace ShitLeopard.DataLoader
{
    public static class AutoFacRegistrationModule
    {
        public static void Build(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<XDocParser>().As<ISeasonParser>().SingleInstance().Named<ISeasonParser>(Constants.DependencyNames.CCParserSMPTE);
            containerBuilder.RegisterType<BulkDataImporter>().As<IShowBulkDataImporter>().InstancePerDependency();
            containerBuilder.RegisterType<SeasonParserFactory>().As<ISeasonParserFactory>().SingleInstance();
            containerBuilder.RegisterType<ConsoleLogger>().As<IConsoleLogger>().SingleInstance();
            containerBuilder.RegisterType<ConnectionString>().SingleInstance();
            containerBuilder.RegisterType<ShowImportService>().As<IShowImportService>().InstancePerDependency();

            containerBuilder.RegisterType<WikipediaMetadataProvider>().As<IMetadataProvider>().InstancePerDependency().Named<IMetadataProvider>(Constants.DependencyNames.Wikipedia);
        }

    }
}