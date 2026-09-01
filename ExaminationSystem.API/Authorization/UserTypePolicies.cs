namespace ExaminationSystem.API.Authorization;

public static class UserTypePolicies
{
    public const string AdminOnly = "AdminOnly";
    public const string StudentOnly = "StudentOnly";
    public const string InstructorOnly = "InstructorOnly";
}
