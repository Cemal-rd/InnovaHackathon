using InnovaHackathon.Dtos;

namespace InnovaHackathon.Services
{
   
        public interface IUserService
        {
            Task<UserProfileDto> GetUserProfileAsync(string userId);
        }

    
}
