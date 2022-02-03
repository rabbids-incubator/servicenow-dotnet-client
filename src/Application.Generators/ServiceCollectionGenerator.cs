﻿using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using RabbidsIncubator.ServiceNowClient.Application.Generators.Extensions;

namespace RabbidsIncubator.ServiceNowClient.Application.Generators
{
    [Generator]
    public class ServiceCollectionGenerator : GeneratorBase
    {
        protected override void GenerateCode(GeneratorExecutionContext context, Models.GenerationConfigurationModel model)
        {
            var sourceBuilder = new StringBuilder($@"
using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace {model.Namespaces.Root}.Infrastructure.ServiceNowRestClient.DependencyInjection
{{
    public static class GeneratedServiceCollectionExtensions
    {{
        public static IServiceCollection AddServiceNowRestClientGeneratedRepositories(this IServiceCollection services)
        {{
");
            foreach (var entityName in model.Entities?.Select(x => x.Name))
            {
                sourceBuilder.Append($@"
            services.TryAddTransient<Domain.Repositories.I{entityName.FirstCharToUpper()}Repository, Repositories.{entityName.FirstCharToUpper()}Repository>();
");
            }

            sourceBuilder.Append(@"
            return services;
        }
    }
}
");

            // inject the created source into the users compilation
            context.AddSource($"GeneratedServiceCollectionExtensions.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }
    }
}
