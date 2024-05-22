using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Domain.DTOs;
using PosterrPosts.Test.Helper;
using System;
using Xunit;

namespace PosterrPosts.Test.Services
{
    public class UserServiceTest
    {
        private readonly IUserService _userService = ServicesHelper.CreateUserService();
        private static readonly string date = DateTime.Now.Date.ToString("MMMM dd, yyyy");

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void MustReturnUser(int id)
        {
            var user = _userService.GetUserById(id).Result;

            var userData = new UserDataDTO() { UserName = $"User{id}", DateJoined = date, QtdPosts = id };

            Assert.Equal(userData.UserName, user.UserName);
            Assert.Equal(userData.QtdPosts, user.QtdPosts);
            Assert.Equal(userData.DateJoined, user.DateJoined);
        }
    }
}
