namespace Minigram.Profile.Services
{
    using Microsoft.EntityFrameworkCore;
    using Minigram.Core.Dto;
    using Minigram.Core.Exceptions;
    using Minigram.Core.Repositories;
    using Minigram.Profile.Dto;
    using Minigram.Profile.Models;
    using Minigram.Profile.Extensions;

    public class RelationService
    {
        private readonly IRepository<Relation> _relationRepository;

        private IQueryable<Relation> Relations => _relationRepository.Get();

        public RelationService(IRepository<Relation> relationRepository)
        {
            _relationRepository = relationRepository;
        }

        public async Task<List<RelationResponseDto>> GetAllByStatus(
            Guid senderId,
            tRelationshipStatus status,
            QueryParams queryParams)
        {
            ArgumentNullException.ThrowIfNull(queryParams);

            if (senderId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(senderId)} cannot be {senderId}", nameof(senderId));
            }

            IQueryable<Relation> relations = Relations;

            int? page = queryParams.Page;
            int? perPage = queryParams.PerPage;

            if (page.HasValue && perPage.HasValue)
            {
                relations.Skip(page.Value * perPage.Value).Take(perPage.Value);
            }

            return await relations
                .Where(r => r.Status == status && r.SenderId == senderId)
                .Select(u => u.ToDto())
                .ToListAsync();
        }

        public async Task<int> CountByStatus(Guid senderId, tRelationshipStatus status)
        {
            if (senderId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(senderId)} cannot be {senderId}", nameof(senderId));
            }

            return await Relations
                .Where(r => r.Status == status && r.SenderId == senderId)
                .CountAsync();
        }

        public async Task<RelationResponseDto> Get(Guid senderId, Guid recieverId)
        {
            if (senderId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(senderId)} cannot be {senderId}", nameof(senderId));
            }

            if (recieverId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(recieverId)} cannot be {recieverId}", nameof(recieverId));
            }

            RelationResponseDto? relation = await Relations
                .Where(r => r.SenderId == senderId && r.ReceiverId == recieverId)
                .Select(r => r.ToDto())
                .FirstOrDefaultAsync();

            if (relation == null)
            {
                throw new EntityNotFoundException(typeof(Relation));
            }
            
            return relation;
        }

        public async Task<Relation> CreateOrUpdate(Guid senderId, Guid recieverId, tRelationshipStatus status)
        {
            if (senderId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(senderId)} cannot be {senderId}", nameof(senderId));
            }

            if (recieverId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(recieverId)} cannot be {recieverId}", nameof(recieverId));
            }

            Relation? relation = await Relations.FirstOrDefaultAsync(
                r => r.SenderId == senderId && r.ReceiverId == recieverId);

            if (relation == null)
            {
                relation = new Relation
                {
                    Id = Guid.NewGuid(),
                    SenderId = senderId,
                    ReceiverId = recieverId
                };

                await _relationRepository.Create(relation);
            }

            relation.Status = status;
            await _relationRepository.SaveAsync();

            return relation;
        }

        public async Task Delete(Guid senderId, Guid recieverId)
        {
            if (senderId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(senderId)} cannot be {senderId}", nameof(senderId));
            }

            if (recieverId == Guid.Empty)
            {
                throw new ArgumentException($"{nameof(recieverId)} cannot be {recieverId}", nameof(recieverId));
            }

            Relation? relation = await Relations.FirstOrDefaultAsync(
                r => r.SenderId == senderId && r.ReceiverId == recieverId);

            if (relation == null)
            {
                throw new EntityNotFoundException(typeof(Relation));
            }

            _relationRepository.Delete(relation);
            await _relationRepository.SaveAsync();
        }
    }
}