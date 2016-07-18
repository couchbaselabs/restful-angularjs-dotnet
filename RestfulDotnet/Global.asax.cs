using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Couchbase;
using Couchbase.Configuration.Client;

namespace RestfulDotnet
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var config = new ClientConfiguration();
            config.Servers = new List<Uri> { new Uri(ConfigurationManager.AppSettings["CouchbaseServer"]) };
            config.UseSsl = false;
            ClusterHelper.Initialize(config);
        }

        protected void Application_End()
        {
            ClusterHelper.Close();
        }
    }
}
