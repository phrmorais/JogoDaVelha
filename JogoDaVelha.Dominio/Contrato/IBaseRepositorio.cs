using JogoDaVelha.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JogoDaVelha.Dominio.Contrato
{
    public interface IBaseRepositorio<TEntity> : IDisposable where TEntity : Entity
    {
        Task<int> Commit();

        TEntity GetId(Guid Id);

        bool Exists(Guid Id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task<int> Save(TEntity entity);
    }
}