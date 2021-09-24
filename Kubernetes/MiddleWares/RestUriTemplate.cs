using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MiddleWares
{
    public class RestUriTemplate
    {
        readonly IConfiguration _configuration;

        public RestUriTemplate(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                //Check if route attribute is alredy defined  
                var hasRoute = controller.Selectors.Any(selector => selector.AttributeRouteModel != null);
                if (hasRoute)
                {
                    continue;
                }

                //Get the version as last part of namespace  
                //var version = controller.ControllerType.Namespace.Split('.').LastOrDefault();
                var version = _configuration.GetSection("Version:ApiVersion");
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = string.Format("SearchApi/{0}/{1}", version.Value, controller.ControllerName)
                };
            }
        }

    }
}
