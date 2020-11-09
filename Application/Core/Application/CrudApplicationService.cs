using Application.Core.Models;
using Application.Core.UnitOfWork;
using AutoMapper;
using Domain.Common.Entities;
using Domain.Core.Repositories;
using SimpleAPI.Framework.Common;
using SimpleAPI.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Application.Core.Application.ICrudApplicationService;

namespace Application.Core.Application
{
    public abstract class CrudApplicationService : Disposable, ICrudApplicationService
    {
        public IServiceProvider ServiceProvider { get; set; }
        public IMapper Mapper { get; set; }

        public TDestination To<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination To<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public IList<TDestination> To<TSource, TDestination>(IList<TSource> sources)
        {
            return sources.As(c => To<TDestination>(c));
        }
    }

    public abstract class CrudApplicationService<TEntity, TId, TEditingDto, TGetDto, TRepository> : CrudApplicationService, ICrudApplicationService<TEntity, TId, TEditingDto, TGetDto>
        where TEntity : IEntity<TId>
        where TEditingDto : IEntityEditingDto<TId>
        where TGetDto : IEntityDto<TId>
        where TRepository : IRepository<TEntity, TId>
    {
        protected readonly TRepository repository;
        public IUnitOfWorkManager unitOfWorkManager { get; set; }

        public CrudApplicationService(TRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TGetDto> GetAsync(TId id)
        {
            return To<TGetDto>(await repository.GetFullAsync(id));
        }

        public async Task<IList<TGetDto>> GetAllAsync()
        {
            return To<TEntity, TGetDto>(await repository.GetAllAsync());
        }

        public async Task<TGetDto> CreateAsync(TEditingDto dto)
        {
            var entity = To<TEditingDto, TEntity>(dto, default);

            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                await repository.CreateAsync(entity);
                await unitOfWork.CompleteAsync();
            }

            return To<TGetDto>(entity);
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = repository.Get(id);
            await repository.DeleteAsync(entity);
        }

        public async Task<TGetDto> UpdateAsync(TEditingDto dto)
        {
            var existingEntity = await repository.GetAsync(dto.Id);
            var entity = To<TEditingDto, TEntity>(dto, existingEntity);

            using (var unitOfWork = unitOfWorkManager.Begin())
            {
                await repository.UpdateAsync(entity);
                await unitOfWork.CompleteAsync();
            }

            return To<TGetDto>(entity);
        }
    }
}
