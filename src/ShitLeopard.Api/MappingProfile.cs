﻿using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;
using ShitLeopard.Api.Models;
using ShitLeopard.Common.Models;
using ShitLeopard.DataLayer.Entities;

namespace ShitLeopard.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tags, TagsModel>().ReverseMap();
            CreateMap<Character, CharacterModel>().ReverseMap();
            CreateMap<Episode, EpisodeModel>().ReverseMap();
            CreateMap<Quote, QuoteModel>().ReverseMap();
            CreateMap<ScriptLine, ScriptLineModel>().ReverseMap();
            CreateMap<Script, ScriptModel>().ReverseMap();
            CreateMap<ScriptWord, ScriptWordModel>().ReverseMap();
            CreateMap<Season, SeasonModel>().ReverseMap();
            CreateMap<RequestProfile, RequestProfileModel>().ReverseMap();
            CreateMap<RequestProfile, SiteMetricsModel>()
                   .ForMember(dest => dest.Headers, opts =>
                   opts.MapFrom(src => JsonConvert.DeserializeObject(src.Headers ?? string.Empty)));


        }
    }
}