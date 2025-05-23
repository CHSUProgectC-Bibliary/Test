using AutoMapper;
using BookReviewAPI.Data;
using BookReviewAPI.Data.Dto;
using BookReviewAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
namespace BookReviewAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
        Task<UserDto> GetUserById(int id, CancellationToken cancellationToken);
        Task<UserDto> CreateUser(CreateUserDto UserDto, CancellationToken cancellationToken);
        Task UpdateUser(int id, UpdateUserDto UserDto, CancellationToken cancellationToken);
        Task DeleteUser(int id, CancellationToken cancellationToken);
    }
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDto> CreateUser(CreateUserDto userDto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteUser(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new NotFoundException("user not found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken)
        {
            var allUsers = await _context.Users.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<UserDto>>(allUsers);
        }

        public async Task<UserDto> GetUserById(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateUser(int id, UpdateUserDto UserDto, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new NotFoundException("user not found");

            _mapper.Map(UserDto, user);
            await _context.SaveChangesAsync();
        }
    }
}
