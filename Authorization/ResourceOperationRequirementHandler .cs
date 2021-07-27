using HrProgram.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HrProgram.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Vacation>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Vacation vacation)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if (requirement.ResourceOperation == ResourceOperation.Create ||
               requirement.ResourceOperation == ResourceOperation.Delete ||
               requirement.ResourceOperation == ResourceOperation.Update)
            {
                if(userId == vacation.UserId)
                {
                    context.Succeed(requirement);
                } 
            }

            if (requirement.ResourceOperation == ResourceOperation.GetAll ||
                requirement.ResourceOperation == ResourceOperation.GetById)
            {
                if (userId == vacation.UserId ||
                    userRole == "Hr")
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
