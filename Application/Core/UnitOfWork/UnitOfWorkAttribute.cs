using System;

namespace Application.Core.UnitOfWork
{
    public class UnitOfWorkAttribute : Attribute
    {
        public bool Enabled { get; set; } = true;
    }
}
