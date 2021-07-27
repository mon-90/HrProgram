using HrProgram.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HrProgram.Authorization
{
    public class EditPasswordRequirementHandler : AuthorizationHandler<EditPasswordRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditPasswordRequirement requirement, User user)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (userId == user.Id)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
