using Autofac;
using ShitLeopard.Api.Contracts;
using ShitLeopard.Api.Services;
using ShitLeopard.Common.Contracts;

namespace ShitLeopard.Api
{
    public static class AutoFacRegistrationModule
    {
        public static void Build(ContainerBuilder builder)
        {
            builder.RegisterType<TagService>().As<ITagService>().InstancePerDependency();
            //builder.RegisterType<CharacterService>().As<ICharacterService>().InstancePerDependency();
            //builder.RegisterType<QuoteService>().As<IQuoteService>().InstancePerDependency();
            builder.RegisterType<EpisodeService>().As<IEpisodeService>().InstancePerDependency();
            builder.RegisterType<ShowService>().As<IShowService>().InstancePerDependency();
            //builder.RegisterType<MongoProvider>().As<IMongoProvider>().SingleInstance();
            //builder.RegisterType<ScriptService>().As<IScriptService>().InstancePerDependency();
            builder.RegisterType<SearchService>().As<ISearchService>().InstancePerDependency();
            builder.RegisterType<EntityContext>().As<IEntityContext>().InstancePerDependency();
            //builder.RegisterType<SeasonService>().As<ISeasonService>().InstancePerDependency();
            builder.RegisterType<RequestProfileService>().As<IRequestProfileService>().InstancePerDependency();

   
         
            //builder.RegisterType<StanfordNaturalLanguageService>().As<INaturalLanguageService>().InstancePerDependency();
        }
    }
}