using Dapper;
using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using EconomizzeHybrid.SqlLiteData;

namespace EconomizzeHybrid.Services.Classes
{
    public class UserLoginCacheServices : ICacheServices
    {
        private readonly ICacheFactory _cacheFactory;
        private readonly MessageHandler _messageHandler;
        public UserLoginModel? CurrentUser { get; set; }


        public UserLoginCacheServices(ICacheFactory cacheFactory, 
                                      MessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
            _cacheFactory = cacheFactory;
        }

        public async Task CreateUserSession(UserLoginModel userLoginModel)
        {
            var connection = _cacheFactory.GetConnection();
            connection.Open();
            try
            {
                var sql = "INSERT INTO UserLogin(UserId, Username, UserToken, Active) " +
                                       "VALUES (@UserId, @Username, @UserToken, @Active) " +
                                       "ON CONFLICT(UserId) DO UPDATE " +
                                       "SET UserToken = @UserToken, " +
                                           "Active = @Active";

                var parameters = new
                {
                    UserId = userLoginModel.UserId,
                    Username = userLoginModel.Username,
                    UserToken = userLoginModel.UserToken,
                    Active = 1,
                    LastLogInDate = DateTime.UtcNow.ToString()
                };

                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                _messageHandler.Message = ex.Message;
            }
            finally
            {

                connection.Close();
            }
        }

        public async Task LogOut()
        {
            var connection = _cacheFactory.GetConnection();
            connection.Open();
            try
            {
                var sql = "UPDATE UserLogin " +
                            "SET Active = @Active";
                var parameters = new
                {
                    Active = 0
                };

                await connection.ExecuteAsync(sql, parameters);

            }
            catch (Exception ex)
            {
                _messageHandler.Message = ex.Message;
            }
            finally
            {

                connection.Close();
            }
        }

        public async Task SearchActiveSession()
        {
            var connection = _cacheFactory.GetConnection();
            
            try
            {
                connection.Open();
                var sql = "SELECT UserId, Username, UserToken " +
                          "FROM UserLogin " +
                          "WHERE Active = @Active " +
                          "LIMIT 1";
                var parameters = new
                {
                    Active = 1
                };

                CurrentUser = await connection.QuerySingleOrDefaultAsync<UserLoginModel>(sql, parameters);
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
    }
}
