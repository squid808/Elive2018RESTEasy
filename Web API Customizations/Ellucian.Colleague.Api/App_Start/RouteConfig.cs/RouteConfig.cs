using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Ellucian.Web.Http.Routes;

namespace Ellucian.Colleague.Api
{
    /// <summary>
    /// Route configuration
    /// </summary>
    public class RouteConfig
    {
        private const string HedtechIntegrationMediaTypeFormat = "application/vnd.hedtech.integration.v{0}+json";
        private const string HedtechIntegrationQapiMediaTypeFormat = "application/vnd.hedtech.integration.{0}.v{1}+json";
        private const string EllucianPDFMediaTypeFormat = "application/vnd.ellucian.v{0}+pdf";
        private const string EllucianJsonPilotMediaTypeFormat = "application/vnd.ellucian-pilot.v{0}+json";

        /// <summary>
        /// Registers the routes
        /// </summary>
        /// <param name="routes">Register the routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Custom

			//An example GET method.
            routes.MapHttpRoute(
                name: "GetCustomWebAdvisorIdAndDateFromColleagueId",
                routeTemplate: "custom/WebAdvisorIdAndDate/{ColleagueId}",
                defaults: new { controller = "CustomWebAdvisorIdAndDate", 
                    action = "GetWebAdvisorIdAndDateFromColleagueIdAsync" },
                constraints: new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    headerVersion = new HeaderVersionConstraint(1, true)
                }
            );

			//Here's an example POST method. Note this technically is a fancy GET, but it's an example.
            routes.MapHttpRoute(
                name: "GetCustomWebAdvisorIdAndDateFromWaiddObject",
                routeTemplate: "custom/WebAdvisorIdAndDate",
                defaults: new { controller = "CustomWebAdvisorIdAndDate", 
                    action = "PostWebAdvisorIdAndDateWaiddObjectAsync" },
                constraints: new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    headerVersion = new HeaderVersionConstraint(1, true)
                }
            );

            #endregion

...