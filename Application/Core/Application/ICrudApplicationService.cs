using Application.Core.Models;
using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Core.Application
{
    public interface ICrudApplicationService
    {
        public interface ICrudApplicationService : IDisposable
        { }

        public interface ICrudApplicationService<TEntity, TId> : ICrudApplicationService
        where TEntity : IEntity<TId>
        {
            Task<TEntity> GetAsync(TId id);
            Task<IList<TEntity>> GetAllAsync();
            Task<TEntity> CreateAsync(TEntity dto);
            Task<TEntity> UpdateAsync(TEntity dto);
            Task DeleteAsync(TId id);
        }

        public interface ICrudApplicationService<TEntity, TId, TEditingDto, TGetDto> : ICrudApplicationService
        where TEntity : IEntity<TId>
        where TEditingDto : IEntityEditingDto<TId>
        where TGetDto : IEntityDto<TId>
        {
            Task<TGetDto> GetAsync(TId id);
            Task<IList<TGetDto>> GetAllAsync();
            Task<TGetDto> CreateAsync(TEditingDto dto);
            Task<TGetDto> UpdateAsync(TEditingDto dto);
            Task DeleteAsync(TId id);
        }
    }
}
