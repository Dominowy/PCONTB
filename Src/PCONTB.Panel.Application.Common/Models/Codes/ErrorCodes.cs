namespace PCONTB.Panel.Application.Common.Models.Codes
{
    public static class ErrorCodes
    {
        public static class Project
        {
            public static readonly ErrorCode ProjectNameEmpty = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(ProjectNameEmpty),
                Message = "Project name cannot be empty"
            };
        }
    }
}
