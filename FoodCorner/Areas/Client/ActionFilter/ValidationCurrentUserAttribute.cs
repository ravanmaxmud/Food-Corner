using Microsoft.AspNetCore.Mvc.Filters;
using FoodCorner.Areas.Client.Controllers;
using FoodCorner.Services.Abstracts;

namespace FoodCorner.Areas.Client.ActionFilter
{
    public class ValidationCurrentUserAttribute : IActionFilter
    {
        //private readonly IUserService _userService;

        //public ValidationCurrentUserAttribute(IUserService userService)
        //{
        //    _userService = userService;
        //}
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var user = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            //if (!user.IsAuthenticated)
            //{
            //    var controller = (AuthenticationController)context.Controller;
            //    context.Result = controller.RedirectToRoute("client-login");
            //}
        }
    }
}
