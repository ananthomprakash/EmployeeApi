namespace Employees.Auth.Interface
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
