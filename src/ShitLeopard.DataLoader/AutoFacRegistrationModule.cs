using Autofac;
using ShitLeopard.DataLoader.Contracts;
using ShitLeopard.DataLoader.Parsers;

namespace ShitLeopard.DataLoader
{
    public static class AutoFacRegistrationModule
    {
        public static void Build(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<XDocParser>().As<ISeasonParser>().SingleInstance().Named<ISeasonParser>("xdoc");
            containerBuilder.RegisterType<HtmlAgilityPackParser>().As<ISeasonParser>().SingleInstance().Named<ISeasonParser>("html");
            containerBuilder.RegisterType<BulkDataImporter>().As<IBulkDataImporter>().InstancePerDependency();
            containerBuilder.RegisterType<WokiScraper>().As<IWikiScraper>().InstancePerDependency();
        }
    }
}