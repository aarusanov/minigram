namespace Minigram.Auth.Services
{
    using Microsoft.EntityFrameworkCore;
    using Minigram.Auth.Dto;
    using Minigram.Auth.Dto.User;
    using Minigram.Auth.Extensions;
    using Minigram.Core.Exceptions;
    using Minigram.Core.Models;
    using Minigram.Core.Repositories;

    public class UserService
    {
        private readonly IRepository<User> _userRepository;

        private IQueryable<User> Users => _userRepository.Get();

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<ReadUserDto>> GetAll(QueryParams queryParams)
        {
            ArgumentNullException.ThrowIfNull(queryParams);

            IQueryable<User> users = Users;

            int? page = queryParams.Page;
            int? perPage = queryParams.PerPage;

            if (page.HasValue && perPage.HasValue)
            {
                users.Skip(page.Value * perPage.Value).Take(perPage.Value);
            }

            return await users
                .Include(u => u.Profiles)
                .Select(u => u.ToDto())
                .ToListAsync();
        }

        public async Task<ReadUserDto> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }

            User? user = await Users
                .Include(u => u.Profiles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user.ToDto();
        }

        public async Task<int> Count()
        {
            return await _userRepository.Count();
        }

        public async Task Create()
        {
            
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }

            User? user = await Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            _userRepository.Delete(user);
            await _userRepository.SaveAsync();
        }
    }
}