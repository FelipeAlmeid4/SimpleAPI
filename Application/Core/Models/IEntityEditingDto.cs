namespace Application.Core.Models
{
    public interface IEntityEditingDto<TId>
    {
        public TId Id { get; set; }
    }
}
