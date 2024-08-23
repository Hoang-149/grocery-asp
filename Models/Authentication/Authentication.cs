//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace BanHangAsp.Models.Authentication
//{
//    public class Authentication : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            if (context.HttpContext.Session.GetString("UserName") == null) {
//                context.Result = new RedirectToActionResult(new RouteValueDictionary
//                {
//                    {"Controller", "Access" },
//                    {"Action", "Login" }
//                });
//            }
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BanHangAsp.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Access", null);
            }
        }
    }
}

