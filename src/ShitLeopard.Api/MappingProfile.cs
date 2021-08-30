using AutoMapper;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Documents;
using ShitLeopard.Common.Models;

namespace ShitLeopard.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Tags, TagsModel>().ReverseMap();
            //CreateMap<Character, CharacterModel>().ReverseMap();
            //CreateMap<Episode, EpisodeModel>().ReverseMap();
            //CreateMap<Quote, QuoteModel>().ReverseMap();
            //CreateMap<ScriptLine, ScriptLineModel>().ReverseMap();
            //CreateMap<Script, ScriptModel>().ReverseMap();
            //CreateMap<ScriptWord, ScriptWordModel>().ReverseMap();
            //CreateMap<Season, SeasonModel>().ReverseMap();
            CreateMap<RequestProfileDocument, RequestProfileModel>().ReverseMap();
            CreateMap<RequestProfileDocument, SiteMetricsModel>();
            //       .ForMember(dest => dest.Headers, opts =>
            //       opts.MapFrom(src => JsonConvert.DeserializeObject(src.Headers ?? string.Empty)));

            CreateMap<DialogDocument, DialogModel>()
                .ForMember(dest => dest.Id, src => src.MapFrom(s => s.ID)).ReverseMap();
            CreateMap<EpisodeDocument, DialogModel>()
               .ForMember(dest => dest.SeasonId, src => src.MapFrom(s => s.SeasonId))
                .ForMember(dest => dest.Synopsis, src => src.MapFrom(s => s.Synopsis))
                 .ForMember(dest => dest.EpisodeTitle, src => src.MapFrom(s => s.Title))
                  .ForMember(dest => dest.EpisodeNumber, src => src.MapFrom(s => s.EpisodeNumber))
                  .ForMember(dest => dest.EpisodeOffsetId, src => src.MapFrom(s => s.OffsetId)).ReverseMap();
            CreateMap<TagsModel, TagsDocument>()
               .ForMember(dest => dest.ID, src => src.MapFrom(s => s.Id)).ReverseMap();

            CreateMap<EpisodeModel, EpisodeDocument>()
              .ForMember(dest => dest.ID, src => src.MapFrom(s => s.Id)).ReverseMap();

            CreateMap<ShowModel, ShowDocument>()
                .ForMember(dest => dest.ID, src => src.MapFrom(s => s.Id)).ReverseMap();
        }
    }
}