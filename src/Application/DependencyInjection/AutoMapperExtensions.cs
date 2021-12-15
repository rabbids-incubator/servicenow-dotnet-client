﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace RabbidsIncubator.ServiceNowClient.Application.DependencyInjection
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                // Infrastructure
                x.AddProfile(new Infrastructure.ServiceNowRestApi.MappingProfiles.ServiceNowRestApiMappingProfile());
                // General
                x.AllowNullCollections = true;
            });

            var mapper = mappingConfig.CreateMapper();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            services.AddSingleton(mapper);
            return services;
        }
    }
}