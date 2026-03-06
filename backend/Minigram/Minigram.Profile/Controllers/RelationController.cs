    namespace Minigram.Profile.Controllers
    {
        using Microsoft.AspNetCore.Mvc;
        using Microsoft.AspNetCore.JsonPatch;
        using Minigram.Profile.Dto;
        using Minigram.Profile.Extensions;
        using Minigram.Profile.Services;
        using Minigram.Profile.Models;
        using Minigram.Core.Dto;

        [ApiController]
        [Route($"{nameof(Profile)}s/[controller]s")]
        public class RelationController : ControllerBase
        {
            private readonly CurrentUserService _currentUserService;

            private readonly RelationService _relationService;

            private Guid UserId =>
                _currentUserService.UserGuid ?? throw new UnauthorizedAccessException();

            public RelationController(
                CurrentUserService currentUserService,
                RelationService relationService,
                ProfileService profileService)
            {
                _currentUserService = currentUserService;
                _relationService = relationService;
            }

            [HttpGet]
            public async Task<PagedResponse<RelationResponseDto>> GetRelationsByStatus(
                [FromQuery] tRelationshipStatus status,
                [FromQuery] QueryParams queryParams)
            {
                int count = await _relationService.CountByStatus(UserId, status);
                List<RelationResponseDto> data = await _relationService.GetAllByStatus(UserId, status, queryParams);

                return new PagedResponse<RelationResponseDto>
                {
                    Count = count,
                    Data = data,
                };
            }

            [HttpGet($"{{{nameof(recieverId)}}}")]
            public async Task<RelationResponseDto> GetRelation([FromRoute] Guid recieverId)
            {
                return await _relationService.Get(UserId, recieverId);
            }

            [HttpPost($"{{{nameof(recieverId)}}}")]
            public async Task<ActionResult> CreateOrUpdateRelation(
                [FromRoute] Guid recieverId,
                [FromQuery] tRelationshipStatus status)
            {
                Relation relation = await _relationService.CreateOrUpdate(UserId, recieverId, status);
                return CreatedAtAction(nameof(GetRelation), relation.ToDto());
            }

            [HttpDelete($"{{{nameof(recieverId)}}}")]
            public async Task DeleteRelation([FromRoute] Guid recieverId)
            {
                await _relationService.Delete(UserId, recieverId);
            }
        }
    }
