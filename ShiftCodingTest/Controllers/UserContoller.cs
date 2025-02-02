using Microsoft.AspNetCore.Mvc;
using ShiftCodingTest.Services;

namespace ShiftCodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContoller : ControllerBase
    {

        private readonly IUserPopulationService _userPopulationService;

        public UserContoller(IUserPopulationService userPopulationService)
        {
            _userPopulationService = userPopulationService;
        }

        [HttpGet(("/user"))]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userPopulationService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet(("/user/{id}"))]
        public async Task<IActionResult> GetUserByID(int id)
        {
            var users = await _userPopulationService.GetUsersAsync();

            var user = users.Where(u=> u.Id == id).FirstOrDefault();

            return Ok(user);
        }

        [HttpGet(("/user/search"))]
        public async Task<IActionResult> SearchForUsers(string? name = null
            , string? email = null
            , string? streetAddress = null
            , string? phone = null
            , string? zip = null
            , string? companyName = null)
        {
            var users = await _userPopulationService.GetUsersAsync();

            var filteredUsers = users.AsQueryable();

            if (!string.IsNullOrEmpty(companyName))
                filteredUsers = filteredUsers.Where(u => !(u.Company == null || string.IsNullOrEmpty(u.Company.Name)) 
                                                        && u.Company.Name.Contains(companyName));

            if (!string.IsNullOrEmpty(streetAddress))
                filteredUsers = filteredUsers.Where(u => !(u.Address == null || string.IsNullOrEmpty(u.Address.Street))
                                                        && u.Address.Street.Contains(streetAddress));

            if (!string.IsNullOrEmpty(zip))
                filteredUsers = filteredUsers.Where(u => !(u.Address == null || string.IsNullOrEmpty(u.Address.ZIPCode)) &&
                                                            u.Address.ZIPCode.Contains(zip));

            if (!string.IsNullOrEmpty(name))
                filteredUsers = filteredUsers.Where(u => !string.IsNullOrEmpty(u.Name) && u.Name.Contains(name));

            if (!string.IsNullOrEmpty(email))
                filteredUsers = filteredUsers.Where(u => !string.IsNullOrEmpty(u.Email) && u.Email.Contains(email));

            if (!string.IsNullOrEmpty(phone))
                filteredUsers = filteredUsers.Where(u => !string.IsNullOrEmpty(u.Phone) && u.Phone.Contains(phone));

            return Ok(filteredUsers.ToList());
        }


    }
}
