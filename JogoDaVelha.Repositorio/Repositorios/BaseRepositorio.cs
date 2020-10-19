using JogoDaVelha.Dominio.Contrato;
using JogoDaVelha.Dominio.Entidade;
using JogoDaVelha.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JogoDaVelha.Repositorio.Repositorios
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : Entity
    {
        protected readonly JogoDaVelhaContexto _jogoDaVelhaContexto;

        public BaseRepositorio(JogoDaVelhaContexto jogoDaVelhaContexto)
        {
            _jogoDaVelhaContexto = jogoDaVelhaContexto;
        }

        public void Add(TEntity entity)
        {
            _jogoDaVelhaContexto.Set<TEntity>().Add(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            _jogoDaVelhaContexto.Set<TEntity>().Update(entity).State = EntityState.Modified;
        }

        public TEntity GetId(Guid Id)
        {
            return _jogoDaVelhaContexto.Set<TEntity>().FirstOrDefault(c => c.Id == Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _jogoDaVelhaContexto.Set<TEntity>().ToList();
        }

        public void Dispose()
        {
            _jogoDaVelhaContexto.Dispose();
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _jogoDaVelhaContexto.Set<TEntity>().Where(predicate);
        }

        public async Task<int> Commit()
        {
            return await _jogoDaVelhaContexto.SaveChangesAsync();
        }

        public bool Exists(Guid Id)
        {
            return _jogoDaVelhaContexto.Set<TEntity>().Find(Id) != default;
        }

        public async Task<int> Save(TEntity entity)
        {
            if (Exists(entity.Id))
            {
                Update(entity);
            }
            else
            {
                Add(entity);
            }
            return await Commit();
        }
    }
}