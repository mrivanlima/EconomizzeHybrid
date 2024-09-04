using Dapper;
using EconomizzeHybrid.Model;
using EconomizzeHybrid.Services.Interfaces;
using EconomizzeHybrid.SqlLiteData;

namespace EconomizzeHybrid.Services.Classes
{
    public class CacheServices : ICacheServices
    {
        private readonly ICacheFactory _cacheFactory;
        private readonly MessageHandler _messageHandler;
        public UserLoginModel? CurrentUser { get; set; }
        public UserModel? UserDetails { get; set; }
        public AddressModel? UserAddress { get; set; }


        public CacheServices(ICacheFactory cacheFactory, 
                                      MessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
            _cacheFactory = cacheFactory;
        }

        public async Task CreateUserSession(UserLoginModel userLoginModel)
        {
            var connection = _cacheFactory.GetConnection();
            try
            {
                connection.Open();
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
            try
            {
                connection.Open();
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

        public async Task ReadUserDetails(int userId)
        {
            var connection = _cacheFactory.GetConnection();
            try
            {
                connection.Open();
                var sql = "SELECT  " +
                            "UserId, " +
                            "UserFirstName," +
                            "UserMiddleName, " +
                            "UserLastName, " +
                            "UserEmail, " +
                            "Cpf, " +
                            "Rg, " +
                            "PhoneNumber, " +
                            "DateOfBirth " +
                          "FROM User " +
                          "WHERE UserId = @UserId " +
                          "LIMIT 1";
                var parameters = new
                {
                    UserId = userId,
                };

                UserDetails = await connection.QuerySingleOrDefaultAsync<UserModel>(sql, parameters);
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

        public async Task ReadAddress(int userId)
        {
            var connection = _cacheFactory.GetConnection();
            try
            {
                connection.Open();
                var sql = "SELECT  " +
                            "UserId, " +
                            "StreetId, " +
                            "ZipCode, " +
                            "StreetName, " +
                            "Complement, " +
                            "NeighborhoodName, " +
                            "CityName, " +
                            "StateName, " +
                            "AddressTypeId, " +
                            "MainAddress " +
                          "FROM Address " +
                          "WHERE UserId = @UserId " +
                          "LIMIT 1";
                var parameters = new
                {
                    UserId = userId,
                };

                UserAddress = await connection.QuerySingleOrDefaultAsync<AddressModel>(sql, parameters);
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

        public async Task AddUserDetails(UserModel model)
        {
            var connection = _cacheFactory.GetConnection();
            try
            {
                connection.Open();
                var sql = "INSERT INTO User(" +
                    "                        UserId, " +
                                            "UserFirstName, " +
                                            "UserMiddleName, " +
                                            "UserLastName, " +
                                            "UserEmail, " +
                                            "Cpf, " +
                                            "Rg, " +
                                            "PhoneNumber, " +
                                            "DateOfBirth) " +
                                       "VALUES (" +
                                           "@UserId, " +
                                           "@UserFirstName, " +
                                           "@UserMiddleName," +
                                           "@UserLastName, " +
                                           "@UserEmail, " +
                                           "@Cpf, " +
                                           "@Rg, " +
                                           "@PhoneNumber, " +
                                           "@DateOfBirth) " +
                                       "ON CONFLICT(UserId) DO UPDATE " +
                                       "SET UserFirstName = @UserFirstName, " +
                                           "UserMiddleName = @UserMiddleName," +
                                           "UserLastName = @UserLastName, " +
                                           "UserEmail = @UserEmail, " +
                                           "Cpf = @Cpf, " +
                                           "Rg = @Rg, " +
                                           "PhoneNumber = @PhoneNumber, " +
                                           "DateOfBirth = @DateOfBirth";


                var parameters = new
                {
                    UserId = model.UserId,
                    UserFirstName = model.UserFirstName,
                    UserLastName = model.UserLastName,
                    UserMiddleName = model.UserMiddleName,
                    UserEmail = model.UserEmail,
                    Cpf = model.Cpf,
                    Rg = model.Rg,
                    DateOfBirth = model.DateOfBirth,
                    PhoneNumber = model.PhoneNumber
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

        public async Task AddUserAddress(AddressModel model)
        {
            var connection = _cacheFactory.GetConnection();
            try
            {
                connection.Open();
                var sql = "INSERT INTO Address(" +
                    "                        UserId, " +
                                            "StreetId, " +
                                            "ZipCode, " +
                                            "StreetName, " +
                                            "Complement, " +
                                            "NeighborhoodName, " +
                                            "CityName, " +
                                            "StateName, " +
                                            "AddressTypeId, " +
                                            "MainAddress)" +
                                       "VALUES (" +
                                           "@UserId, " +
                                           "@StreetId, " +
                                           "@ZipCode," +
                                           "@StreetName, " +
                                           "@Complement, " +
                                           "@NeighborhoodName, " +
                                           "@CityName, " +
                                           "@StateName, " +
                                           "@AddressTypeId, " +
                                           "@MainAddress)" +
                                       "ON CONFLICT(UserId) DO UPDATE " +
                                       "SET StreetId = @StreetId, " +
                                           "ZipCode = @ZipCode," +
                                           "StreetName = @StreetName, " +
                                           "Complement = @Complement, " +
                                           "NeighborhoodName = @NeighborhoodName, " +
                                           "CityName = @CityName, " +
                                           "StateName = @StateName, " +
                                           "AddressTypeId = @AddressTypeId, " +
                                           "MainAddress = @MainAddress";


                var parameters = new
                {
                    UserId = model.UserId,
                    StreetId = model.StreetId,
                    ZipCode = model.ZipCode,
                    StreetName = model.StreetName,
                    Complement = model.Complement,
                    NeighborhoodName = model.NeighborhoodName,
                    CityName = model.CityName,
                    StateName = model.StateName,
                    AddressTypeId = model.AddressTypeId,
                    MainAddress = model.MainAddress
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

        public Task VerifyAsync(RegisterModel register)
        {
            throw new NotImplementedException();
        }
    }
}
