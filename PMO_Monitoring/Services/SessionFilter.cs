using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PMO_Monitoring.Services
{

    public class SessionFilter : Attribute, IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if there is no session whitch key is "register", user will not access to specified action and redirect to login page.

            var result = context.HttpContext.Session.GetInt32("IdUser");
            if (result == null)
            {
                context.Result = new RedirectToActionResult("Index", "LoginPage", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

}

