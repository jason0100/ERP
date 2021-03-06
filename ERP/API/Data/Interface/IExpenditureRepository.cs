﻿using ErrorCodeLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Data.Interface
{

    public interface IExpenditureRepository<TEntity>
        where TEntity : class
    {
        ResultModel FindAll(Expression<Func<TEntity, bool>> expression);

        TEntity FindById(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
