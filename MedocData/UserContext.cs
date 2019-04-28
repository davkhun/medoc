using MedocDto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocData
{
    public class UserContext
    {
        private Connect sql = new Connect();

        public enum CheckUserResult
        {
            Ok=1,
            DoesNotExist = -1
        };

        public enum CreateUserResult
        {
            Ok = 1,
            AlreadyExists = -1
        };

        public CreateUserResult CreateUser(string login, string password, string email)
        {
            var result = sql.ExecuteScalar<int>("CALL `users_create`(@login,@email,@password)", new Dictionary<string, string>
            {
                {"@login", login },
                {"@password", password },
                {"@email", email }
            });
            return (CreateUserResult)result;
        }

        public CheckUserResult CheckUser(string login, string password)
        {
            var result= sql.ExecuteScalar<int>("CALL `users_check`(@login,@password)", new Dictionary<string, string>
            {
                { "@login", login},
                {"@password", password }
            });
            return (CheckUserResult)result;
        }

        public UserModel GetUser(string login)
        {
            var result = sql.ExecuteAdapter("CALL `users_get`(@login)", new Dictionary<string, string>
            {
                {"@login", login }
            });
            if (result.Rows.Count == 0)
                return null;
            return new UserModel
            {
                Id = Convert.ToInt32(result.Rows[0]["id"]),
                Login = result.Rows[0]["login"].ToString(),
                Email = result.Rows[0]["email"].ToString()
            };
        }
    }
}
