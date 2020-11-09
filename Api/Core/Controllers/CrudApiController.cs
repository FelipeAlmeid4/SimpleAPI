using Application.Core.Models;
using Application.Core.UnitOfWork;
using Domain.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.WebFramework.Mvc.Response;
using System.Threading.Tasks;
using static Application.Core.Application.ICrudApplicationService;

namespace Api.Core.Controllers
{
    public abstract class CrudApiController : ControllerBase
    {
        protected IActionResult Result<TObj>(TObj obj, bool returnNotFoundIfIsNull = true)
        {
            if (returnNotFoundIfIsNull && obj == null)
            {
                return NotFound();
            }

            return Ok(new ResultResponse<TObj>(obj));
        }

        protected IActionResult Result(bool success)
        {
            return Ok(new ResultResponse(success));
        }
    }

    public class CrudApiController<TEntity, TId, TEditingDto, TGetDto, TEntityService> : CrudApiController
       where TEntity : IEntity<TId>
       where TEditingDto : IEntityEditingDto<TId>
       where TGetDto : IEntityDto<TId>
       where TEntityService : ICrudApplicationService<TEntity, TId, TEditingDto, TGetDto>
    {
        protected readonly TEntityService appService;

        public CrudApiController(TEntityService appService)
        {
            this.appService = appService;
        }

        [UnitOfWork(Enabled = false)]
        public async Task<IActionResult> CreateAsync([FromBody] TEditingDto entity)
        {
            return Result(await appService.CreateAsync(entity));
        }

        public async Task<IActionResult> DeleteAsync(TId id)
        {
            await appService.DeleteAsync(id);
            return Result(true);
        }

        public async Task<IActionResult> GetAllAsync()
        {
            return Result(await appService.GetAllAsync());
        }

        public async Task<IActionResult> GetAsync(TId id)
        {
            return Result(await appService.GetAsync(id));
        }

        [UnitOfWork(Enabled = false)]
        public async Task<IActionResult> UpdateAsync([FromBody] TEditingDto entity)
        {
            await appService.UpdateAsync(entity);
            return Result(true);
        }
    }
}
