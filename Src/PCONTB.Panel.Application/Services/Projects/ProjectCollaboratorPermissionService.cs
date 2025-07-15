using PCONTB.Panel.Application.Common;
using PCONTB.Panel.Application.Common.Exceptions;
using PCONTB.Panel.Application.Contracts.Services.Auth;
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Collaborators;

namespace PCONTB.Panel.Application.Services.Projects
{
    public class ProjectCollaboratorPermissionService(ISessionAccesor sessionAccesor) 
        : IProjectCollaboratorPermissionService
    {
        public async Task Verify(Project project, ProjectPermission permission, CancellationToken cancellationToken)
        {
            var userId = sessionAccesor.Session.UserId;

            if (project.UserId == userId)
                return;

            var collaborator = project.Collaborators.FirstOrDefault(c => c.UserId == userId);

            if (collaborator == null)
                throw new UnauthorizedException(ErrorCodes.Projects.Project.Access.Message);

            switch (permission)
            {
                case ProjectPermission.ManageProjectPermission:
                    VerifyManageProjectPermission(collaborator);
                    break;
                case ProjectPermission.ManageCommunityPermission:
                    VerifyManageCommunityPermission(collaborator);
                    break;
                case ProjectPermission.ManageFulfillmentPermission:
                    VerifyManageFulfillmentPermission(collaborator);
                    break;
                default:
                    break;
            }
        }

        private static void VerifyManageProjectPermission(ProjectCollaborator collaborator)
        {
            if (!collaborator.ManageProjectPermission)
                throw new UnauthorizedException(ErrorCodes.Projects.ProjectCollaborator.ManageProject.Message);
        }

        private static void VerifyManageCommunityPermission(ProjectCollaborator collaborator)
        {
            if (!collaborator.ManageCommunityPermission)
                throw new UnauthorizedException(ErrorCodes.Projects.ProjectCollaborator.ManageCommunity.Message);
        }

        private static void VerifyManageFulfillmentPermission(ProjectCollaborator collaborator)
        {
            if (!collaborator.ManageFulfillmentPermission)
                throw new UnauthorizedException(ErrorCodes.Projects.ProjectCollaborator.ManageFulfillment.Message);
        }
    }
}