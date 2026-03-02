using LibrarySystem.Application.DTOs.Users;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Interfaces;

namespace LibrarySystem.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Phone=u.Phone
            }).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Phone=user.Phone
            };
        }

        public async Task<Guid> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Phone= dto.Phone
            };

            await _repository.AddAsync(user);
            return user.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            user.FullName = dto.FullName;
            user.Phone = dto.Phone;

            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
