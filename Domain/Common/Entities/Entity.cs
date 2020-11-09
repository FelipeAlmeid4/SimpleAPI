using SimpleAPI.Framework.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Common.Entities
{
    public class Entity<TId> : IEntity<TId>
    {
        protected Guid instaceId;

        public virtual Guid InstaceId
        {
            get
            {
                if (instaceId.IsNullOrEmpty())
                {
                    instaceId = Guid.NewGuid();
                }

                return instaceId;
            }
        }

        public virtual TId Id { get; set; }

        public virtual bool HasIdentifier()
        {
            return !EqualityComparer<TId>.Default.Equals(Id, default);
        }
    }
}
