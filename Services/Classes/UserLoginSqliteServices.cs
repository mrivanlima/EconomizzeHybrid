using Dapper;
using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using EconomizzeHybrid.SqlLiteData;

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

        public async Task CreateAsync(RegisterModel register)
        {
            var connection = _connectionFactory.GetConnection();
            connection.Open();
            try
            {
                var sql = "INSERT INTO UserLogin (UserId, Username, Password) " +
                                         "VALUES (@UserId, @Username, @Password)";
                var parameters = new
                {
                    UserId = register.UserId,
                    Username = register.Username,
                    Password = register.Password
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
                var sql = "SELECT* FROM UserLogin WHERE Username = @Username";
                var parameters = new
                {
                    Username = user.Username,
                };

                var results = await connection.QuerySingleOrDefaultAsync<UserLoginModel>(sql, parameters);
                var r = _userLoginServices;
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
    }
}
