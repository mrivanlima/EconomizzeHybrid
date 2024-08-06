using EconomizzeHybrid.Model;

namespace EconomizzeHybrid.Services.Interfaces
{
    public interface IUserServices
    {
        Task ReadAsyncById(int id);
        UserModel CurrentUserDetails { get; set; }
        string Message { get; set; }
    }
}
