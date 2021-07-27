using Microsoft.AspNetCore.Authorization;

namespace HrProgram.Authorization
{
    public enum ResourceOperation
    {
        GetAll,
        GetById,
        Create,
        Delete,
        Update
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }

        public ResourceOperation ResourceOperation { get; }
    }
}
