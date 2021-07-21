using Autofac;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Parsers;
using ShitLeopard.DataLoader.Shows;

namespace ShitLeopard.DataLoader
{
    public static class AutoFacRegistrationModule
    {
        public static void Build(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<XDocParser>().As<ISeasonParser>().SingleInstance().Named<ISeasonParser>("xdoc");
            containerBuilder.RegisterType<HtmlAgilityPackParser>().As<ISeasonParser>().SingleInstance().Named<ISeasonParser>("html");
            containerBuilder.RegisterType<BulkDataImporter>().As<IShowBulkDataImporter>().InstancePerDependency();
            containerBuilder.RegisterType<EastboundAndDownLoader>().As<IShowBulkDataLoader>().SingleInstance().Named<IShowBulkDataLoader>("eastboundanddown");
            containerBuilder.RegisterType<TrailerParkBoysLoader>().As<IShowBulkDataLoader>().SingleInstance().Named<IShowBulkDataLoader>("trailerparkboys");
            containerBuilder.RegisterType<WokiScraper>().As<IWikiScraper>().InstancePerDependency();
            containerBuilder.RegisterType<SeasonParserFactory>().As<ISeasonParserFactory>().SingleInstance();


        }
    }
}