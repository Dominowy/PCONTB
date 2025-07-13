namespace PCONTB.Panel.Application.Common.Models.Codes
{
    public static class ErrorCodes
    {
        public static class Projects
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

                public static readonly ErrorCode CategoryEmpty = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(CategoryEmpty),
                    Message = "Category cannot be empty."
                };

                public static readonly ErrorCode CategoryExist = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(CategoryExist),
                    Message = "Category not exist."
                };

                public static readonly ErrorCode SubcategoryExist = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(SubcategoryExist),
                    Message = "Subcategory not exist."
                };

                public static readonly ErrorCode CountryExist = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(CountryExist),
                    Message = "Country not exist."
                };

                public static readonly ErrorCode CountryEmpty = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Project) + "." + nameof(CountryEmpty),
                    Message = "Country cannot be empty."
                };
            }

            public static class ProjectCollaborator
            {
                public static readonly ErrorCode NotFound = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectCollaborator) + "." + nameof(NotFound),
                    Message = "Collaborator not found"
                };

                public static readonly ErrorCode UserEmpty = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectCollaborator) + "." + nameof(UserEmpty),
                    Message = "User cannot be empty."
                };

                public static readonly ErrorCode UserExist = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectCollaborator) + "." + nameof(UserExist),
                    Message = "User not exist."
                };

                public static readonly ErrorCode UserExistInProject = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectCollaborator) + "." + nameof(UserExistInProject),
                    Message = "User is already add."
                };

                public static readonly ErrorCode TryToAddCreator = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectCollaborator) + "." + nameof(TryToAddCreator),
                    Message = "Cannot add creator as collaborator."
                };

                public static readonly ErrorCode ProjectEmpty = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectCollaborator) + "." + nameof(ProjectEmpty),
                    Message = "Project cannot be empty."
                };

                public static readonly ErrorCode ProjectExist = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectCollaborator) + "." + nameof(ProjectExist),
                    Message = "Project not exist."
                };
            }
            public static class ProjectImage
            {
                public static readonly ErrorCode PathNotExist = new ErrorCode
                {
                    Code = nameof(PathNotExist) + "." + nameof(ProjectImage) + "." + nameof(NotFound),
                    Message = "Path not exist"
                };

                public static readonly ErrorCode NotFound = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectImage) + "." + nameof(NotFound),
                    Message = "Image not found"
                };

                public static readonly ErrorCode ImageNotSelect = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectImage) + "." + nameof(ImageNotSelect),
                    Message = "Image not select"
                };

                public static readonly ErrorCode TypeAllowed = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(ProjectImage) + "." + nameof(TypeAllowed),
                    Message = "Only JPG, PNG, WEBP or GIF files are allowed."
                };

                
            }
        }

        public static class Categories
        {
            public static class Category
            {
                public static readonly ErrorCode NotFound = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Category) + "." + nameof(NotFound),
                    Message = "Category not found."
                };

                public static readonly ErrorCode NameEmpty = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Category) + "." + nameof(NameEmpty),
                    Message = "Name cannot be empty."
                };

                public static readonly ErrorCode NameExist = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Category) + "." + nameof(NameExist),
                    Message = "A category with the provided name already exists."
                };
            }
        }

        public static class Users
        {
            public static class User
            {
                public static readonly ErrorCode AccesDenied = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(AccesDenied),
                    Message = "User have no access."
                };

                public static readonly ErrorCode AccountLock = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(AccountLock),
                    Message = "User is locked."
                };

                public static readonly ErrorCode NotFound = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(NotFound),
                    Message = "User not found."
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

                public static readonly ErrorCode PasswordsNotEqual = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(PasswordsNotEqual),
                    Message = "Passwords must be equal."
                };

                public static readonly ErrorCode RoleEmpty = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(RoleEmpty),
                    Message = "Role cannot be empty."
                };

                public static readonly ErrorCode RoleDuplicate = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(RoleDuplicate),
                    Message = "Role cannot be duplicate."
                };

                public static readonly ErrorCode RoleValid = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(RoleValid),
                    Message = "Role is not valid."
                };

                public static readonly ErrorCode UserIsNotBlock = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(User) + "." + nameof(UserIsNotBlock),
                    Message = "User is not block."
                };
            }
        }

        public static class Countries
        {
            public static class Country
            {
                public static readonly ErrorCode NotFound = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Country) + "." + nameof(NotFound),
                    Message = "Country not found."
                };

                public static readonly ErrorCode NameEmpty = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Country) + "." + nameof(NameEmpty),
                    Message = "Name cannot be empty."
                };

                public static readonly ErrorCode NameExist = new ErrorCode
                {
                    Code = nameof(ErrorCode) + "." + nameof(Country) + "." + nameof(NameExist),
                    Message = "A country with the provided name already exists."
                };
            }
        }
    }
}