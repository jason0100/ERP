using API.Data.Interface;
using API.Models.Project;
using ErrorCodeLib;
using ErrorCodeLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NLog;

namespace API.Data.Repository
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {

        private readonly ErpDbContext Context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 建構EF一個Entity的Repository，需傳入此Entity的Context。
        /// </summary>
        /// <param name="inContext">Entity所在的Context</param>
        public GenericRepository(ErpDbContext inContext)
        {
            Context = inContext;
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 取得符合條件的內容
        /// </summary>
        /// <param name="expression">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression).AsQueryable();
     
             

        }

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        public TEntity FindById(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        public void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }


        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        public void Delete(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

    }
}
