using EconomizzeHybrid.Model;

namespace EconomizzeHybrid.Services.Interfaces
{
    public interface IUserServices
    {
        Task CreateUserAsync(UserModel userModel, string userToken);
        Task ReadAsyncById(int id);
        UserModel CurrentUserDetails { get; set; }
        bool isError { get; set; }
    }
}
