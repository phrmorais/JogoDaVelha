using System;

namespace JogoDaVelha.Dominio.Entidade
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            id = id == Guid.Empty ? Guid.NewGuid() : id;
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}