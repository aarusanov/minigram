namespace Minigram.Auth.Services
{
    using Minigram.Core.Models;
    using Minigram.Core.Repositories;

    public class RelationService
    {
        private readonly IRepository<Relation> _relationRepository;

        private IQueryable<Relation> Relations => _relationRepository.Get();

        public RelationService(IRepository<Relation> relationRepository)
        {
            _relationRepository = relationRepository;
        }
    }
}