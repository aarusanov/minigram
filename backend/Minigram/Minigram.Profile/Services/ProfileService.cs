namespace Minigram.Profile.Services
{
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using Minigram.Core.Dto;
    using Minigram.Core.Exceptions;
    using Minigram.Core.Repositories;
    using Minigram.Profile.Dto;
    using Minigram.Profile.Models;
    using Minigram.Profile.Extensions;

    public class ProfileService
    {
        private readonly IRepository<Profile> _profileRepository;

        private IQueryable<Profile> Profiles => _profileRepository.Get();

        public ProfileService(IRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<List<ProfileResponseDto>> GetAll(QueryParams queryParams)
        {
            ArgumentNullException.ThrowIfNull(queryParams);

            IQueryable<Profile> profiles = Profiles;

            int? page = queryParams.Page;
            int? perPage = queryParams.PerPage;

            if (page.HasValue && perPage.HasValue)
            {
                profiles.Skip(page.Value * perPage.Value).Take(perPage.Value);
            }

            return await profiles
                .Select(u => u.ToDto())
                .ToListAsync();
        }

        public async Task<Profile> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(id)} cannot be {id}", nameof(id));
            }

            Profile? profile = await Profiles
                .FirstOrDefaultAsync(u => u.Id == id);

            if (profile == null)
            {
                throw new EntityNotFoundException(typeof(Profile), id);
            }

            return profile;
        }

        public async Task<Profile> GetByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(userId)} cannot be {userId}", nameof(userId));
            }

            Profile? profile = await Profiles
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (profile == null)
            {
                throw new EntityNotFoundException(nameof(Profile), nameof(Profile.UserId), userId);
            }

            return profile;
        }

        public async Task<int> Count()
        {
            return await _profileRepository.Count();
        }

        public async Task<Profile> Create(Guid userId, ProfileRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            if (userId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(userId)} cannot be {userId}", nameof(userId));
            }

            Profile profile = new ()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = dto.Name,
                PhotoUrl = dto.PhotoUrl,
            };

            await _profileRepository.Create(profile);
            await _profileRepository.SaveAsync();

            return profile;
        }

        public async Task Update(Profile profile, ProfileRequestDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            ArgumentNullException.ThrowIfNull(profile);

            profile.Name = dto.Name;
            profile.PhotoUrl = dto.PhotoUrl;

            _profileRepository.Update(profile);
            await _profileRepository.SaveAsync();
        }

        public async Task Patch(Profile profile, JsonPatchDocument<ProfileRequestDto> patch)
        {
            ArgumentNullException.ThrowIfNull(patch);
            ArgumentNullException.ThrowIfNull(profile);

            ProfileRequestDto dto = new ()
            {
                Name = profile.Name,
                PhotoUrl = profile.PhotoUrl,
            };

            patch.ApplyTo(dto);

            profile.Name = dto.Name;
            profile.PhotoUrl = dto.PhotoUrl;

            _profileRepository.Update(profile);
            await _profileRepository.SaveAsync();
        }
    }
}