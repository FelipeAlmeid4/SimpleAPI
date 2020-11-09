using System.Collections;

namespace Domain.Common.Entities
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
        bool HasIdentifier();
    }
}
