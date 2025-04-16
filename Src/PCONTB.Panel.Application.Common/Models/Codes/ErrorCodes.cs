namespace PCONTB.Panel.Application.Common.Models.Codes
{
    public static class ErrorCodes
    {
        public static class Project
        {
            public static readonly ErrorCode NotFound = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(NotFound),
                Message = "Project not found"
            };

            public static readonly ErrorCode ProjectNameEmpty = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(ProjectNameEmpty),
                Message = "Project name cannot be empty."
            };
        }

        public static class User
        {
            public static readonly ErrorCode AccesDenied = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(AccesDenied),
                Message = "User have no access"
            };

            public static readonly ErrorCode NotFound = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(NotFound),
                Message = "User not found"
            };

            public static readonly ErrorCode UsernameEmpty = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(UsernameEmpty),
                Message = "User name cannot be empty."
            };

            public static readonly ErrorCode UsernameExist = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(UsernameExist),
                Message = "A user with the provided username already exists."
            };

            public static readonly ErrorCode EmailEmpty = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(EmailEmpty),
                Message = "Email cannot be empty."
            };

            public static readonly ErrorCode EmailBadFormat = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(EmailBadFormat),
                Message = "Invalid email format. Please include the '@' symbol in the address."
            };

            public static readonly ErrorCode EmailExist = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(EmailExist),
                Message = "A user with the provided email already exists"
            };

            public static readonly ErrorCode PasswordEmpty = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(PasswordEmpty),
                Message = "Password cannot be empty."
            };

            public static readonly ErrorCode PasswordMinimalLength = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(PasswordMinimalLength),
                Message = "Password must be at least 8 characters long."
            };

            public static readonly ErrorCode PasswordMatchRules = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(PasswordMatchRules),
                Message = "Password must contain at least one uppercase letter, one special character, and one digit."
            };

            public static readonly ErrorCode LoginWrongCredential = new ErrorCode
            {
                Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(LoginWrongCredential),
                Message = "Wrong username or password."
            };

        }
    }
}
