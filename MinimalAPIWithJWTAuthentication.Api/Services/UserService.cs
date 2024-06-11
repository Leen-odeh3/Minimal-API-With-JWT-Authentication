using MinimalAPIWithJWTAuthentication.Api.Abstracts;
using MinimalAPIWithJWTAuthentication.Api.Models;
using MinimalAPIWithJWTAuthentication.Api.Repositories;

namespace MinimalAPIWithJWTAuthentication.Api.Services;

public class UserService
{
    private readonly IUserRepo _userRepository;

    public UserService(IUserRepo userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task AddUserAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _userRepository.AddAsync(user);
    }

    public async Task DeleteUserAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _userRepository.DeleteAsync(user);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task UpdateUserAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
    {
        return await _userRepository.ValidateUserCredentialsAsync(username, password);
    }

    public async Task<User> GetByUserNameAsync(string userName)
    {
        return await _userRepository.GetUserByUsernameAsync(userName);
    }
}
