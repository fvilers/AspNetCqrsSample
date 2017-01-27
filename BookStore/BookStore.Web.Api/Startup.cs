using BookStore.Web.Api;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(Startup))]

namespace BookStore.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            ConfigureWebApi(app);
        }
    }
}