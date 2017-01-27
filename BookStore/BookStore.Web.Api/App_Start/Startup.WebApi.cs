using BookStore.Core.Http.Dependencies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Web.Http;

namespace BookStore.Web.Api
{
    public partial class Startup
    {
        public void ConfigureWebApi(IAppBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            // Web API configuration and services
            var configuration = new HttpConfiguration
            {
                DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer())
            };

            configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            configuration.MapHttpAttributeRoutes();

            app.UseWebApi(configuration);
        }
    }
}