using Dapper;
using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using EconomizzeHybrid.SqlLiteData;
using Microsoft.Win32;

namespace EconomizzeHybrid.Services.Classes
{
    public class UserLoginSqliteServices : IUserLoginServices
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly IUserLoginServices _userLoginServices;
        public UserLoginModel? CurrentUser { get; set; }
        public RegisterModel RegisteredUser { get; set; }
        public string Message { get; set; }

        public UserLoginSqliteServices(IDatabaseConnectionFactory connectionFactory, 
                                       IUserLoginServices userLoginServices)
        {
            _connectionFactory = connectionFactory;
            _userLoginServices = userLoginServices;
        }

        public async Task CreateUserSession(UserLoginModel userLoginModel)
        {
            var connection = _connectionFactory.GetConnection();
            connection.Open();
            try
            {
                var sql = "INSERT INTO UserSession(UserId, UserToken, IsLogged, LastLoginDate) " +
                                       "VALUES (@UserId, @UserToken, @IsLogged, @LastLogInDate)";

                var parameters = new
                {
                    UserId = userLoginModel.UserId,
                    UserToken = userLoginModel.UserToken,
                    IsLogged = true,
                    LastLogInDate = DateTime.UtcNow.ToString()
                };

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                connection.Close();
            }
        }

        public async Task CreateAsync(RegisterModel register)
        {
            var connection = _connectionFactory.GetConnection();
            connection.Open();
            try
            {
                var sql = "INSERT INTO UserLogin (UserId, Username) " +
                                         "VALUES (@UserId, @Username)";
                           
                var parameters = new
                {
                    UserId = register.UserId,
                    Username = register.Username
                };

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                
                connection.Close();
            }

        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task ReadAsync(UserLoginModel user)
        {
            var connection = _connectionFactory.GetConnection();
            connection.Open();
            try
            {
                var sql = "SELECT ul.*, us.UserToken " +
                          "FROM UserLogin ul  " +
                             "JOIN UserSession us " +
                                "ON ul.UserId = us.UserId " +
                                "AND us.IsLogged = 1 " +
                          "WHERE Username = @Username";
                var parameters = new
                {
                    Username = user.Username,
                };

                var results = await connection.QuerySingleOrDefaultAsync<UserLoginModel>(sql, parameters);
                _userLoginServices.CurrentUser = results;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                connection.Close();
            }
            
        }

        public Task VerifyAsync(RegisterModel register)
        {
            throw new NotImplementedException();
        }

        Task IUserLoginServices.LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
