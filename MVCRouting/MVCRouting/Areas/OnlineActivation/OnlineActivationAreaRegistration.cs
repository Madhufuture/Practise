using System.Web.Mvc;

namespace MVCRouting.Areas.OnlineActivation
{
    public class OnlineActivationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OnlineActivation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OnlineActivation_default",
                "OnlineActivation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}