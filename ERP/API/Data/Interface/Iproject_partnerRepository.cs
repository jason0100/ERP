using ErrorCodeLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Data.Interface
{

    public interface Iproject_partnerRepository<TEntity>
        where TEntity : class
    {
        ResultModel FindAll(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> ListProtoType(Expression<Func<TEntity, bool>> expression);


        TEntity FindById(int id);
        void Create(TEntity entity);
        ResultModel Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
