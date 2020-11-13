using ErrorCodeLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Data.Interface
{

    public interface IProjectRepository<TEntity>
        where TEntity : class
    {
        ResultModel FindAll(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> ListProtoType(Expression<Func<TEntity, bool>> expression);

        TEntity FindById(int id);
        ResultModel Create(TEntity entity);
        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
