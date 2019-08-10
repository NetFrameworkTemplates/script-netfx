using System.Net;
using Funq;
using ServiceStack;
using MyApp.ServiceInterface;

namespace MyApp
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("MyApp", typeof(MyServices).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            Plugins.Add(new SharpPagesFeature());

            SetConfig(new HostConfig
            {
                DebugMode = AppSettings.Get("DebugMode", false),
                WebHostPhysicalPath = MapProjectPath("~/wwwroot"),
                AddRedirectParamsToQueryString = true,
                UseCamelCase = true,
            });

            this.CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new SharpPageHandler("/notfound");
            this.CustomErrorHttpHandlers[HttpStatusCode.Forbidden] = new SharpPageHandler("/forbidden");
        }
    }
}