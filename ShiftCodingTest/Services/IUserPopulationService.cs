using ShiftCodingTest.Models;

namespace ShiftCodingTest.Services
{
    public interface IUserPopulationService
    {
        Task<List<User>?> GetUsersAsync();
    }
}
