﻿using AutoMapper;

namespace RabbidsIncubator.ServiceNowClient.Infrastructure.ServiceNowRestApi.MappingProfiles
{
    public class ServiceNowRestApiMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "RabbidsIncubatorServiceNowClientRestApiServiceNowRestApiMappingProfile"; }
        }

        public ServiceNowRestApiMappingProfile()
        {
            CreateMap<Dto.ConfigurationItemRelationshipDto, Domain.Models.ConfigurationItemRelationshipModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.SystemId))
                .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.Parent != null ? x.Parent.Value : null))
                .ForMember(x => x.TypeId, opt => opt.MapFrom(x => x.ConfigurationItemRelationshipType != null ? x.ConfigurationItemRelationshipType.Value : null));
        }
    }
}