﻿namespace PCONTB.Panel.Domain.Common
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; private set; }

        protected Entity()
        {
        }

        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}
