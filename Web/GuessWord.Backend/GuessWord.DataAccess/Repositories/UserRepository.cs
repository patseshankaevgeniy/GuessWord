using GuessWord.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace GuessWord.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User GetByLogin(string login)
        {
            var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                //sqlCommand.CommandText = $"select [Id], [Name], [Login], [Password] from [Users] where [Login] = '{login}'";
                sqlCommand.CommandText = $"select * from Users as u where u.Login = '{login}'";

                var reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        var user = new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Login = reader.GetString(2),
                            Password = reader.GetString(3)
                        };
                        return user;
                    }
                }
                return null;
            }
            finally
            {
                sqlConnection.Dispose();
            }
        }

        public int AddNewUser(string name, string login, string password)
        {
            var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;


                sqlCommand.CommandText = $"insert into Users Values ('{login}', '{name}', '{password}'); SELECT CAST(scope_identity() AS int)";
                var id = sqlCommand.ExecuteScalar();
                return (int)id;
            }
            catch (SqlException)
            {
                return Constants.DublicateResult;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
