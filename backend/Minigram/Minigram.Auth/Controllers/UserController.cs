namespace Minigram.Auth.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.JsonPatch;
    using Minigram.Auth.Dto;
    using Minigram.Auth.Dto.User;
    using Minigram.Auth.Extensions;
    using Minigram.Auth.Services;
    using Minigram.Core.Models;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CurrentUserService _currentUserService;

        private readonly RelationService _relationService;

        private readonly ProfileService _profileService;

        private Guid UserId =>
            _currentUserService.UserGuid ?? throw new UnauthorizedAccessException();

        public UserController(
            CurrentUserService currentUserService,
            RelationService relationService,
            ProfileService profileService)
        {
            _currentUserService = currentUserService;
            _relationService = relationService;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<PagedResponse<ReadProfileDto>> GetAll([FromQuery] QueryParams queryParams)
        {
            int count = await _profileService.Count();
            List<ReadProfileDto> data = await _profileService.GetAll(queryParams);

            return new PagedResponse<ReadProfileDto>
            {
                Count = count,
                Data = data,
            };
        }

        [HttpGet($"{{{nameof(userId)}}}")]
        public async Task<ReadProfileDto> Get([FromRoute] Guid userId)
        {
            return await _profileService.Get(userId);
        }

        [HttpGet(nameof(Relation))]
        public async Task<PagedResponse<ReadRelationDto>> GetRelationsByStatus(
            [FromQuery] tRelationshipStatus status,
            [FromQuery] QueryParams queryParams)
        {
            int count = await _relationService.CountByStatus(UserId, status);
            List<ReadRelationDto> data = await _relationService.GetAllByStatus(UserId, status, queryParams);

            return new PagedResponse<ReadRelationDto>
            {
                Count = count,
                Data = data,
            };
        }

        [HttpGet($"{nameof(Relation)}/{{{nameof(recieverId)}}}")]
        public async Task<ReadRelationDto> GetRelation([FromRoute] Guid recieverId)
        {
            return await _relationService.Get(UserId, recieverId);
        }

        [HttpPost($"{nameof(Relation)}/{{{nameof(recieverId)}}}")]
        public async Task<ActionResult> CreateOrUpdateRelation(
            [FromRoute] Guid recieverId,
            [FromQuery] tRelationshipStatus status)
        {
            Relation relation = await _relationService.CreateOrUpdate(UserId, recieverId, status);
            return CreatedAtAction(nameof(GetRelation), relation.ToDto());
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateProfileDto dto)
        {
            User user = await _profileService.Update(UserId, dto);
            return CreatedAtAction(nameof(GetRelation), user.ToProfileDto());
        }

        [HttpPatch]
        public async Task<ActionResult> Patch([FromBody] JsonPatchDocument<UpdateProfileDto> patch)
        {
            User user = await _profileService.Patch(UserId, patch);
            return CreatedAtAction(nameof(GetRelation), user.ToProfileDto());
        }

        [HttpDelete($"{nameof(Relation)}/{{{nameof(recieverId)}}}")]
        public async Task DeleteRelation([FromRoute] Guid recieverId)
        {
            await _relationService.Delete(UserId, recieverId);
        }
    }
}
