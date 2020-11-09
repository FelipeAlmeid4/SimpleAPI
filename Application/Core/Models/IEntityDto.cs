namespace Application.Core.Models
{
    public interface IEntityDto<TId>
    {
        public TId Id { get; set; }
    }
}
