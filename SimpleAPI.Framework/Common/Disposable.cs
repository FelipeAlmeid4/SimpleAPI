using System;
using System.Threading.Tasks;

namespace SimpleAPI.Framework.Common
{
    public class Disposable : IDisposable
    {
        private bool disposing = true;

        public virtual async void Dispose()
        {
            if (disposing)
            {
                OnDispose();
                await OnDisposeAsync();
            }

            disposing = false;
        }

        protected virtual void OnDispose()
        { }

        protected virtual Task OnDisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
