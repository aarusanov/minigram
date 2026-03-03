namespace Minigram.Auth.Dto
{
    public class PagedResponse<TDto>
    {
        public int Count { get; set; }

        public List<TDto> Data { get; set; } = new List<TDto>();
    }
}
