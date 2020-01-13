using Microsoft.Extensions.Configuration;
using TestFramework.Test.Models;

namespace TestFramework.Test.Services
{
    public static class UserCreator
    {
        public static User CreateUser(IConfiguration configuration)
        {
            var user = new User
            {
                Email = configuration["user:email"],
                Name = configuration["user:name"],
                Surname = configuration["user:surname"],
                Password = configuration["user:password"],
                NameChange = configuration["user:nameChange"],
                PhoneNumber = configuration["user:phoneNumber"],
                CommentText = configuration["user:commentText"],
                CommentResult = configuration["user:commentResult"]
            };
            return user;
        }
    }
}
