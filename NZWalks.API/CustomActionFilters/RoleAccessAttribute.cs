using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.API.CustomActionFilters
{
    public class RoleAccessAttribute:ActionFilterAttribute
    {
        private readonly string[] roles;

        public RoleAccessAttribute(params string[] roles)
        {
            this.roles = roles;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if (user != null)
            {
                if (user.Identity is not { IsAuthenticated: true })
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                if (!roles.Any(x => user.IsInRole(x)))
                {
                    context.Result=new ForbidResult();
                    return;
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
