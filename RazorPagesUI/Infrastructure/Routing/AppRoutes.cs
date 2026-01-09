namespace RazorPagesUI.Infrastructure.Routing;

public static class AppRoutes
{
    public const string IndexPage = "/Index";
    private const string FormUrl = "/Forms";
    
    public const string LoginPage = "/Login";
    public const string LogoutPage = "/Logout";
    
    private const string User = $"{FormUrl}/User";
    public const string AddUser = $"{User}/AddUser";
    public const string DeleteUser = $"{User}/DeleteUser";
    public const string EditUser = $"{User}/EditUser";

    private const string Address = $"{FormUrl}/Address";
    public const string DeleteAddress = $"{Address}/DeleteAddress";
    public const string AddAddress = $"{Address}/AddAddress";
}
