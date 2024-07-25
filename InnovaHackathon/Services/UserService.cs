using Core.Models;
using InnovaHackathon.Dtos;
using Microsoft.AspNetCore.Identity;

namespace InnovaHackathon.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserProfileDto> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return new UserProfileDto
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }

}
