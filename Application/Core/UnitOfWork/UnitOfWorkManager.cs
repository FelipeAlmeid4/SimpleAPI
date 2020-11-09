using SimpleAPI.Framework.Common;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application.Core.UnitOfWork
{
    public class UnitOfWorkManager : Disposable, IUnitOfWorkManager
    {
        protected readonly IServiceProvider serviceProvider;
        protected IUnitOfWork unitOfWork;

        public UnitOfWorkManager(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IUnitOfWork Begin()
        {
            return CreateUnitOfWork();
        }

        public bool HasActiveUnitOfWork()
        {
            return unitOfWork != null;
        }

        protected virtual IUnitOfWork CreateUnitOfWork()
        {
            if (HasActiveUnitOfWork())
            {
                return new InnerUnitOfWork();
            }

            unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            unitOfWork.Begin();
            return unitOfWork;
        }

        protected override void OnDispose()
        {
            unitOfWork?.Dispose();
            unitOfWork = null;
        }
    }
}
